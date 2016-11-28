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
    private string myUpgrades;
    private string activedperso;
    public Sprite[] spites = new Sprite[2];
    private float value;
    private string upgrade;
    public Player player;
    private bool isEncriptionInitialized;
    int textsSkin;

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
             
      
        if (ZPlayerPrefs.HasKey("ws_myUpgrades"))
            myUpgrades = ZPlayerPrefs.GetString("ws_myUpgrades");

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
            if (verify.Equals("Hunter"))
            {
                if (ZPlayerPrefs.GetString("ws_activedperso") == verify)
                {
                     myPerson.sprite = person[1];
                    texts[0].text = verify + " Ative";
                }
                else texts[0].text = verify + " Acquired";
            }
            else  if (verify.Equals("Hippie"))
            {
                if (ZPlayerPrefs.GetString("ws_activedperso") == verify)
                {
                    myPerson.sprite = person[3];
                    texts[1].text = verify + " Ative";
                }
                else texts[1].text = verify + " Acquired";

            }
        }

	}
	
  

    public void Scene (string scene) {
        SceneManager.LoadScene(scene);;
    }
    void Update ()
    {
        moneyState.text = player.GetMoney().ToString();
   
        if (activedperso != null && activedperso.Equals ("Hunter") && textsSkin != 0) textsSkin = 0;
        else if (activedperso != null && activedperso.Equals("Hippie") && textsSkin != 1) textsSkin = 1;
     

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
        upgrade = upgrades;
    }
	public void BuyAndSelect(GameObject texts)
    {
        Text text = texts.GetComponent<Text>();
		bool iHave = false;
        if (myUpgrades != null && myUpgrades != "")
        {
            string[] acquiredUp;
            acquiredUp = myUpgrades.Split('|');
            foreach (string verify in acquiredUp)
            {
                if (verify.Equals(upgrade))
                {
                    iHave = true;
                }
            }
        }
        if (iHave)
        {
            if (text.text.Equals(upgrade + " Ative") && upgrade != "Hell")
            {
                text.text = upgrade + " Acquired";
                DesactivePerson();
                ZPlayerPrefs.SetString("ws_activedperso", "");

            }
            else
            {
                ActiveUpgrade(text);
            }
        }
        else if (player.GetMoney() >= value)
        {
            myUpgrades += "|" + upgrade;
            ActiveUpgrade(text);
            SumCoin(int.Parse((value * -1).ToString()));
            ZPlayerPrefs.SetString("ws_myUpgrades", myUpgrades);
            Debug.Log("oi");
        }
	}
    void ActiveUpgrade(Text text)
    {
        if (activedperso != upgrade) texts[textsSkin].text = activedperso + " Acquired";
        text.text = upgrade + " Ative";
        if (upgrade.Equals("Hunter"))
        {
            myPerson.sprite = person[1];
            activedperso = upgrade;
            Debug.Log("oi1");
            ZPlayerPrefs.SetString("ws_activedperso", activedperso);

        }
        if (upgrade.Equals("Mage"))
        {
            myPerson.sprite = person[2];
            activedperso = upgrade;
            ZPlayerPrefs.SetString("ws_activedperso", activedperso);
            Debug.Log("oi2");
        }
        if (upgrade.Equals("Hippie"))
        {
            activedperso = upgrade;
            myPerson.sprite = person[3];
            ZPlayerPrefs.SetString("ws_activedperso", activedperso);
            Debug.Log("oi3");
        }

 
    }
    void DesactivePerson()
    {
        myPerson.sprite = person[0];
    }
   
}