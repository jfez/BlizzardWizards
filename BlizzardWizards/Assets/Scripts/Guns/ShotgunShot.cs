using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : MonoBehaviour
{
    public GameObject pelletPrefab;

    public int numPellets = 10;
    public float timeBetweenShots = 0.5f;

    float timer;
    AudioSource zasca;

    void Awake()
    {
        zasca = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots) {
            timer = 0f;
            zasca.Play();

            for (int i = 0; i < numPellets; i++) {
                Instantiate(pelletPrefab, transform.position, transform.rotation);
            }
        }
    }
}
