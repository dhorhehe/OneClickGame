using UnityEngine;
using System.Collections;
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


        gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Enter name here...";
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(currentName);

	    newName = GetComponentInChildren<InputField>().text;
    }

    public void NameHandlerFunction()
    {
        if (currentName == null || currentName != newName)
        {
            PlayerPrefs.SetString("currentName", newName);
            //PlayerPrefs.SetString("newName",currentName);
            PlayerPrefs.Save();
        }
        Debug.Log("Gay");
    }
}
