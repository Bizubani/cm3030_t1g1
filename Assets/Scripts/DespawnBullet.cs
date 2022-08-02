using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour
{
    [SerializeField]
    public bool TriggerDelay = false;
    public float bulletDespawnDelayMin;
    public float bulletDespawnDelayMax;
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
}
