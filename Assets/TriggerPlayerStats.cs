using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerStats : MonoBehaviour
{
    public bool IsBoss = false;
    private bool BossHasSpawned = false;
    public GameObject triggerMenu;

    public int statsRange;
    public bool playerInStatRange;
    public LayerMask whatIsPlayer;

    private bool explore = false;

    // Start is called before the first frame update
    void Start()
    {
        triggerMenu = GameObject.Find("Canvas (Mission Stats)");   
    }

    // Update is called once per frame
    void Update()
    {
        playerInStatRange = Physics.CheckSphere(transform.position, statsRange, whatIsPlayer);
        
        if(playerInStatRange && Input.GetKeyDown(KeyCode.X))
        {
            TriggerResults();
        }
    }

    void TriggerResults()
    {
        if(triggerMenu.activeSelf)
        {
            triggerMenu.SetActive(false);
        }
        else
        {
            triggerMenu.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, statsRange);
    }
}
