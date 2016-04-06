using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    private int move;

    private Rigidbody2D rb;

    public float speedOfPlayer;
    public float maxSpeed;
    public float rotationSpeed;

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
        Rotation();
	}

    void Movement ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move += 1;
        }

        if (move%2==0 && rb.velocity.y < maxSpeed)
        {
           rb.AddForce(new Vector2(0, speedOfPlayer) *Time.deltaTime);
        }
        if (move%2==1 && rb.velocity.y > -maxSpeed)
        {
            rb.AddForce(new Vector2(0, -speedOfPlayer) *Time.deltaTime);
        }

        
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rb.velocity.y*rotationSpeed);
    }
}
