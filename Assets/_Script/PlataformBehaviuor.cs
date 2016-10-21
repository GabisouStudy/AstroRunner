using UnityEngine;
using System.Collections;

public class PlataformBehaviuor : MonoBehaviour {

    private Transform player;
    private bool call;

    void Start()
    {
        call = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
            Invoke("Decrease", 4f);

    }
	void Update () {
        if (!call && player.position.x > this.transform.position.x)
        {
            Invoke("Decrease", 4f);
            call = true;
        }
    }
    void Decrease()
    {
        try
        {
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        catch { }
        Invoke("Destroy", 4f);
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
