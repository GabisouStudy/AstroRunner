using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {
    [SerializeField]
    private int value;
    [SerializeField]
    private int possibility;


    void Start()
    {
        int i = (possibility!= 0)? Random.Range(0, possibility) : Random.Range(0, 5);
        if (i != 0) Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().SetMoney((value.Equals(0))?1:value);
            Destroy(this.gameObject);
        }
    }
}
