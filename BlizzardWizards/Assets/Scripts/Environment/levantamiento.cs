using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levantamiento : MonoBehaviour
{
    public GameObject PuenteDerecha;
    public GameObject PuenteIzquierda;
    public int speed;

    bool dentro = false;
    bool rotar = false;
    Vector3 rotacion = new Vector3(0, 0, -35);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("return") && dentro || rotar)  && PuenteDerecha.transform.rotation.z < 0)
        {
            PuenteDerecha.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            PuenteIzquierda.transform.Rotate(Vector3.forward * speed * Time.deltaTime);

            if (!rotar) rotar = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dentro = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.name == "Player")
        {
            dentro = false;
        }
    }
}
