﻿using System.Collections;
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

    Animator anim;
    private EnemyFollows enemyFollows;

    private void Awake()
    {
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyFollows = GetComponent<EnemyFollows>();

        anim = GetComponent<Animator>();
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
        ScoreManager.score += 10;
        isDead = true;
        //Destroy(GetComponent<Rigidbody>());
        anim.SetTrigger("Death");
        ScoreManager.zombiesCounter--;
        gameObject.layer = LayerMask.NameToLayer("Default");
        capsuleCollider.isTrigger = true;
        //capsuleCollider.enabled = false;
        StartSinking();
    }

    void StartSinking() {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = false;
        

        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
