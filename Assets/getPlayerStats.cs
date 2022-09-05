using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerStats : MonoBehaviour
{
    public PlayerCollectableStats playerCollectableStats;
    public GameObject MissionComplete;
    // Start is called before the first frame update
    void Start()
    {
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();
    }

    void Update()
    {
        if(playerCollectableStats.count >= 5)
        {
            MissionComplete.SetActive(true);
        }
        else
        {
            MissionComplete.SetActive(false);
        }
    }
}
