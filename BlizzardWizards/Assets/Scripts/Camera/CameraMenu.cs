using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    private float timer;
    private bool paneo;
    private float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        paneo = false;
        offset = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        offset = offset - 0.01f;
        

        if (timer > 5)
        {
            paneo = !paneo;
            timer = 0;
            offset = 1;
        }


        if (!paneo)
        {
           

            transform.Translate(Vector3.forward * Time.deltaTime * offset);


        }

        else
        {
            

            transform.Translate(-Vector3.forward * Time.deltaTime * offset);
        }
        
    }
}
