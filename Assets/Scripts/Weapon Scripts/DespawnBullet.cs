using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour
{
    [SerializeField]
    public bool TriggerDelay = false;
    public float bulletDespawnDelayMin;
    public float bulletDespawnDelayMax;

    public GameObject bulletExplosion;
    public bool explodes = false;
    // Start is called before the first frame update
    void Start()
    {
        if(TriggerDelay == true)
        {
            float bulletDelay = Random.Range(bulletDespawnDelayMin, bulletDespawnDelayMax);
            Destroy(gameObject,bulletDelay);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && 
            TriggerDelay == false)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(explodes)
        {
            if(other.gameObject.tag == "Zombie Enemy")
            {
                Instantiate(bulletExplosion, transform.position, Quaternion.identity,transform);
                Destroy(gameObject,1f);
            }
        }
    }
}
