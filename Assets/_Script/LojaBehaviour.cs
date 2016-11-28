using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LojaBehaviour : MonoBehaviour {

	public Text moneyState;
    public SpriteRenderer myPerson;
    public Sprite[] person;
    public Text[] texts;
    public GameObject menu;
    private string mySkins;
    private string activedperso;
    private float value;
    private string skinSelected;
    public Player player;
    private bool isEncriptionInitialized;
    int textsSkin;
    [SerializeField]
    private int[] valuesSkins;

    void Awake()
    {
        InicializeEncryotion();

    }
    void InicializeEncryotion()
    {
        if (!isEncriptionInitialized)
        {
            if (!PlayerPrefs.HasKey("ws_id"))
            {
                PlayerPrefs.SetString("ws_id", Random.Range(1000000, 999999).ToString());
                ZPlayerPrefs.Initialize("CHAVE_DE_ENCRIPTACAO", "Sd" + PlayerPrefs.GetString("ws_id") + "ds");
            }
        }
    }
    void Start()
    {
        GetDada();
    }
    void GetDada()
    {
        if (PlayerPrefs.HasKey("ws_activedperso"))
            activedperso = PlayerPrefs.GetString("ws_activedperso");
        else
            activedperso = null;

        if (ZPlayerPrefs.HasKey("ws_myUpgrades"))
            mySkins = ZPlayerPrefs.GetString("ws_myUpgrades");
        else
            mySkins = null;

        if (ZPlayerPrefs.HasKey("ws_money"))
            player.SetMoney(ZPlayerPrefs.GetInt("ws_money"));
        else
        {
            player.SetMoney(10000); Debug.Log("Set Money Teste");
        }
        string[] acquiredUp;
        acquiredUp = ZPlayerPrefs.GetString("ws_myUpgrades").Split('|');
        foreach (string verify in acquiredUp)
        {
            if (verify.Equals("0"))
            {
                if (PlayerPrefs.GetString("ws_activedperso") == verify)
                {
                     myPerson.sprite = person[1];
                    texts[0].text = "Ative";
                }
                else texts[0].text = "Acquired";
            }
            else  if (verify.Equals("2"))
            {
                texts[0].text = valuesSkins[0].ToString();
                if (PlayerPrefs.GetString("ws_activedperso") == verify)
                {
                    myPerson.sprite = person[3];
                    texts[1].text = "Ative";
                }
                else texts[1].text = "Acquired";
            }
            else
            {
                texts[0].text = valuesSkins[0].ToString();
                texts[1].text = valuesSkins[1].ToString();
            }
        }
	}
	
  

    public void Scene (string scene) {
        SceneManager.LoadScene(scene);
    }
    void Update ()
    {
        moneyState.text = player.GetMoney().ToString();
   
        if (activedperso != null && activedperso.Equals ("0") && textsSkin != 0) textsSkin = 0;
        else if (activedperso != null && activedperso.Equals("2") && textsSkin != 2) textsSkin = 2;

    }

    public void ResetarDados()
    {
        PlayerPrefs.DeleteAll();
        GetDada();
    }
    public void SumCoin(int value)
	{
        player.AddMoney(value);
        if (player.GetMoney() > 1000000)
            moneyState.text = ((Mathf.Floor(player.GetMoney() / 100000)) / 10).ToString() + "M";
        else if (player.GetMoney() > 10000)
            moneyState.text = ((Mathf.Floor(player.GetMoney() / 100)) / 10).ToString() + "K";
        else
            moneyState.text = player.GetMoney().ToString();
        ZPlayerPrefs.SetInt("ws_money", int.Parse(player.GetMoney().ToString()));
	}
  
    public void SetValue(float values)
    {
        value = values;
    }
    public void SetUpgrade(string upgrades)
    {
        skinSelected = upgrades;
    }
	public void BuyAndSelect(GameObject texts)
    {
        Text text = texts.GetComponent<Text>();
		bool iHave = false;
        if (mySkins != null && mySkins != "")
        {
            string[] acquiredUp;
            acquiredUp = mySkins.Split('|');
            foreach (string verify in acquiredUp)
            {
                if (verify.Equals(skinSelected))
                {
                    iHave = true;
                }
            }
        }
        if (iHave)
        {
            if (text.text.Equals("Ative"))
            {
                text.text = "Acquired";
                DesactivePerson();
                PlayerPrefs.SetString("ws_activedperso", "");
            }
            else
            {
                ActiveUpgrade(text);
            }
        }
        else if (player.GetMoney() >= value)
        {
            mySkins += "|" + skinSelected;
            ActiveUpgrade(text);
            SumCoin(int.Parse((value * -1).ToString()));
            ZPlayerPrefs.SetString("ws_myUpgrades", mySkins);
            Debug.Log("oi");
        }
	}
    void ActiveUpgrade(Text text)
    {
        if (activedperso != skinSelected && texts[textsSkin].text.Equals("Ative")) texts[textsSkin].text =  "Acquired";
        text.text = "Ative";
        if (skinSelected.Equals("0"))
        {
            myPerson.sprite = person[1];
            activedperso = skinSelected;
            Debug.Log("oi1");
            PlayerPrefs.SetString("ws_activedperso", activedperso);

        }
        if (skinSelected.Equals("1"))
        {
            activedperso = skinSelected;
            myPerson.sprite = person[2];
            PlayerPrefs.SetString("ws_activedperso", activedperso);
            Debug.Log("oi3");
        }
        if (skinSelected.Equals("2"))
        {
            myPerson.sprite = person[3];
            activedperso = skinSelected;
            PlayerPrefs.SetString("ws_activedperso", activedperso);
            Debug.Log("oi2");
        }
       

 
    }
    void DesactivePerson()
    {
        myPerson.sprite = person[0];
    }
   
}