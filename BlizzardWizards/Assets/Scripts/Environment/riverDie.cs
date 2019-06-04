using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riverDie : MonoBehaviour
{
    public Transform newPosition;

    private GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.position = newPosition.position;
            if(gameObject.tag != "Volcano")
            {
                playerHealth.TakeDamage(70);
            }
            
        }
    }
}
