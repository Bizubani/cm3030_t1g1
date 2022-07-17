using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] GameObject robotBody;
    public float playerHealth;

     public static Color ColorAlive;
     public static Color ColorDamaged;
     public static Color ColorDead;
     public Light targetlight;

    void Start()
    {
        ColorAlive = Color.green;
        ColorDamaged = Color.yellow;
        ColorDead = Color.red;
    }

    void Update()
    {
        if(playerHealth >= 80)
        {
            targetlight.color = ColorAlive;
        }
        else if(playerHealth < 80 && playerHealth >= 50)
        {
            targetlight.color = ColorDamaged;
        }
        else if(playerHealth < 50 && playerHealth >= 0)
        {
            targetlight.color = ColorDead;
        }
    }

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Zombie Enemy")
        {
            if(collisionInfo.gameObject.GetComponent<EnemyController>().enemyHealth > 0)
            {
                TakeDamage(1);
                Debug.Log("Player Got Hit, Health:" + playerHealth + "/10");
            }
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
