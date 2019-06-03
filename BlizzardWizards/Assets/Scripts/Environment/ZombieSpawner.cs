using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject standardZombie;
    public GameObject gorillaZombie;
    public GameObject fastZombie;

    private float standardZombieProbability = 0.6f;
    private float gorillaZombieProbability = 1f;
    private float fastZombieProbability = 0.9f;

    private bool spawning = false;
    private float timeBetweenSpawn = 4f;

    private float timer;
    private Transform[] spawnPoints;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) gameManager = GameManager.instance;
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (spawning) {
            if (timer >= timeBetweenSpawn && gameManager.zombiesSpawnedInRound < gameManager.zombiesPerRound && gameManager.amountZombies < gameManager.maxZombies) {

                timer = 0f;

                Transform point = spawnPoints[Random.Range(1, spawnPoints.Length)];

                float random = Random.Range(0f, 1f);

                if (random <= standardZombieProbability)
                {
                    Instantiate(standardZombie, point.position, point.rotation);
                }
                else if (random <= fastZombieProbability)
                {
                    Instantiate(fastZombie, point.position, point.rotation);
                }
                else
                {
                    Instantiate(gorillaZombie, point.position, point.rotation);
                }

                gameManager.amountZombies++;
                gameManager.zombiesSpawnedInRound++;

            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            spawning = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            spawning = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
