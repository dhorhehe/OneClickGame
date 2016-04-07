using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject Bubble;

    float timer;
    
    public float CoolDownMin;
    public float CoolDownMax;
    float coolDown;

    public int MinBubbles;
    public int MaxBubbles;

	void Start ()
	{
	    coolDown = Random.Range(CoolDownMin, CoolDownMax);
	}
	
	void Update () 
    {
	    SpawnControl();
	}

    void SpawnControl()
    {
        timer += Time.deltaTime;

        if (timer > coolDown)
        {
            int randomSpawn = Random.Range(0, SpawnPoints.Length);
            int amount = Random.Range(MinBubbles, MaxBubbles);

            for (int i = 0; i < amount; i++)
            {
                Instantiate(Bubble, new Vector2(SpawnPoints[randomSpawn].transform.position.x + Random.Range(-0.5f,0.5f), SpawnPoints[randomSpawn].transform.position.y), Quaternion.identity);
            }

            coolDown = Random.Range(CoolDownMin, CoolDownMax);

            timer = 0;
        }
    }
}
