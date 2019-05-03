using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    [HideInInspector]
    public int currentHealth;

    bool isDead;
    bool damaged;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead) {
            isDead = true;
        }
    }
}
