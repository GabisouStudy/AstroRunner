using UnityEngine;
using System.Collections;

public class SpawnsObjectsManager : MonoBehaviour {

    private Player player;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject actual;
    [SerializeField]
    private GameObject[] tiles;
    [SerializeField]
    private int[] tilesEasy;
    [SerializeField]
    private int[] tilesMedium;
    private GameObject[] Spawned = new GameObject[20];

    private int height;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject[] Spawned = new GameObject[20];
        Invoke("GeneratorOfMap", 1);
	}
    void GeneratorOfMap()
    {
        if (player.GetMoveSpeed() > 0 && player.GetDirection() > 0)
        {
            int i = 0;
            do
            {
                if (gameController.GetScore() > 200)
                    i = Random.Range(0, tiles.Length);
                else if (gameController.GetScore() > 120)
                {
                    i = tilesMedium[Random.Range(0, tilesMedium.Length)];
                }
                else
                    i = tilesEasy[Random.Range(0, tilesEasy.Length)];
            }
            while (!actual.GetComponent<SpawnTileSet>().GetCanSpawn(i));

            GameObject gameObject = (GameObject)Instantiate(tiles[i], actual.transform.position + Vector3.right * actual.GetComponent<SpawnTileSet>().GetMyEnd() *-1, Quaternion.identity);
            if(actual.tag.Equals("down"))
                height -= 1;
            else if (actual.tag.Equals("up"))
                height += 1;
            gameObject.GetComponent<SpawnTileSet>().ChangeHeight(height);
            actual = gameObject;
        }
        Invoke("GeneratorOfMap", 1);
    }

}
