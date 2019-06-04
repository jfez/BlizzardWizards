using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : MonoBehaviour
{
    public GameObject pelletPrefab;

    public int numPellets = 10;
    public float timeBetweenShots = 0.5f;

    public AudioSource reloading;

    public int maxAmmo = 8;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    float timer;
    AudioSource zasca;

    void Awake()
    {
        zasca = GetComponent<AudioSource>();
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

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots) {
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
