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
	void Update () {
        if (!call && player.position.x > this.transform.position.x)
        {
            Invoke("Decrease", 4f);
            call = true;
        }
    }
    void Decrease()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
        Invoke("Destroy", 4f);
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
