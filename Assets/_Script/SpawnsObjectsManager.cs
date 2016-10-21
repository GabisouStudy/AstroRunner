using UnityEngine;
using System.Collections;

public class SpawnsObjectsManager : MonoBehaviour {

    private Player player;
    [SerializeField]
    private bool first;
    [SerializeField]
    private bool canRepeat;
    [SerializeField]
    private float myStart;
    [SerializeField]
    private float myEnd;
    [SerializeField]
    private GameObject[] lateral;
    [SerializeField]
    private GameObject[] goods;

    private int height;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(!first)
            transform.position = transform.position + Vector3.right * myStart;
        if (lateral.Length > 0)
            GenerateLateral();
        if (!InputMouse.tuto && !InputMouse.menu)
            Invoke("GeneratorOfMap", 1);
        else
            Invoke("GeneratorOfMap", 2f);
	}
	
	void GenerateLateral ()
    {
        int i = Random.Range(-1, lateral.Length + 1);
        if(i.Equals(lateral.Length))
        {
            Instantiate(lateral[0], transform.position - Vector3.right * lateral[0].GetComponent<SpawnsObjectsManager>().GetMyStart(), Quaternion.identity);
            Instantiate(lateral[1], transform.position - Vector3.right * lateral[1].GetComponent<SpawnsObjectsManager>().GetMyStart(), Quaternion.identity);
        }
        else if(i.Equals(-1))
        {
            Instantiate(lateral[0], transform.position - Vector3.right * lateral[0].GetComponent<SpawnsObjectsManager>().GetMyStart(), Quaternion.identity);
        }
        else if (i.Equals(1))
        {
            Instantiate(lateral[1], transform.position - Vector3.right * lateral[1].GetComponent<SpawnsObjectsManager>().GetMyStart(), Quaternion.identity);
        }
    }
    public float GetMyStart()
    {
        return myStart;
    }
    public void ChangeHeight(int h)
    {
        height += h;
        transform.position += Vector3.up * height * 10;
    }
    void GeneratorOfMap()
    {
        if (player.moveSpeed > 0 && player.direction > 0)
        {
            int i = Random.Range(0, goods.Length);
            if (canRepeat && i.Equals(0)) i = Random.Range(0, goods.Length);
            GameObject gameObject = (GameObject)Instantiate(goods[i], transform.position + Vector3.right * myEnd*-1, Quaternion.identity);
            if(this.tag.Equals("down"))
            {
                gameObject.GetComponent<SpawnsObjectsManager>().ChangeHeight(height - 1);
            }
            if (this.tag.Equals("up"))
            {
                gameObject.GetComponent<SpawnsObjectsManager>().ChangeHeight(height + 1);
            }
        }
        else
        {            
            Invoke("GeneratorOfMap", 1);
        }
    }

}
