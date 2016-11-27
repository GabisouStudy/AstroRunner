using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    private float maxJumpHeight = 2;
    private float minJumpHeight = 1;
    private float timeToJumpApex = .3f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6;
    private Animator animator;
    [SerializeField]
    private Camera camera_;
    [SerializeField]
    private Sprite sprite_Jump, sprite_Dead, sprite_Croush, sprite_Swipe, sprite_Stop;
    private SpriteRenderer spriteRenderer;
    private int direction;
    private bool croushe;
    private Vector2 wallJumpClimb, wallJumpOff, wallLeap;
    private bool dead;
    private bool decrease;

    private float wallSlideSpeedMax = 3;
    private float wallStickTime = .25f;
    float timeToWallUnstick;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    Controller2D controller;
    private float limiteY = -100;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private InputMouse InputMouse_Up, InputMouse_Down;
    [SerializeField]
    private bool invertGravity;
    private int invert_gravity;
    [SerializeField]
    private int money;
    [SerializeField]
    private Image[] Botoes;
    void Start()
    {
        Botoes[0].enabled = true;
        Botoes[1].enabled = true;
        wallJumpClimb = new Vector2(5, 15);
        wallJumpOff = new Vector2(5, 15);
        wallLeap = new Vector2(3, 15);
        dead = false;
        croushe = false;
        decrease = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        direction = 1;

        if (invert_gravity != 1 && invert_gravity != -1) invert_gravity = 1;

    }
    void Decrease()
    {
        decrease = true;
    }
    
    void GameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public bool GetCroushe() { return croushe; }
    public bool GetDead() { return dead; }
    public void SetGravity(bool j) { invertGravity = j; }
    public void SetMoney(int j)
    {
        money += j;
    }
    public void SetDead(bool j) { dead = j; }
    public float GetMoveSpeed() { return moveSpeed; }
    public int GetDirection() { return direction; }
    void Update()
    {
        if (invertGravity)
        {
            if (!GetComponent<SpriteRenderer>().flipY)
            {
                GetComponent<SpriteRenderer>().flipY = true;
                invert_gravity = -1;
            }

        }
        else if (!invertGravity && GetComponent<SpriteRenderer>().flipY)
        {
            GetComponent<SpriteRenderer>().flipY = false;
            invert_gravity = 1;
        }
        if (decrease)
        {
            Vector2 input = new Vector2(0, 0);
            velocity.y -= 1;
            controller.Move(velocity * Time.deltaTime, input);
            Invoke("GameOver", 1.5f);
        }
        if (dead && !decrease)
        {
            Invoke("Decrease", 0.2f);
            Vector2 input = new Vector2(0, 0);
            velocity.y = wallLeap.y;
            this.GetComponent<Controller2D>().collisionMask = 0;
            this.GetComponent<BoxCollider2D>().enabled = false;
            velocity.x = 0;
            velocity.y += gravity * invert_gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);
            if (animator.enabled) animator.enabled = false;
            spriteRenderer.sprite = sprite_Dead;
            camera_.transform.SetParent(null);
        }
        else if (!decrease && !InputMouse.menu && !InputMouse.tuto)
        {
            if (this.transform.position.y < limiteY || this.transform.position.y >50) dead = true;
            Vector2 input = new Vector2(direction, Input.GetAxisRaw("Vertical"));
            if (direction.Equals(1)) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
            if (croushe) moveSpeed = 3;
            else moveSpeed = 6;
            int wallDirX = (controller.collisions.left) ? -1 : 1;

            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

            bool wallSliding = false;
            if ((controller.collisions.left || controller.collisions.right))
            {
                moveSpeed = 0;
                if (animator.enabled) animator.enabled = false;

            }
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && !controller.collisions.above)
            {

                wallSliding = true;

                if (velocity.y < -wallSlideSpeedMax && !invertGravity)
                {
                   velocity.y = -wallSlideSpeedMax;
                  
                }

                else if (velocity.y > wallSlideSpeedMax && invertGravity)
                {
                    velocity.y = wallSlideSpeedMax;
                }

                if (timeToWallUnstick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;

                    if (input.x != wallDirX && input.x != 0)
                    {
                        timeToWallUnstick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnstick = wallStickTime;
                    }
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }

            }
            if (wallSliding)
            {
                if (animator.enabled) animator.enabled = false;
                spriteRenderer.sprite = sprite_Swipe;
                if (direction > 0)
                    spriteRenderer.flipX = true;
                else spriteRenderer.flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) || InputMouse_Up.GetJump() || Input.GetAxisRaw("Vertical") > 0)
            {
                Jump(wallSliding, wallDirX, int.Parse(input.x.ToString()));
            }

            velocity.y += gravity * invert_gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }


            if (controller.collisions.below && invert_gravity.Equals(1) || controller.collisions.above && invert_gravity.Equals(-1))
            {
                velocity.y = 0;
                //velocity.y bug
                if (InputMouse_Down.GetShrink() || Input.GetAxisRaw("Vertical") < 0)
                {
                    if (animator.enabled) animator.enabled = false;
                    spriteRenderer.sprite = sprite_Croush;
                    if (invert_gravity.Equals(1))
                    {
                        GetComponent<BoxCollider2D>().offset = new Vector2(0, -1f);
                        GetComponent<BoxCollider2D>().size = new Vector2(3.6f, 3f);
                    }
                    else
                    {
                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 1f);
                        GetComponent<BoxCollider2D>().size = new Vector2(3.6f, 3f);
                    }
                    croushe = true;
                    Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + 0.6f), Color.blue);
                }
                else if (controller.collisions.below)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.6f, layerMask);
                    if (hit.collider == null)
                    {
                        croushe = false;
                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                        GetComponent<BoxCollider2D>().size = new Vector2(3.6f, 6.42f);

                        if (!animator.enabled) animator.enabled = true;

                    }
                    //else Debug.Log(hit.collider.gameObject);

                }
                else if (controller.collisions.above)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, layerMask);
                    Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 0.6f), Color.blue);

                    if (hit.collider == null)
                    {
                        croushe = false;
                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                        GetComponent<BoxCollider2D>().size = new Vector2(3.6f, 6.42f);
                        if (!animator.enabled) animator.enabled = true;

                    }
                    //else Debug.Log(hit.collider.gameObject);

                }
            }

            if (!wallSliding && !controller.collisions.below && !croushe && !controller.collisions.above)
            {
                if (animator.enabled) animator.enabled = false;
                spriteRenderer.sprite = sprite_Jump;
            }

        }
    }
    public void Jump(bool wallSliding, int wallDirX, int input)
    {
        InputMouse_Up.SetJump(false);
        if (wallSliding)
        {
            direction = direction * -1;
            if (wallDirX == input)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y * invert_gravity;
            }
            else if (input == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y * invert_gravity;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y * invert_gravity;
            }
        }

        if (controller.collisions.below && invert_gravity.Equals(1) && !croushe)
        {
            velocity.y = maxJumpVelocity;
        }
        else if (controller.collisions.above && invert_gravity.Equals(-1) && !croushe)
        {
            velocity.y = maxJumpVelocity * invert_gravity;
        }
    }
    public void Jump()
    {
        InputMouse_Up.SetJump(false);
        velocity.y = maxJumpVelocity * invert_gravity;
    }
}