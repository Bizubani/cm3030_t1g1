using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour
{
    [SerializeField]
    public float bulletDespawnDelayMin;
    public float bulletDespawnDelayMax;
    // Start is called before the first frame update
    void Start()
    {
        float bulletDelay = Random.Range(bulletDespawnDelayMin, bulletDespawnDelayMax);
        Destroy(gameObject,bulletDelay);
    }
}
