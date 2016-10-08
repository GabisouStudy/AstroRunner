using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text t_Score;
    public Text t_Recorde;
    public float score;
    public float recorde;
    public Player player;

    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("Record"))
        {
            recorde = PlayerPrefs.GetFloat("Record");
        }

        else
        {
            recorde = 0;
        }
        t_Recorde.text = "Record: " + Mathf.Floor(recorde);

    }

    void Update()
    {
        if(player.moveSpeed != 0 && !player.dead)
        {
            score += 5 * Time.deltaTime;
            t_Score.text = "Score: " + Mathf.Floor(score);
        }
        if (player.dead)
        {
            if(PlayerPrefs.GetFloat("Record") < score)
            {
                PlayerPrefs.SetFloat("Record", score);
            }

        }
    }
}
