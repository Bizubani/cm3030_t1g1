using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    Vector3 offset;
    private Camera camera1;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = Camera.main;
        offset = camera1.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        camera1.transform.position = transform.position + offset;
    }
}
