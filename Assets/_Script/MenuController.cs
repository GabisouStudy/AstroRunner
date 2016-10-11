using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private SpawnsObjectsManager start;
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private GameObject tuto;
    [SerializeField]
    private Animator[] tut;


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
                foreach(Animator i in tut)
                    i.SetBool("end", true);
                Destroy(this.gameObject);
            }
        }
	}
}
