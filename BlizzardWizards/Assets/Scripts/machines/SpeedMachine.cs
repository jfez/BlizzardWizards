using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMachine : MonoBehaviour
{
    public Text machineText;

    GameObject player;
    PlayerMovement playerMovement;

    private bool inMachine;
    private float timer;
    private float maxTimePerk;
    private AudioSource drink;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        inMachine = false;

        timer = 0;
        maxTimePerk = 60;

        drink = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //print(timer);

        if (Input.GetKeyDown(KeyCode.Return) && inMachine && ScoreManager.score >= 50 && !playerMovement.speedPerk)
        {
            playerMovement.speedPerk = true;
            timer = 0;
            ScoreManager.score -= 50;
            drink.Play();
            //print(playerMovement.speedPerk);

        }

        if (timer > maxTimePerk)
        {
            playerMovement.speedPerk = false;
            //print(playerMovement.speedPerk);
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
