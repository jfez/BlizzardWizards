using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int startingShield = 100;
    public Slider healthSlider;
    public Slider shieldSlider;
    public Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    [HideInInspector]
    public int currentHealth;

    [HideInInspector]
    public int currentShield;

    bool isDead;
    bool damaged;

    private int rest;

    Animator anim;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    private float timerRestart;

    private void Awake()
    {
        currentHealth = startingHealth;
        currentShield = startingShield;
        anim = GetComponentInChildren<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        timerRestart = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            timerRestart += Time.deltaTime;
        }

        if (timerRestart >= 3)
        {
            RestartLevel();
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

        shieldSlider.value = currentShield;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        if (currentShield > amount)
        {
            currentShield -= amount;
        }

        else
        {
            rest = amount - currentShield;
            currentShield -= currentShield;
            currentHealth -= rest;
        }

        

        if (currentHealth <= 0 && !isDead) {
            Death();
            
        }
    }

    private void Death()
    {
        isDead = true;
        anim.SetTrigger("death");

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
