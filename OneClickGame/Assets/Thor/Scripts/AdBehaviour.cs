using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;


public class AdBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Advertisement.Initialize("1056811",true);

	    StartCoroutine(ShowAdWhenReady());

    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Show();
        
    }
}
