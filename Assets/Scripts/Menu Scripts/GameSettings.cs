using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private float timeSpeed = 1f;
    public GameObject TriggerMenu;
    private PlayerCollectableStats playerCollectableStats;
    // Update is called once per frame
    void Start()
    {
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();;


        timeManipulation(timeSpeed);
    }

    public void timeManipulation(float speed)
    {
        Time.timeScale = speed;
    }

    public void setMissionStats()
    {
        if(TriggerMenu.activeSelf)
        {
            timeManipulation(1);
            TriggerMenu.SetActive(false);
        }
        else
        {
            timeManipulation(0);
            TriggerMenu.SetActive(true);
        }

        playerCollectableStats.isMissionComplete();
    }
}
