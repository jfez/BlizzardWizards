using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levantamiento : MonoBehaviour
{
    public GameObject PuenteDerecha;
    public GameObject PuenteIzquierda;
    public GameObject colliderPuente;
    public int speed;
    public Text buyText;

    bool dentro;
    bool rotar;
    Vector3 rotacion; 
    private AudioSource open;

    // Start is called before the first frame update
    void Start()
    {
        dentro = false;
        rotar = false;
        rotacion = new Vector3(0, 0, -35);
        open = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("return") && dentro && ScoreManager.score >= 0 || rotar)  && PuenteDerecha.transform.rotation.z < 0)
        {
            PuenteDerecha.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            PuenteIzquierda.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            

            if (!rotar)
            {
                rotar = true;
                Destroy(colliderPuente);
                ScoreManager.score -= 100;
                open.Play();
                buyText.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dentro = true;
            buyText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.name == "Player")
        {
            dentro = false;
            buyText.gameObject.SetActive(false);
        }
    }
}
