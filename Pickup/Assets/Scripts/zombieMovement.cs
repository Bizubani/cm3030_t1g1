using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    GameObject player;

    float highestDif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posDif = player.transform.position - transform.position;
        Vector3 dir = new Vector3 (0, 0, 0);
        if(posDif.x > 0)
        {
            dir.x = 1;
        }
        else
        {
            dir.x = -1;
        }
        if(posDif.z > 0)
        {
            dir.z = 1;
        }
        else
        {
            dir.z = -1;
        }
        if((posDif.x > posDif.z && dir.x == 1 && dir.z == 1) || (posDif.x*dir.x > posDif.z && dir.x == -1 && dir.z == 1) || (posDif.x > posDif.z*dir.z && dir.x == 1 && dir.z == -1) || (posDif.x*dir.x > posDif.z*dir.z && dir.x == -1 && dir.z == -1))
        {
            highestDif = posDif.x;
        }
        else
        {
            highestDif = posDif.z;
        }
        if(highestDif < 0)
        {
            highestDif = highestDif/-1;
        }
        Vector3 movement = new Vector3((posDif.x/highestDif)*movementSpeed, 0 ,(posDif.z/highestDif)*movementSpeed);
        transform.Translate(movement * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Ow! That hurts");
        }
    }
}
