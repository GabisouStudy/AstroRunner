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
    private bool controller_touch;

    void Start()
    {
        controller_touch = true;
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

    void Update()
    {
        if(controller_touch) {
        }
        else
        if (player.GetMoveSpeed() > 0 && !player.GetDead() && player.GetDirection() > 0 && !InputMouse.menu && !InputMouse.tuto)
        {
            score += 5 * Time.deltaTime;
            t_Score.text = "" + Mathf.Floor(score);
        }
        if (player.GetDead())
        {
            if(PlayerPrefs.GetFloat("Record") < score)
            {
                PlayerPrefs.SetFloat("Record", score);
            }

        }
    }
}
