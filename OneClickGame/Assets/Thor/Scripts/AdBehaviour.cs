using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;


public class AdBehaviour : MonoBehaviour
{
    [SerializeField] string gameID = "1056811";

    public static int extraLives;
    public static bool canIGetReward;

    public static string zone;

    void Awake()
    {
        Advertisement.Initialize(gameID,true);
    }

    void Start()
    {
        zone = "rewardedVideo";
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
                Debug.Log("Get Extra Lives");
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





}