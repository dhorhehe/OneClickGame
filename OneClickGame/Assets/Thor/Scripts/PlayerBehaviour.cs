using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    

    //Rigidbody
    private Rigidbody2D rb;

    //Ints and floats
    private int move;
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

    void FixedUpdate()
    {
        Collision();
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

    void Collision()
    {
        //RaycastHit2D hitX = Physics2D.Raycast(transform.position, Vector2.right * 0.55f);
        RaycastHit2D hitY = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-0.4f), Vector2.up,0.9f);

        

        if (hitY.collider != null)
        {
            Debug.Log(hitY.collider);
        }
    }
}
