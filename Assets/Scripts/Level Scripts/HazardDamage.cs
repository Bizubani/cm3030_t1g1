using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    public float hazardRange = 1f;
    public bool playerInHazardRange = false;

    private GameObject player;
    public LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Character");
    }

    // Update is called once per frame
    void Update()
    {
        playerInHazardRange = Physics.CheckSphere(transform.position, hazardRange, whatIsPlayer);

        if(playerInHazardRange)
        {
            player.GetComponent<PlayerDeathController>().TakeDamage(1);
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hazardRange);
    }
}
