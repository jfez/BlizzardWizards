using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class openDoor : MonoBehaviour
{
    public GameObject vanishPrefab;
    public Transform vanishParticleSystemPoint;

    public Image buyText;

    public AudioSource open;
    public AudioSource deny;
    public GameObject mesh;
    
    

    //public GameObject surfaceObject;

    //private NavMeshSurface surface;
    private bool inside;
    private GameObject player;
    private ParticleSystem vanishParticles;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
        player = GameObject.FindGameObjectWithTag("Player");

        vanishParticles = Instantiate(vanishPrefab).GetComponent<ParticleSystem>();

        //open = GetComponentInParent<AudioSource>();

        //surfaceObject = GameObject.FindWithTag("Surface");
        //surface = surfaceObject.GetComponentInChildren<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.Return) && ScoreManager.score >= 100)
        {
            open.Play();
            
            //print(open);

            gameObject.SetActive(false);
            if (mesh != null)
            {
                mesh.SetActive(false);
            }
            

            vanishParticles.transform.position = vanishParticleSystemPoint.position + (Vector3.up * 10);
            
            vanishParticles.Play();

            buyText.gameObject.SetActive(false);
            ScoreManager.score -= 100;

            Destroy(transform.parent.gameObject, 2f);


            //surface.BuildNavMesh();
            //surfaceObject.SetActive(true);

        }

        else if (inside && Input.GetKeyDown(KeyCode.Return) && ScoreManager.score < 100)
        {
            deny.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = true;
            buyText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = false;
            buyText.gameObject.SetActive(false);
        }
    }
}
