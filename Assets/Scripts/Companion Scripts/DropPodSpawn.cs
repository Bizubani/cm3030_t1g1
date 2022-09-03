using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPodSpawn : MonoBehaviour
{
    private GameObject playerCharacter;
    public Transform spawnPoint;
    public Animator podAnimator1;
    public Animator podAnimator2;
    public Animator podAnimator3;

    public string podIdle = "DoorIdle";
    public string podOpen = "DoorOpen";

    public bool isPodIdle = true;
    public bool isPodOpen = false;

    void Start()
    {
        playerCharacter = GameObject.Find("Player Character");
        playerCharacter.transform.position = spawnPoint.position;
        if(isPodIdle)
        {
            podAnimator1.SetBool(podIdle, false);
            podAnimator1.SetBool(podOpen, true);

            podAnimator2.SetBool(podIdle, false);
            podAnimator2.SetBool(podOpen, true);

            podAnimator3.SetBool(podIdle, false);
            podAnimator3.SetBool(podOpen, true);
        }
    }
}
