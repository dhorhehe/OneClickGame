using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    public GameObject PlayerSprite;

    //Rigidbody
    private Rigidbody2D rb;

    //Ints and floats
    private int move;
    public float speedOfPlayer;
    public float maxSpeed;
    public float rotationSpeed;
    public int score;
    public int highScore;
    public float ScoreDelay;
    public float DeadSpeed;

    //Gameobjects
    private GameObject spawnPoint;
    private GameObject scoreText;
    private GameObject scoreText2;
    public GameObject GameOverUI;

    //Bools
    public bool gameOver;
    
    
    //Sprites
    public Sprite DeadSprite;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	    move = 1;
	    gameOver = true;

        spawnPoint = GameObject.Find("SpawnPoint");
        scoreText = GameObject.Find("ScoreText");
        scoreText2 = GameObject.Find("ScoreText2");
        //gameOverUI = GameObject.Find("GameOverCanvas");

	    highScore = PlayerPrefs.GetInt("highScore", 0);

	    score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Rotation();
        Score();
        //Debug.Log(score);
        //Debug.Log(highScore);
        

        
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
            rb.velocity = new Vector2(0,rb.velocity.y);
            PlayerSprite.GetComponent<Animator>().enabled = false;
            PlayerSprite.GetComponent<SpriteRenderer>().sprite = DeadSprite;
            GameOverUI.SetActive(true);
            Dead();
        }
    }

    void Score()
    {
        

        if (ScoreDelay <= 0 && gameOver == false)
        {
                if (spawnPoint.GetComponent<ObstacleSpawner>().timer <= 0)
                {
                    score += 1;
                    scoreText.GetComponent<Text>().text = score.ToString();
                    scoreText2.GetComponent<Text>().text = score.ToString();
                }
        }
        else if (gameOver == false)
        {
            ScoreDelay -= Time.deltaTime;
        }
        
        Debug.Log(ScoreDelay);
        
        

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore",score);
            PlayerPrefs.Save();
        }


    }

    public void RestartControl()
    {

        SceneManager.LoadScene(0);

    }

    void Dead()
    {
        //for (float i = transform.position.y; i > 0; i--)
        if (transform.position.y > -4)
        {
            rb.velocity = new Vector2(0,-DeadSpeed);
        }
        else if (transform.position.y <= -4)
        {
            rb.velocity = new Vector2(0, 0);
        }



    }
}
