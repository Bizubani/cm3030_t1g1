using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] GameObject robotTurret;
    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
 
    private float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timestamp && Input.GetButton("Fire1"))
        {
                Instantiate(bullet, robotTurret.transform.position, transform.rotation);
                timestamp = Time.time + timeBetweenShots;
        }
    }
}
