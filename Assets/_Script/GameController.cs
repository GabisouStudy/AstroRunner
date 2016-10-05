using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] Tobjs = new GameObject[3];
    int nextObj;
    int lastObj;
    public int radom;
    public GameObject nextObjects;

	void Start () {
  
	}
   public GameObject NextObj()
    {
        radom = Random.Range(0,Tobjs.Length);
        nextObj = radom;
        if (nextObj == lastObj) radom = Random.Range(0, Tobjs.Length);
        else
        {
            nextObjects = Tobjs[radom];
            lastObj = radom;
        }
        Debug.Log(nextObj + "" + lastObj + "" + radom);
        return nextObjects = Tobjs[radom];

    }
	void Update () {
	
	}
}
