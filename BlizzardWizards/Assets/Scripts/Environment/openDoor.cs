using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public GameObject vanishPrefab;
    public Transform vanishParticleSystemPoint;

    private bool inside;
    private GameObject player;
    private ParticleSystem vanishParticles;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        player = GameObject.FindGameObjectWithTag("Player");

        vanishParticles = Instantiate(vanishPrefab).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(transform.parent.gameObject);
            vanishParticles.transform.position = vanishParticleSystemPoint.position + (Vector3.up * 10);
            
            vanishParticles.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = false;
        }
    }
}
