using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    private GameObject damagePrefab;
    public int startingHealth = 40;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioSource hurt_zombie;

    [HideInInspector]
    public int currentHealth;

    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    Animator anim;
    EnemyFollows enemyFollows;

    GameManager gameManager;

    private ParticleSystem damageParticles;

    private void Awake()
    {
        currentHealth = startingHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyFollows = GetComponent<EnemyFollows>();

        anim = GetComponent<Animator>();
        gameManager = GameManager.instance;

        damagePrefab = GameObject.FindGameObjectWithTag("Particles");

        damageParticles = Instantiate(damagePrefab).GetComponentInChildren<ParticleSystem>();
        
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

        damageParticles.transform.position = transform.position;

        damageParticles.Play();

        currentHealth -= amount;
        hurt_zombie.Play();

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

        gameManager.amountZombies--;
        gameManager.zombiesDeadInRound++;

        if (gameManager.zombiesDeadInRound == gameManager.zombiesPerRound)
        {
            gameManager.newRound();
        }
    }
}
