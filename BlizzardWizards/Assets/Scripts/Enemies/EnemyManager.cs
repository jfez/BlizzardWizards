using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;



    private int counter;


    void Start()
    {
        counter = 0; ;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        if (enemy.tag == "Zombie")
        {
            ScoreManager.zombiesCounter++;
            Debug.Log(ScoreManager.zombiesCounter);

        }

        /*else if (enemy.tag == "Zombear")
        {
            ScoreManager.zombearsCounter++;

        }

        else if (enemy.tag == "Hellephant")
        {
            ScoreManager.hellephantsCounter++;

        }*/
    }
}
