using UnityEngine;
using System.Collections;

public class FishHook : MonoBehaviour
{
    public float StartPosY;
    public float EndPosY;
    public float Speed;

    bool moveDown = true;

    private GameObject player;

	void Start ()
	{
        player = GameObject.Find("Player");
	    EndPosY = EndPosY + Random.Range(0f, 1f);
	}
	
	void Update () 
    {
	    if (!player.GetComponent<PlayerBehaviour>().gameOver)
	    {
            MoveControl();
	    }
	   
	}

    void MoveControl()
    {
        if (transform.position.y > EndPosY && moveDown)
        {
            transform.Translate(Vector2.down * Time.deltaTime * Speed);
        }
        else if (transform.position.y < EndPosY)
        {
            transform.position = new Vector2(transform.position.x, EndPosY);
            moveDown = false;
        }
    }
}
