using UnityEngine;
using System.Collections;

public class StartSpawn : MonoBehaviour
{
    [SerializeField]
    private SpawnsObjectsManager code;

    void Start()
    {
        code = GameObject.FindGameObjectWithTag("Make").GetComponent<SpawnsObjectsManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            code.GeneratorOfMap();
        }

    }
}
