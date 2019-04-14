using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        transform.position = target.position;
        Vector3 offset2 = new Vector3(0.05f, 18.1f, -22);
        transform.position += offset2;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); //lerp es como una transición entre la posición de 2 vectores en un tiempo x

    }
}
