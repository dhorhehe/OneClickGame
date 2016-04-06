using UnityEngine;
using System.Collections;

public class SandMovement : MonoBehaviour
{
    public float Speed;
    public float MaxXPos;

    public float ObjectLength;
    public int TotalObjects;

	void Start ()
	{
        if (Speed == 0)
	    Speed = GameObject.Find("SpawnPoint").GetComponent<ObstacleSpawner>().Speed;
	}
	
	void Update () 
    {
	    
	}

    void LateUpdate()
    {
        MovementControl();
    }

    void MovementControl()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);

        if (transform.position.x < MaxXPos)
        {
            ChangePosition();
        }
    }

    void ChangePosition()
    {
        transform.position = new Vector2(transform.position.x + ObjectLength * TotalObjects, transform.position.y);
    }
}
