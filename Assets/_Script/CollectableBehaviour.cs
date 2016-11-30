using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {
    [SerializeField]
    private int value;
    [SerializeField]
    private int possibility;
    private bool collect;

    void Start()
    {
        int i = (possibility!= 0)? Random.Range(0, possibility) : Random.Range(0, 5);
        if (i != 0) Destroy(this.gameObject);
        collect = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !collect)
        {
            collision.gameObject.GetComponent<Player>().SetMoney((value.Equals(0))?1:value);
            this.GetComponent<AudioSource>().enabled=true;
            this.GetComponent<AudioSource>().Play();
            this.GetComponentInChildren<SpriteRenderer>().enabled = false;
            collect = true;
        }
    }
    void Update()
    {
        if(!this.GetComponentInChildren<SpriteRenderer>().enabled && !this.GetComponent<AudioSource>().isPlaying)
            Destroy(this.gameObject);
    }
}
