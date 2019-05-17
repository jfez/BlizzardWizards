using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float pelletSpeed = 100f;
    public int pelletDamage = 5;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = Random.Range(-0.2f, 0.2f);
        direction.y = Random.Range(-0.2f, 0.2f);
        direction.z = 0f;
        direction = (transform.forward + direction).normalized * pelletSpeed;
        Destroy(gameObject, 0.08f);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            zombieHealth.TakeDamage(pelletDamage);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
