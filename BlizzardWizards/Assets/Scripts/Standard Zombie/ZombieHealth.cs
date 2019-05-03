using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int startingHealth = 40;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;

    [HideInInspector]
    public int currentHealth;

    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    private void Awake()
    {
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking) {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount) {
        if (isDead) {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0) {
            Death();
        }
    }

    void Death() {
        isDead = true;
        capsuleCollider.isTrigger = true;
        StartSinking();
    }

    void StartSinking() {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
