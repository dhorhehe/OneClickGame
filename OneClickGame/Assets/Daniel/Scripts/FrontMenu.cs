using UnityEngine;
using System.Collections;

public class FrontMenu : MonoBehaviour
{
    bool menuShowing = true;

    public GameObject[] frontMenues;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	    MenuControl();
	}

    void MenuControl()
    {
        if (menuShowing && Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject g in frontMenues)
            {
                g.gameObject.SetActive(false);
            }

            menuShowing = false;
        }
    }
}
