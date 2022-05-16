using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float seconds;

    // Start is called before the first frame update
    void Start()
    {   
        //set the bullet to destroy itself after a set number of seconds.
        Destroy(gameObject, seconds);
    }

    // Update is called once per frame
    void Update()
    {   
        //moves the buttet forward by it's speed value.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
