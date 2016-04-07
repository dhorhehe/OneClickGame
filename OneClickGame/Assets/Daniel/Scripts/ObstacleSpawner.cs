using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public float Speed;
    public float CoolDown;

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
            GameObject newest = Instantiate(ObstacleList[Random.Range(0,ObstacleList.Length)], transform.position, Quaternion.identity) as GameObject;

            newest.GetComponent<ObstacleMovement>().Speed = Speed;

            timer = 0;
        }
    }
}
