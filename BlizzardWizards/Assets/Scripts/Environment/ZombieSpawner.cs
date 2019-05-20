using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject standardZombie;

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
                Instantiate(standardZombie, point.position, point.rotation);
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
