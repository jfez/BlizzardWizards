using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShot : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    public int maxAmmo = 8;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public AudioSource reloading;

    public Animator animator;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    AudioSource beep;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

        beep = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("IsReloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }
        
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        beep.Play();
        timer = 0f;
        currentAmmo--;
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
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    IEnumerator Reload()
    {
        reloading.Play();
        isReloading = true;
        animator.SetBool("IsReloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("IsReloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
