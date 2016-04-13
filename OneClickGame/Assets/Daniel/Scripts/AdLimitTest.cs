using System;
using UnityEngine;
using System.Collections;

public class AdLimitTest : MonoBehaviour
{
    public int DailyLimit;
    private int EkstraLives;

	void Start () 
    {
	    //PlayerPrefs.DeleteAll();
	}
	
	void Update () 
    {
        Debug.Log("EKSTRA LIVES: " + EkstraLives);
	}

    public void PlayAd()
    {
        int AdsPlayedToday = PlayerPrefs.GetInt("AdsPlayedToday");
        int Date = System.DateTime.Now.DayOfYear;
        EkstraLives = PlayerPrefs.GetInt("EkstraLives");

        //Tjekker om ny dag, og resetter AdsPlayedToday
        if (PlayerPrefs.GetInt("Date") != Date)
        {
            AdsPlayedToday = 0;
            Debug.Log("RESET");
        }

        //Hvis der er spillet under 3 ads, tilføj en til AdsPlayedToday og en til EkstraLives
        if (AdsPlayedToday < DailyLimit && EkstraLives < DailyLimit)
        {
            AdsPlayedToday ++;
            EkstraLives++;

            Debug.Log("PLAYED AD");
        }
        //Hvis dailylimit er nået
        else if (AdsPlayedToday >= DailyLimit)
        {
            Debug.Log("ERROR - Played All Ads Today");
        }

        //Sætter playerprefs
        PlayerPrefs.SetInt("AdsPlayedToday", AdsPlayedToday);
        PlayerPrefs.SetInt("EkstraLives", EkstraLives);
        PlayerPrefs.SetInt("Date", Date);
    }
}
