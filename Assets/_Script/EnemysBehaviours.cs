using UnityEngine;
using System.Collections;

public class EnemysBehaviours : MonoBehaviour {

    public string type;
    public bool alert;
    Rigidbody2D rigidbody;
    private Transform player;
    public int rageAlert;
    public Sprite spriteAlert;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = this.GetComponent<Rigidbody2D>();
        alert = false;
	}
	
	void Update () {
        switch (type)
        {
            case "Rocket":
                RocketUpdate();
                break;
            case "Opa":
               
                break;
            default:
                break;
        }
	}
    void RocketUpdate () 
    {
        if(alert)
        {
            rigidbody.velocity =-Vector2.right * 6;
            this.GetComponent<SpriteRenderer>().sprite = spriteAlert;
        }
        else  
        {
            if (this.transform.position.x < player.transform.position.x + rageAlert)
            alert = true;
          
        }
    }
  

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == ("hatch")) Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), this.GetComponent<PolygonCollider2D>());
        if (collision.gameObject.tag != ("hatch") || collision.gameObject.layer.Equals("Ground") || collision.gameObject.tag.Equals("Player")) Destroy(this.gameObject);
        if (collision.gameObject.tag.Equals("Player") && type.Equals("Rocket"))
        {
            collision.gameObject.GetComponent<Player>().dead = true;
        }  
    }
}
