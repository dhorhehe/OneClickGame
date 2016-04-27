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
    public float scoreDelay;
    public float scoreTimer;
    public float DeadSpeed;

    //Gameobjects
    private GameObject spawnPoint;
    private GameObject scoreText;
    private GameObject scoreText2;
    public GameObject GameOverUI;
    public GameObject GameOverScore;
    public GameObject GameOverScore2;
    public GameObject GameOverHighScore;
    public GameObject GameOverHighScore2;
    public GameObject Camera;
    public GameObject ExtraLifeUI;
    public GameObject RankText;

    //Bools
    public bool gameOver;
    public bool firstStart;
    public bool extraHPUsed;
    private bool highScorePosted;
    
    
    //Sprites
    public Sprite DeadSprite;

    //Sound
    private AudioSource _audioSource;
    public AudioSource _audioSourceIGUI;

	// Use this for initialization
	void Start ()
    {
        //PlayerPrefs.SetInt("highScore", 0);

	    if (PlayerPrefs.GetInt("PlayedOnce") == 0)
	    {
	        SceneManager.LoadScene(1);
	    }

	    Debug.Log(PlayerPrefs.GetString("currentName"));

        rb = GetComponent<Rigidbody2D>();
	    move = 1;
	    gameOver = true;

	    _audioSource = gameObject.GetComponent<AudioSource>();
        spawnPoint = GameObject.Find("SpawnPoint");
        scoreText = GameObject.Find("ScoreText");
        scoreText2 = GameObject.Find("ScoreText2");
        //gameOverUI = GameObject.Find("GameOverCanvas");

	    highScore = PlayerPrefs.GetInt("highScore", 0);

	    score = 0;

        //PlayerPrefs.DeleteAll();

	    firstStart = true;

        //extraHPUsed = false;

	    int EkstraLives = PlayerPrefs.GetInt("EkstraLives");

        if (PlayerPrefs.GetInt("UseExtraLife") == 1 && EkstraLives != 0)
        {
            extraHPUsed = true;
            EkstraLives --;
        }
        else
        {
            extraHPUsed = false;
        }

        PlayerPrefs.SetInt("EkstraLives", EkstraLives);
	    
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Rotation();
        Score();
        //Debug.Log(extraHPUsed);
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
            if(Input.GetTouch(0).phase == TouchPhase.Began && firstStart == true)
            {
                if (MuteButtonBehaviour.mute == false)
                {
                    _audioSource.Play();
                }

                move += 1;
                gameOver = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && firstStart == true)
        {
            if (MuteButtonBehaviour.mute == false)
            {
                _audioSource.Play();
            }

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

        if (!extraHPUsed)
        {

            GetComponent<Collider2D>().enabled = false;

             if (hitY.collider != null && hitY.collider.name != "Player" || hitX.collider != null && hitX.collider.name != "Player")
             {
                    gameOver = true;
                    firstStart = false;
                    rb.velocity = new Vector2(0,rb.velocity.y);
                    PlayerSprite.GetComponent<Animator>().enabled = false;
                    PlayerSprite.GetComponent<SpriteRenderer>().sprite = DeadSprite;

                    if (scoreText.active == true)
                    {
                        Camera.GetComponent<CameraShakeScript>().shakeDuration = 0.1f;
                        
                        if (MuteButtonBehaviour.mute == false)
                        {
                            
                        }
                    }

                    scoreText.active = false;
                    scoreText2.active = false;

                    GameOverScore.GetComponent<Text>().text = "SCORE: " + score;
                    GameOverScore2.GetComponent<Text>().text = "SCORE: " + score;
                    GameOverHighScore.GetComponent<Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("highScore");
                    GameOverHighScore2.GetComponent<Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("highScore");

                    GameOverUI.SetActive(true);
                    
                    Dead();

                 PostHighscore();

             }
        }

        if (extraHPUsed)
        {
            RaycastHit2D hitY2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), Vector2.up, 0.09f);

            //Debug.DrawLine(new Vector2(transform.position.x, transform.position.y + 0.4f),new Vector2(transform.position.x, transform.position.y + 0.4f) + Vector2.up * 0.08f);

            GetComponent<Collider2D>().enabled = true;

            if (hitY.collider != null && hitY.collider.name != "Player" || hitY2.collider != null && hitY2.collider.name != "Player" || hitX.collider != null && hitX.collider.name != "Player")
            {
                Camera.GetComponent<CameraShakeScript>().shakeDuration = 0.1f;
                StartCoroutine(WaitAndDelay(0.7f));
                ExtraLifeUI.SetActive(true);
            }
        }

        
        

    }

    //Skal optimeres
    void Score()
    {
        if (scoreDelay <= 0 && gameOver == false)
        {


            if (scoreTimer <= 0 && gameOver == false)
            {
                    scoreTimer = 1;
                    score += 1;
                    scoreText.GetComponent<Text>().text = score.ToString();
                    scoreText2.GetComponent<Text>().text = score.ToString();

                    if (MuteButtonBehaviour.mute == false)
                    {
                        _audioSourceIGUI.Play();
                    }
                    
                }
            else if (scoreTimer >= 0)
            {
                scoreTimer -= Time.deltaTime;
            }


        }
        else if (scoreTimer >= 0 && gameOver == false)
        {
            scoreDelay -= Time.deltaTime;
        }

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore",score);
            PlayerPrefs.Save();
        }


    }

    void PostHighscore()
    {
        if (!highScorePosted)
        {
            StartCoroutine(Highscores.PostHighscore(PlayerPrefs.GetString("currentName"), score));


            ScoreEntry pos = Highscores.GetBestByUser(PlayerPrefs.GetString("currentName"));
            RankText.GetComponent<Text>().text = "YOUR RANK: " + pos.Position;

            Debug.Log("NEW HIGHSCORE");
            highScorePosted = true;
        }
    }

    public void RestartControl()
    {

        SceneManager.LoadScene(0);

    }

    void Dead()
    {
        if (transform.position.y > -4)
        {
            rb.velocity = new Vector2(0,-DeadSpeed);
        }
        else if (transform.position.y <= -4)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    IEnumerator WaitAndDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        extraHPUsed = false;
        PlayerPrefs.SetInt("UseExtraLife", 0);
    }

}
