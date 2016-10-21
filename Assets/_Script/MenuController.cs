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
    private GameObject tut;
    [SerializeField]
    private GameObject logo;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private SpriteRenderer spriteR;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private RuntimeAnimatorController z;
    private int g = 0;


	void Update () {
        if (!InputMouse.menu)
        {
            if (start != null && g.Equals(0))
            {
                logo.SetActive(false);
                tuto.SetActive(true);
                tut.SetActive(true);
                g = 1;
            }
            if (!InputMouse.tuto)
            {
                gc.enabled = true;
                start.enabled = true;
                player.GetComponent<Animator>().runtimeAnimatorController = z;
                player.GetComponent<Player>().Jump();
                spriteR.sprite = sprite;
                spriteR.sortingOrder = 1;
                Destroy(this.gameObject);
            }
        }
	}
}
