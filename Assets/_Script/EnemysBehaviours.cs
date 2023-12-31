﻿using UnityEngine;
using System.Collections;

public class EnemysBehaviours : MonoBehaviour {

    public string type;
    public bool alert;
    Rigidbody2D rigidbody;
    private Transform player;
    public int rageAlert;
    public int rageAlertY;
    public Sprite spriteAlert;
    private bool fallingDown;


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = this.GetComponent<Rigidbody2D>();
        alert = false;
        if (rageAlertY.Equals(0))
            rageAlertY = 2;
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
            if (rageAlert > 0)
                rigidbody.velocity = (fallingDown ? new Vector2(-0.8f, -0.3f) : new Vector2(-1f, 0)) * 6;
            else
                rigidbody.velocity = (fallingDown ? new Vector2(0.8f, -0.3f) : new Vector2(1f, 0)) * 6;
            this.GetComponent<SpriteRenderer>().sprite = spriteAlert;
           
        }
        else  
        {
 
            if(this.transform.position.y + rageAlertY > player.transform.position.y && this.transform.position.y - rageAlertY < player.transform.position.y)
                if (this.transform.position.x < player.transform.position.x + rageAlert)
                {
                    alert = true;
                    this.GetComponent<AudioSource>().Play();
                }
        
        }
    }
  

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == ("hatch")) Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), this.GetComponent<PolygonCollider2D>());
            if (collision.gameObject.tag != ("hatch") && alert || collision.gameObject.layer.Equals("Ground") && alert || collision.gameObject.tag.Equals("Player") && alert)
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                Invoke("Delete", 1);
            }
        if (collision.gameObject.tag.Equals("Player") && type.Equals("Rocket"))
        {
            this.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Player>().SetDead(true);


        }  
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Player") && type.Equals("Rocket") && alert)
        {
            collision.gameObject.GetComponent<Player>().doobleJump = true;

            collision.gameObject.GetComponent<Player>().Jump(false,1,1);
            fallingDown = true;

        }
    }
    void Delete()
    {
        Destroy(this.gameObject);
    }
  
}
