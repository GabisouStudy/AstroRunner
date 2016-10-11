using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private SpawnsObjectsManager start;
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private GameObject tuto;


	void Update () {
        if (!InputMouse.menu)
        {
            if (start != null)
            {
                start.enabled = true;
                tuto.SetActive(true);
            }
            if (!InputMouse.tuto)
            {
                gc.enabled = true;
                //change to start animation
                Destroy(tuto);
                Destroy(this.gameObject);
            }
        }
	}
}
