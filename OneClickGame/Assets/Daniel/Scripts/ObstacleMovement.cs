using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    public float Speed;
    public GameObject Player;

    public float startPosY;


	void Start () 
    {
	    if (gameObject.name == "EnemyCoral(Clone)")
	    {
            transform.position = new Vector2(transform.position.x, startPosY);
	    }
        else if (gameObject.name == "FishHook(Clone)")
	    {
            transform.position = new Vector2(transform.position.x, GetComponent<FishHook>().StartPosY);
	    }

        Player = GameObject.Find("Player");
	}
	
	void Update () 
    {
	    if (!Player.GetComponent<PlayerBehaviour>().gameOver)
	    {
            MovementControl();
	    }

	}

    void MovementControl()
    {
        transform.Translate(Vector3.left * Time.deltaTime * Speed);

        if (transform.position.x < -5)
        {
            Destroy(gameObject);
        }
    }
}
