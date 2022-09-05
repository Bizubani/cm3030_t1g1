using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip collisionSound;
    public float volume = 0.8f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioSource.clip = collisionSound;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    void Start()
    {
        audioSource = GameObject.Find("Environment Collision Audio Source").GetComponent<AudioSource>();
    }
}
