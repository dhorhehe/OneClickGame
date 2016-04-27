using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameHandler : MonoBehaviour
{

    private string newName;
    public static string currentName;


	// Use this for initialization
	void Start ()
	{
	    currentName = PlayerPrefs.GetString("currentName");
	    newName = PlayerPrefs.GetString("NewName");


        gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "ENTER NEW USERNAME...";
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(currentName);

	    newName = GetComponentInChildren<InputField>().text;
    }

    public void NameHandlerFunction()
    {
        if (currentName == null || currentName != newName)
        {
            ScoreEntry score = Highscores.GetBestByUser(newName);

            if (score == null)
            {
                PlayerPrefs.SetString("currentName", newName);
                PlayerPrefs.SetInt("PlayedOnce", 1);
                PlayerPrefs.Save();
                
                

                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("USERNAME TAKEN!");
            }
        }
    }
}
