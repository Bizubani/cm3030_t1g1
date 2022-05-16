using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFiring : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] GameObject robotTurret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position, robotTurret.transform.rotation);
        }
    }
}
