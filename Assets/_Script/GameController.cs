using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text t_Score, t_Recorde;
    private float score, recorde;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject AudioGame;


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
        t_Recorde.text = "" + Mathf.Floor(recorde);

    }
    void Actived_AudioGame()
    {
        AudioGame.SetActive(true);
    }
    public float GetScore()
    {
        return score;
    }
    void Update()
    {
        if (!InputMouse.tuto && !InputMouse.menu && !AudioGame.activeSelf) Invoke("Actived_AudioGame", 4.4f);
        if (player.GetMoveSpeed() > 0 && !player.GetDead() && player.GetDirection() > 0 && !InputMouse.menu && !InputMouse.tuto)
        {
            score += 5 * Time.deltaTime * player.GetMoveSpeed()/10;
            t_Score.text = "" + Mathf.Floor(score);
        }
        if (player.GetDead())
        {
            //Encriptografar dps namoral
            if(PlayerPrefs.GetFloat("Record") < score && PlayerPrefs.GetFloat("Record") != score)
            {
               PlayerPrefs.SetFloat("Record", score);
            }

        }
    }
}
