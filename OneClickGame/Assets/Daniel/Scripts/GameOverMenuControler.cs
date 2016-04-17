using UnityEngine;
using System.Collections;

public class GameOverMenuControler : MonoBehaviour
{
    public GameObject CheckBox;
    public GameObject CheckMark;
    public GameObject DontPlayAd;
    public GameObject SpeakingBox1;
    public GameObject SpeakingBox2;

	void Start () 
    {
	    CheckBoxStartControl();
	}

	void Update () 
    {
	
	}

    void CheckBoxStartControl()
    {
        //Tjekker om der overhovedet er nogle ekstra liv
        int EkstraLives = PlayerPrefs.GetInt("EkstraLives");

        Debug.Log(EkstraLives);

        if (EkstraLives != 0)
        {
            CheckBox.SetActive(true);
            SpeakingBox1.SetActive(true);
        }
        else
        {
            SpeakingBox2.SetActive(true);
        }

        //Tjekker om UseExtraLife er true

        int UseExtraLife = PlayerPrefs.GetInt("UseExtraLife");

        if (UseExtraLife == 1)
        {
            CheckMark.SetActive(true);
        }
        else if (UseExtraLife == 0)
        {
            DontPlayAd.SetActive(true);
        }
    }

    public void CheckBoxSwitch()
    {
        //Void brugt på knappen
        int UseExtraLife = PlayerPrefs.GetInt("UseExtraLife");
        int EkstraLives = PlayerPrefs.GetInt("EkstraLives");

        if (UseExtraLife == 1)
        {
            DontPlayAd.SetActive(true);
            CheckMark.SetActive(false);

            PlayerPrefs.SetInt("UseExtraLife", 0);
        }
        else if (UseExtraLife == 0)
        {
            DontPlayAd.SetActive(false);
            CheckMark.SetActive(true);
            PlayerPrefs.SetInt("UseExtraLife", 1);
        }
    }
}
