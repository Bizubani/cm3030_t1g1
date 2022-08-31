using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerStats : MonoBehaviour
{
    public PlayerCollectableStats playerCollectableStats;
    // Start is called before the first frame update
    void Start()
    {
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();

        playerCollectableStats.updateLootCounters();
    }
}
