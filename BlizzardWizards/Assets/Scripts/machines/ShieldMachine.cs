using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldMachine : MonoBehaviour
{
    public Text machineText;

    GameObject player;
    PlayerHealth playerHealth;

    private bool inMachine;
    private AudioSource drink;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        inMachine = false;

        drink = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inMachine && ScoreManager.score >= 50 && playerHealth.currentShield < 100)
        {
            playerHealth.currentShield = 100;
            ScoreManager.score -= 50;
            drink.Play();

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inMachine = true;
            machineText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inMachine = false;
            machineText.gameObject.SetActive(false);
        }
    }

}
