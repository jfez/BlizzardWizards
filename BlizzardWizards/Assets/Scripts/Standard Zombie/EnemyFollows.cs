using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollows : MonoBehaviour
{
    GameObject player;

    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;

    private float distMin;
    private Vector3 distance;

    private bool moving;

    Rigidbody ZombieRigidbody;

    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        distMin = 5f;
        anim = GetComponent<Animator>();

        moving = false;



    }


    void Update()
    {
        if (!moving)
        {
            moving = true;
        }

        if (nav.enabled) nav.SetDestination(player.transform.position);
        anim.SetBool("IsMoving", moving);


        /*anim.SetBool("IsMoving", moving);

        distance = player.transform.position - transform.position;
        if (distance.magnitude < distMin && goingHome == false && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            moving = true;
            
        }

        else if ((distance.magnitude >= distMin && goingHome == false) || enemyHealth.currentHealth < 0 || playerHealth.currentHealth < 0)   //podría cambiarse el playerHealth.currentHealth por el bool de isDead
        {
            nav.enabled = false;
            moving = false;
        }*/


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            nav.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            nav.enabled = true;
        }
    }
}
