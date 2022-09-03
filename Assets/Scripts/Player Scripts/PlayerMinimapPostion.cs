using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimapPostion : MonoBehaviour
{
    public Transform playerTarget;
    public float ItemHeight;
    public bool randomizePosition;
    private float randomX = 0f;
    private float randomZ = 0f;

    private void Update()
    {
        transform.position = new Vector3(playerTarget.position.x + randomX,ItemHeight,playerTarget.position.z + randomZ);
    }

    public void GetRandomPosition()
    {
        if(randomizePosition)
        {
            randomX = Random.Range(-100f, 100f);
            randomZ = Random.Range(-100f, 100f);
        }
    }
}
