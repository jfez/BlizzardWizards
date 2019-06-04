using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShot : MonoBehaviour
{
    public int damagePerShot;
    public float timeBetweenBullets;
    public float range;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTimef;
    private bool isReloading;
    private float reloadTime;

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

        damagePerShot = 20;
        timeBetweenBullets = 0.15f;
        range = 100f;

        maxAmmo = 8;
    
        reloadTime = 1f;
        isReloading = false;
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

        else if (currentAmmo < maxAmmo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
                return;
            }
        }

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && !GameManager.instance.pause)
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
