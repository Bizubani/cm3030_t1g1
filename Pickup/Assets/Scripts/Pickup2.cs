using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup2 : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
    }

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
