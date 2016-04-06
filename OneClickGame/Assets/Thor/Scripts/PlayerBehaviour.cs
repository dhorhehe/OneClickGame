using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    private int move;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	    move = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
	}

    void Movement ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move += 1;
        }

        if (move%2==0)
        {
           rb.AddForce(new Vector2(0, 0.1f) *Time.deltaTime);
        }
        if (move%2==1)
        {
            rb.AddForce(new Vector2(0, -0.1f) *Time.deltaTime);
        }


    }
}
