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
    private Animator music;
    [SerializeField]
    private AudioSource explode;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private RuntimeAnimatorController z;
    private int g = 0;

	void Start(){
		AdsController.Init();
	}

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
            if (!InputMouse.tuto && g.Equals(1))
            {
                music.enabled = true;
                explode.enabled = true;
                Invoke("StartPlayer", 1f);
                g = 2;
            }
        }
	}


    void StartPlayer()
    {
        gc.enabled = true;
        start.enabled = true;
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Animator>().runtimeAnimatorController = z;
        spriteR.sprite = sprite;
        spriteR.sortingOrder = 1;
        Invoke("Jump", 0.01f);
    }
    void Jump()
    {
        player.GetComponent<Player>().Jump();

    }
}
