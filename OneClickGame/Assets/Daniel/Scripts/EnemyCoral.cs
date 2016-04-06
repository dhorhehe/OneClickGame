using UnityEngine;
using System.Collections;

public class EnemyCoral : MonoBehaviour
{
    public float StartPosY;
    public float EndPosY;
    public float Speed;

    bool moveUp = true;

	void Start () 
    {
        transform.position = new Vector2(transform.position.x, StartPosY);
	}
	
	void Update () 
    {
	    MoveControl();
	}

    void MoveControl()
    {
        if (transform.position.y < EndPosY && moveUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * Speed);
        }
        else if (transform.position.y > EndPosY)
        {
            transform.position = new Vector2(transform.position.x, EndPosY);
            moveUp = false;
        }
    }
}
