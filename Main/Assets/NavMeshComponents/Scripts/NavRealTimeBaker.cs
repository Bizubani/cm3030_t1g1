using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRealTimeBaker : MonoBehaviour
{
    // Start is called before the 
    [SerializeField]
    NavMeshSurface[] navMeshBlocks;

    void Start()
    {
        for(int i = 0; i < navMeshBlocks.Length; i++)
        {
            navMeshBlocks[i].BuildNavMesh();
        }
    }
}
