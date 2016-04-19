using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButtonBehaviour : MonoBehaviour
{

    public static bool mute;

    private int click;

    private int playerPrefsClick;

    public Sprite[] muteButton;

    public GameObject player;

    

	// Use this for initialization
	void Start ()
	{
	    playerPrefsClick = PlayerPrefs.GetInt("playerPrefsClick",playerPrefsClick);
	    click = playerPrefsClick;

        if (click % 2 == 0)
        {
            mute = false;
            player.GetComponent<Button>().image.overrideSprite = muteButton[0];
        }
        if (click % 2 == 1)
        {
            mute = true;
            player.GetComponent<Button>().image.overrideSprite = muteButton[1];
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
    
     if (click > playerPrefsClick)
        {
        PlayerPrefs.SetInt("playerPrefsClick", click);
        PlayerPrefs.Save();
        }
	}

    public void MuteAllSounds ()
    {
        click++;

        if (click % 2 == 0)
        {
            mute = false;
            player.GetComponent<Button>().image.overrideSprite = muteButton[0];
        }
        if (click % 2 == 1)
        {
            mute = true;
            player.GetComponent<Button>().image.overrideSprite = muteButton[1];
        }

        
        
    }
}
