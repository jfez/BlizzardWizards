﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMachine : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    private bool inMachine;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        inMachine = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inMachine)
        {
            playerHealth.currentShield = 100;
            
        }
    }

    

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player)
        {
            inMachine = true;
            
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject == player)
        {
            inMachine = false;
            
        }
    }

}
