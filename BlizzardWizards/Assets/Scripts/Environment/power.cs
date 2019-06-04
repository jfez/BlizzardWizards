using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class power : MonoBehaviour
{
    public Image powerText;
    public AudioSource deny;

    GameObject player;
    //PlayerHealth playerHealth;

    private bool inZone;
    private AudioSource powerAudio;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();

        inZone = false;

        powerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inZone && ScoreManager.score >= 200 && !GameManager.instance.powerOn)
        {
            GameManager.instance.powerOn = true;
            ScoreManager.score -= 200;
            powerAudio.Play();

        }

        else if (Input.GetKeyDown(KeyCode.Return) && inZone && ScoreManager.score < 200 && !GameManager.instance.powerOn)
        {
            deny.Play();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inZone = true;
            powerText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inZone = false;
            powerText.gameObject.SetActive(false);
        }
    }
}
