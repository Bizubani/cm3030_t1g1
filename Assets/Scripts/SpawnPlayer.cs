using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public bool hasSpawned = false;
    // Update is called once per frame
    void Update()
    {
        if(!hasSpawned)
        {
            transform.position = playerSpawnPoint.position;
        }
    }
}
