using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 4;

    GameObject player;
    PlayerHealth playerHealth;

    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange) {
            Attack();
        }
    }

    private void Attack() {
        timer = 0f;

        if (playerHealth.currentHealth > 0) {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = false;
        }
    }
}
