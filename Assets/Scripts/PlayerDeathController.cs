using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] GameObject robotBody;
    public float playerHealth;

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Zombie Enemy")
        {
            TakeDamage(1);
            Debug.Log("Player Got Hit, Health:" + playerHealth + "/10");
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0) 
        {
            Invoke(nameof(SelfDestruct), 0.5f);
        }
    }

    public void SelfDestruct()
    {
        Destroy(robotBody);
    }
}
