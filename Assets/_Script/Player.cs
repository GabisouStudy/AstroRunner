using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;
    private Animator animator;
    public Sprite sprite_Jump;
    public Sprite sprite_Croush;
    public Sprite sprite_Swipe;
    private SpriteRenderer spriteRenderer;
    public int direction;
    public bool croushe;
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	void Start()
    {
        croushe = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D> ();
        animator = GetComponent<Animator>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
        direction = 1;
	}

	void Update() 
    {
        Vector2 input = new Vector2(direction, Input.GetAxisRaw("Vertical"));
        if (direction.Equals(1)) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
        if (croushe) moveSpeed = 3;
        else moveSpeed = 6;
        int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}
        if(wallSliding)
        {
            animator.enabled = false;
            spriteRenderer.sprite = sprite_Swipe;
            Debug.Log("oi");
            if(direction > 0)
                spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }
        if (Input.GetAxis("Vertical") >= 0) {
			if (Input.GetAxisRaw("Vertical")> 0) {
				if (wallSliding) {
                    direction = direction * -1;
                    if (wallDirX == input.x) {
						velocity.x = -wallDirX * wallJumpClimb.x;
						velocity.y = wallJumpClimb.y;
					} else if (input.x == 0) {
						velocity.x = -wallDirX * wallJumpOff.x;
						velocity.y = wallJumpOff.y;
					} else {
						velocity.x = -wallDirX * wallLeap.x;
						velocity.y = wallLeap.y;
					}
				}
                
                if (controller.collisions.below) {
					velocity.y = maxJumpVelocity;
				}
			}
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
            if (Input.GetAxis("Vertical") < 0)
            {
                spriteRenderer.sprite = sprite_Croush;
                if (animator.enabled) animator.enabled = false;
                GetComponent<BoxCollider2D>().offset = new Vector2(0, -1f);
                GetComponent<BoxCollider2D>().size = new Vector2(4.77f, 3f);
                croushe = true;

            }
            else
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                GetComponent<BoxCollider2D>().size = new Vector2(4.8f, 6.42f);
                if(!animator.enabled) animator.enabled = true;
                croushe = false;
            }
        }
        else
        {
            if(!wallSliding)
            {
                if (animator.enabled) animator.enabled = false;
                 spriteRenderer.sprite = sprite_Jump;
            }

        }

    }
}