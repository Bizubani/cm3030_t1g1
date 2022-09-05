using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderGenerator : MonoBehaviour
{
    private float timeBetweenThunder;
    public float timeBetweenThunderMin;
    public float timeBetweenThunderMax;

    PlayerMinimapPostion playerMinimapPostion;

    private bool inCooldown;

    Light thunderLight;
    // Start is called before the first frame update
    void Start()
    {
        thunderLight = GetComponent<Light>();
        playerMinimapPostion = GetComponent<PlayerMinimapPostion>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!inCooldown)
        {
            StartCoroutine(ThunderPhaseCooldown());
        }
    }

    private IEnumerator ThunderPhaseCooldown()
    {
        //Set the cooldown flag to true, wait for the cooldown time to pass, then turn the flag to false
        inCooldown = true; 
        playerMinimapPostion.GetRandomPosition();
        thunderLight.enabled = true;
        timeBetweenThunder = Random.Range(timeBetweenThunderMin, timeBetweenThunderMin*2);
        yield return new WaitForSeconds(timeBetweenThunder);
        thunderLight.enabled = false;
        timeBetweenThunder = Random.Range(timeBetweenThunderMin, timeBetweenThunderMax);
        yield return new WaitForSeconds(timeBetweenThunder);
        inCooldown = false;
    }
}
