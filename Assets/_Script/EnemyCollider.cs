using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (this.tag.Equals("bomb") && !collision.gameObject.GetComponent<Player>().dead)
            {
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            collision.gameObject.GetComponent<Player>().dead = true; 
        }
    }
}
