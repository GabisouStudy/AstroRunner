using UnityEngine;
using System.Collections;

public class SpawnsObjectsManager : MonoBehaviour {

    [SerializeField]
    private bool first;
    [SerializeField]
    private float myStart;
    [SerializeField]
    private float myEnd;
    [SerializeField]
    private GameObject[] goods;

    void Start () {
        if(!first)
            transform.position = transform.position + Vector3.right * myStart;
        Invoke("GeneratorOfMap", 1);
	}
	
	void Update () {
	
	}
  
    void GeneratorOfMap()
    {
        int i = Random.Range(0, goods.Length - 1);
        if(i.Equals(0)) i = Random.Range(0, goods.Length - 1);
        GameObject gameObject = (GameObject)Instantiate(goods[i], transform.position + Vector3.right * myEnd*-1, Quaternion.identity);
    }

}
