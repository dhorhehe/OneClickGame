using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public float Speed;
    public float CoolDown;

    private int lastSpawn;
    private int spawnInRow = 1;

    public float timer;
    public GameObject[] ObstacleList;

    GameObject player;

	void Start () 
    {
        GameObject newest = Instantiate(ObstacleList[Random.Range(0, ObstacleList.Length)], transform.position, Quaternion.identity) as GameObject;

        newest.GetComponent<ObstacleMovement>().Speed = Speed;

        player = GameObject.Find("Player");
	}
	
	void Update () 
    {
        if (!player.GetComponent<PlayerBehaviour>().gameOver)
	    SpawnControl();
	}

    void SpawnControl()
    {
        timer += Time.deltaTime;

        if (timer > CoolDown)
        {
            int obstacleToSpawn = Random.Range(0, ObstacleList.Length);

            //Debug.Log(obstacleToSpawn);

            if (obstacleToSpawn == lastSpawn)
            {
                spawnInRow++;
            }
            else
            {
                spawnInRow = 1;
            }


            if (spawnInRow > 2)
            {
                if (obstacleToSpawn == 0)
                {
                    obstacleToSpawn = 1;
                }
                else if (obstacleToSpawn == 1)
                {
                    obstacleToSpawn = 0;
                }

                spawnInRow = 1;
            }

            GameObject newest = Instantiate(ObstacleList[obstacleToSpawn], transform.position, Quaternion.identity) as GameObject;

            newest.GetComponent<ObstacleMovement>().Speed = Speed;

            timer = 0;

            lastSpawn = obstacleToSpawn;


        }
    }
}
