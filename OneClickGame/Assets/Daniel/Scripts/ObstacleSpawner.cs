using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public float Speed;
    public float CoolDown;

    float timer;
    public GameObject[] ObstacleList;

	void Start () 
    {
        GameObject newest = Instantiate(ObstacleList[Random.Range(0, ObstacleList.Length)], transform.position, Quaternion.identity) as GameObject;

        newest.GetComponent<ObstacleMovement>().Speed = Speed;
	}
	
	void Update () 
    {
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
