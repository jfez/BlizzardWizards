using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class updateNavMesh : MonoBehaviour
{
    private NavMeshSurface surface;

    // Start is called before the first frame update
    void Awake()
    {
        surface = GetComponentInChildren<NavMeshSurface>();
    }

    void OnEnable()
    {
        surface.BuildNavMesh();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
