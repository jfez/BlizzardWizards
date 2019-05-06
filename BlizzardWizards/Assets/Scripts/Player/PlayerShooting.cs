using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q)) {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets) {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }
    }

    public void DisableEffects() {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot() {
        timer = 0f;
        gunLight.enabled = true;
        gunLine.enabled = true;

        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            ZombieHealth zombieHealth = shootHit.collider.GetComponent<ZombieHealth>();

            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damagePerShot);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
