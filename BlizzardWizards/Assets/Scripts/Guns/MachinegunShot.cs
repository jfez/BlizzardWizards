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

    float timer;
    bool burst;
    AudioSource pium;

    void Awake()
    {
        pium = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            burst = !burst;
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
            }
        }
    }

    IEnumerator burstShoot()
    {
        pium.Play();
        timer = 0f;
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(timeBetweenBurstBullets);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(timeBetweenBurstBullets);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
