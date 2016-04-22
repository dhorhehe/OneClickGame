using UnityEngine;
using System.Collections;

public class MenuSound : MonoBehaviour
{
    private AudioSource ad;

	void Start ()
	{
	    ad = GetComponent<AudioSource>();
	}
	
	void Update () 
    {
	
	}

    public void PlayClick()
    {
        if (!MuteButtonBehaviour.mute)
        {
            ad.Play();
        }
    }
}
