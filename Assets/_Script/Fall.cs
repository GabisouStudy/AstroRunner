using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
