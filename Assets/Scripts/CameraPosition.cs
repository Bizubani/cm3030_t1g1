using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    Vector3 offset;
    [SerializeField] GameObject robotPlayer;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - robotPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = robotPlayer.transform.position + offset;
    }
}
