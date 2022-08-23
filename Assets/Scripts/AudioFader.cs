using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsPlayer;

    public bool playerInAudioRange;
    public float audioRange;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAudioRange = Physics.CheckSphere(transform.position, audioRange, whatIsPlayer);

        if(playerInAudioRange)
        {
            float dist = Vector3.Distance(player.position, transform.position);

            audioSource.volume = 1 - (dist/audioRange) * 1;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, audioRange);
    }
}
