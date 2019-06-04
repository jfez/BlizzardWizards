using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : MonoBehaviour
{
    public GameObject pelletPrefab;

    public int numPellets;
    public float timeBetweenShots;

    public AudioSource reloading;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading;

    public Animator animator;

    float timer;
    AudioSource zasca;

    void Awake()
    {
        zasca = GetComponent<AudioSource>();

        numPellets = 10;
        timeBetweenShots = 0.5f;
        maxAmmo = 4;

        reloadTime = 1.5f;
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

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots && !GameManager.instance.pause) {
            timer = 0f;
            currentAmmo--;
            zasca.Play();

            for (int i = 0; i < numPellets; i++) {
                Instantiate(pelletPrefab, transform.position, transform.rotation);
            }
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
