using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerStats : MonoBehaviour
{
    public int statsRange;
    public bool playerInStatRange;
    public LayerMask whatIsPlayer;

    private GameSettings gameSettings;

    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {
        playerInStatRange = Physics.CheckSphere(transform.position, statsRange, whatIsPlayer);
        
        if(playerInStatRange && Input.GetKeyDown(KeyCode.X))
        {
            gameSettings = GameObject.Find("Game Settings").GetComponent<GameSettings>();
            TriggerResults();
        }
    }

    void TriggerResults()
    {
        gameSettings.setMissionStats();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, statsRange);
    }
}
