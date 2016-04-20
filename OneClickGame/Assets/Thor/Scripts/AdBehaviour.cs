using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class AdBehaviour : MonoBehaviour
{
    [SerializeField] string gameID = "1056811";

    public int extraLives;
    public static bool canIGetReward;
    private bool maxlimit;

    private int timeNow;
    public int lastTime;

    public static string zone;

    public GameObject sand;
    public GameObject CheckBox;
    public GameObject SpeakingBox1;
    public GameObject SpeakingBox2;
    public GameObject PopUp1;
    public GameObject PopUp2;
    public GameObject PopFade;

    //Daniel Variabler
    public int DailyLimit;
    private int EkstraLives;

    void Awake()
    {
        Advertisement.Initialize(gameID,true);

        //PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        //MaxLifes();



        timeNow = System.DateTime.Now.Day;
    }

    void Start()
    {
        zone = "rewardedVideo";
        maxlimit = false;

        lastTime = PlayerPrefs.GetInt("lastTime", 0);

    }

    public void ShowAd()
    {

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
          Advertisement.Show(zone, options);
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
            {
                if (zone == "rewardedVideo")
                {
                    EkstraLives++;
                    Debug.Log("EKSTRA LIVES: " + EkstraLives);
                    PlayerPrefs.SetInt("EkstraLives", EkstraLives);

                    CheckBox.SetActive(true);
                    SpeakingBox1.SetActive(true);
                    SpeakingBox2.SetActive(false);
                }

                break;
            }

            case ShowResult.Skipped:
            {
                Debug.Log("Ad skipped");
                break;
            }

            case ShowResult.Failed:
            {
                Debug.Log("Ad failed to load or some shit");
                break;
            }
        }
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
            zone = "rewardedVideo";
        }

        //Hvis der er spillet under 3 ads, tilføj en til AdsPlayedToday og en til EkstraLives
        if (AdsPlayedToday < DailyLimit && EkstraLives < DailyLimit)
        {
            AdsPlayedToday++;

            int DontShow = PlayerPrefs.GetInt("DontShow");
            if (DontShow == 1)
            {
                ShowAd();
            }
            else
            {
                PopFade.SetActive(true);
                PopUp2.SetActive(true);
            }
            



            Debug.Log("PLAYED AD");

        }
        //Hvis dailylimit er nået
        else if (AdsPlayedToday >= DailyLimit)
        {
            Debug.Log("ERROR - Played All Ads Today");
            
            PopFade.SetActive(true);
            PopUp1.SetActive(true);

        }

        Debug.Log(zone);

        //Sætter playerprefs
        PlayerPrefs.SetInt("AdsPlayedToday", AdsPlayedToday);
        PlayerPrefs.SetInt("Date", Date);
    }

    public void WatchSupportAd()
    {
        zone = "skipableInstant";
        ShowAd();
        PopFade.SetActive(false);
        PopUp1.SetActive(false);
    }

    public void RemovePopUp()
    {
        PopFade.SetActive(false);
        PopUp1.SetActive(false);
    }

    public void PlayAdAfterInfo(bool DontShow)
    {
        PopFade.SetActive(false);
        PopUp2.SetActive(false);
        ShowAd();

        if (DontShow)
        {
            PlayerPrefs.SetInt("DontShow", 1);
        }
    }
}