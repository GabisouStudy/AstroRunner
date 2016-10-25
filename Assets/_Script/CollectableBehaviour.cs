using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {
    [SerializeField]
    private int value;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().SetMoney(value);
            Destroy(this.gameObject);
        }
    }
}
