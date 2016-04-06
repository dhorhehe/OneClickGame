using UnityEngine;
using System.Collections;

public class SandMovement : MonoBehaviour
{
    private float Speed;
    public float MaxXPos;

	void Start ()
	{
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
        transform.position = new Vector2(transform.position.x + 12.8f * 2, transform.position.y);
    }
}
