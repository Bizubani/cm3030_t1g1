using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimapPostion : MonoBehaviour
{
    public Transform playerTarget;
    public float ItemHeight;

    private void Update()
    {
        transform.position = new Vector3(playerTarget.position.x,ItemHeight,playerTarget.position.z);;
    }
}
