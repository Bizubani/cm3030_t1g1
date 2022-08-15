using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public float range;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < range)
        {
            Destroy(gameObject);
            player.GetComponent<playerPickup>().pickup("Wumpa Fruit");
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(Vector3.Distance(transform.position,player.transform.position));
            Debug.Log(range);
        }
    }
}
