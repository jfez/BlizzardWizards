using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaAttack : MonoBehaviour
{
    public GameObject explosionPrefab;
    public Transform particleSystemPoint;
    public AudioSource explosionAudio;
    public LayerMask playerMask;
    public LayerMask zombieMask;
    public int maxDamage = 20;
    public float explosionForce = 150f;
    public float explosionRadius = 20f;

    GameObject player;
    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;
    ParticleSystem explosionParticles;

    bool playerInRange;

    Animator anim;

    private bool attacking;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        zombieHealth = GetComponent<ZombieHealth>();
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();

        anim = GetComponent<Animator>();
        attacking = false;
    }

    public void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            // Find the player in the area around the hit
            Collider[] playerColliders = Physics.OverlapSphere(transform.position, explosionRadius, playerMask);

            for (int i = 0; i < playerColliders.Length; i++)
            {
                Rigidbody targetRigidbody = playerColliders[i].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

                if (!targetHealth)
                    continue;

                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage((int)damage);
            }

            // Find all the zombies in an area around the hit
            Collider[] zombieColliders = Physics.OverlapSphere(transform.position, explosionRadius, zombieMask);

            for (int i = 0; i < zombieColliders.Length; i++)
            {
                Rigidbody targetRigidbody = zombieColliders[i].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            explosionParticles.transform.position = particleSystemPoint.position;
            explosionParticles.Play();
            explosionAudio.Play();

        }
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            attacking = true;
            anim.SetBool("Attacking", attacking);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            attacking = false;
            anim.SetBool("Attacking", attacking);
        }
    }
}
