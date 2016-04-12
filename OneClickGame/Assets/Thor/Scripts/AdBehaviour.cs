using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class AdBehaviour : MonoBehaviour
{
    [SerializeField] string gameID = "1056811";

    public static int extraLives;
    public static bool canIGetReward;
    private bool maxlimit;

    private int timeNow;
    private int lastTime;

    public static string zone;

    public GameObject sand;

    void Awake()
    {
        Advertisement.Initialize(gameID,true);
    }

    void Update()
    {
        MaxLifes();

        timeNow = System.DateTime.Now.DayOfYear;

        Debug.Log(timeNow);
        Debug.Log(lastTime);
        Debug.Log(extraLives);

        

    }

    void Start()
    {
        zone = "rewardedVideo";
        maxlimit = false;
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
                extraLives += 1;
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

    void MaxLifes()
    {
        if (extraLives < 3 && maxlimit == false)
        {
            zone = "rewardedVideo";
        }

        if (extraLives == 3)
        {
            maxlimit = true;
            PlayerPrefs.SetInt("lastTime",timeNow);
            zone = "skipableInstant";

            WaitAndDelay();

            if (lastTime != timeNow && maxlimit)
            {
                PlayerPrefs.SetInt("lastTime", 0);
                maxlimit = false;
            }
        }

        
    }

    IEnumerator WaitAndDelay()
    {
        //Delays
        yield return new WaitForSeconds(1);
    }

}