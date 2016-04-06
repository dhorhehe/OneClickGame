using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    public float Speed;


	void Start () 
    {
	    if (gameObject.name == "EnemyCoral(Clone)")
	    {
            transform.position = new Vector2(transform.position.x, -3.42f);
	    }
        else if (gameObject.name == "FishHook(Clone)")
	    {
            transform.position = new Vector2(transform.position.x, GetComponent<FishHook>().StartPosY);
	    }
	}
	
	void Update () 
    {
	    MovementControl();
	}

    void MovementControl()
    {
        transform.Translate(Vector3.left * Time.deltaTime * Speed);
    }
}
