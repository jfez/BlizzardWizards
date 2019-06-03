using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int attackDamage = 4;

    GameObject player;
    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;

    bool playerInRange;

    Animator anim;

    private bool attacking;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        zombieHealth = GetComponent<ZombieHealth>();

        anim = GetComponent<Animator>();
        attacking = false;
    }

    public void Attack() {
        if (playerHealth.currentHealth > 0) {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = true;
            attacking = true;
            anim.SetBool("Attacking", attacking);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) {
            playerInRange = false;
            attacking = false;
            anim.SetBool("Attacking", attacking);
        }
    }
}
