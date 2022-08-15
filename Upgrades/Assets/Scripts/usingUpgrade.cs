using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usingUpgrade : MonoBehaviour
{
    [SerializeField]
    public GameObject upgrades;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(upgrades.GetComponent<Main>().stats[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Gain skillpoint");
            upgrades.GetComponent<Main>().skillPoints += 1;
        }
        
    }
}
