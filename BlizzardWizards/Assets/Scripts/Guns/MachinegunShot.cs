using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunShot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;

    public float timeBetweenBurst = 5f;
    public float timeBetweenBurstBullets = 0.5f;

    public AudioSource relaoding;

    public int maxAmmo = 8;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    float timer;
    bool burst;
    AudioSource pium;

    void Awake()
    {
        pium = GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            burst = !burst;
        }

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StopCoroutine(burstShoot());
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (burst && timer >= timeBetweenBurst)
            {
                StartCoroutine(burstShoot());
            }
            else if (!burst && timer >= timeBetweenBullets)
            {
                pium.Play();
                Instantiate(bulletPrefab, transform.position, transform.rotation);
                timer = 0f;
                currentAmmo--;
            }
        }
    }

    IEnumerator burstShoot()
    {
        pium.Play();
        timer = 0f;
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        currentAmmo--;
        yield return new WaitForSeconds(timeBetweenBurstBullets);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        currentAmmo--;
        yield return new WaitForSeconds(timeBetweenBurstBullets);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        relaoding.Play();
        isReloading = true;
        animator.SetBool("IsReloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("IsReloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
