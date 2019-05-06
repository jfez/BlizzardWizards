using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    public int bulletDamage = 20;
    float bulletForce = 100f;
    Rigidbody bulletRigidbody;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.AddRelativeForce(Vector3.forward * bulletForce, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            ZombieHealth zombieHealth = collision.collider.GetComponent<ZombieHealth>();
            zombieHealth.TakeDamage(bulletDamage);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
