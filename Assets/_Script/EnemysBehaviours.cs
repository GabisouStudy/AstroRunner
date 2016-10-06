using UnityEngine;
using System.Collections;

public class EnemysBehaviours : MonoBehaviour {

    public string type;
    public bool alert;
    Rigidbody2D rigidbody;
    public Transform player;
    public int rageAlert;
    public Sprite spriteAlert;

	void Start () {
        //alert = false;
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
            rigidbody.AddRelativeForce(-Vector2.right * 10);
            this.GetComponent<SpriteRenderer>().sprite = spriteAlert;
        }
        else  
        {
            if (this.transform.position.x < player.transform.position.x + rageAlert)
            alert = true;
          
        }
    }
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject);
        if (collision.gameObject.tag.Equals("Player") && type.Equals("Rocket"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }  
    }
}
