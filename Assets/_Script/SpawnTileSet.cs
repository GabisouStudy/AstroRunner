using UnityEngine;
using System.Collections;

public class SpawnTileSet : MonoBehaviour {

    [SerializeField]
    private bool first;
    [SerializeField]
    private bool[] canSpawn = new bool[20];
    [SerializeField]
    private float myStart;
    [SerializeField]
    private float myEnd;
    [SerializeField]
    private float endHeight;
    void Start()
    {
        if (!first)
            transform.position = transform.position + Vector3.right * myStart;
    }
    public bool GetCanSpawn(int i)
    {
        return (canSpawn[i]);
    }
    public void ChangeHeight(float h)
    {
        transform.position = new Vector3(this.transform.position.x, h, 0);
    }
    public float GetMyStart()
    {
        return (myStart);
    }
    public float GetEndHeight()
    {
        return (endHeight);
    }
    public float GetMyEnd()
    {
        return (myEnd);
    }
}
