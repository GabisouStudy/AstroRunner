using UnityEngine;
using System.Collections;

public class SpawnsObjectsManager : MonoBehaviour {

    public GameObject ground;
    public Transform target_;

	void Start () {
        InvokeRepeating("GeneratorOfMap", 0f, 1f);
	}
	
	void Update () {
	
	}
  
    void GeneratorOfMap()
    {
        GameObject gameObject = (GameObject)Instantiate(ground, new Vector2(target_.position.x + 7.2f, 0), 
            Quaternion.identity);
        target_ = gameObject.transform;
    }

}
