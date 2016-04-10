using UnityEngine;
using System.Collections;

public class EnemyCoral : MonoBehaviour
{
    public float StartPosY;
    public float EndPosY;
    public float Speed;

    bool moveUp = true;

    private GameObject player;

	void Start () 
    {
        transform.position = new Vector2(transform.position.x, StartPosY);

        EndPosY = EndPosY - Random.Range(0f, 1f);

        player = GameObject.Find("Player");
	}
	
	void Update () 
    {
        if (!player.GetComponent<PlayerBehaviour>().gameOver)
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
