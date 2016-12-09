using UnityEngine;
using System.Collections;

public class SpawnsObjectsManager : MonoBehaviour {

    public enum Difficulty 
    {
        Easy,
        Medium,
        Hard,
    }

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
    [SerializeField]
    private Difficulty difficulty;

    private int startHard, startMedium;
    private float height;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject[] Spawned = new GameObject[20];
        if(difficulty.Equals(Difficulty.Easy))
        {
            startHard = 100;
            startMedium = 60;
        }
        else if(difficulty.Equals(Difficulty.Medium))
        {
            startHard = 70;
            startMedium = 1;
        }
        else if (difficulty.Equals(Difficulty.Hard))
        {
            startHard = 2;
            startMedium = 1;
        }
	}
    public void GeneratorOfMap()
    {
        if (player.GetMoveSpeed() > 0 && player.GetDirection() > 0)
        {
            int i = 0;
            do
            {
                if (gameController.GetScore() > startHard)
                    i = Random.Range(0, tiles.Length);
                else if (gameController.GetScore() > startMedium)
                {
                    i = tilesMedium[Random.Range(0, tilesMedium.Length)];
                }
                else
                    i = tilesEasy[Random.Range(0, tilesEasy.Length)];
            }
            while (!actual.GetComponent<SpawnTileSet>().GetCanSpawn(i));

            GameObject gameObject = (GameObject)Instantiate(tiles[i], actual.transform.position + Vector3.right * actual.GetComponent<SpawnTileSet>().GetMyEnd() *-1, Quaternion.identity);
            gameObject.GetComponent<SpawnTileSet>().ChangeHeight(height);
            height += gameObject.GetComponent<SpawnTileSet>().GetEndHeight();
            actual = gameObject;
            
        }
    }

}
