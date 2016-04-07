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
    public int score;
    public int highScore;

    private GameObject spawnPoint;

    //Bools
    public bool gameOver;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	    move = 1;
	    gameOver = true;

        spawnPoint = GameObject.Find("SpawnPoint");

	    highScore = PlayerPrefs.GetInt("highScore", 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Rotation();
        Score();
        Debug.Log(score);
        Debug.Log(highScore);
	}

    void FixedUpdate()
    {
        Collision();
    }

    void Movement ()
    {
        if (Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                move += 1;
                gameOver = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            move += 1;
            gameOver = false;
        }
        

        if (gameOver == false)
        {
            if (move % 2 == 0 && rb.velocity.y < maxSpeed)
            {
                rb.AddForce(new Vector2(0, speedOfPlayer) * Time.deltaTime);
            }
            if (move % 2 == 1 && rb.velocity.y > -maxSpeed)
            {
                rb.AddForce(new Vector2(0, -speedOfPlayer) * Time.deltaTime);
            }
        }
        

        
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rb.velocity.y*rotationSpeed);
    }

    void Collision()
    {
        RaycastHit2D hitY = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y-0.4f), Vector2.up,0.9f);
        RaycastHit2D hitX = Physics2D.Raycast(new Vector2(transform.position.x-0.4f, transform.position.y), Vector2.right,0.9f);
        

        if (hitY.collider != null || hitX.collider != null)
        {
            gameOver = true;
            rb.velocity = new Vector2(0,0);
        }
    }

    void Score()
    {
        if (spawnPoint.GetComponent<ObstacleSpawner>().timer <= 0)
        {
            score += 1;
        }

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore",score);
            PlayerPrefs.Save();
        }


    }
}
