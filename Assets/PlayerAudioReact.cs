using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioReact : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.8f;
    public bool playActionMusic = false;
    public bool musicChecked = false;

    public List<Transform> enemy = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Zombie Enemy" && other.gameObject.GetComponent<EnemyController>().enemyHealth > 1)
        {
            enemy.Add(other.gameObject.transform);
            checkDistanceFromEnemies();
            if(enemy.Count > 1 && !musicChecked)
            {
                playActionMusic = true;
            }

        }
        else
        {
            playActionMusic = false;
            enemy.Remove(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Zombie Enemy" && other.gameObject.GetComponent<EnemyController>().enemyHealth <= 1)
        {
            enemy.Remove(other.gameObject.transform);
        }
    }

    // void Start()
    // {
    //     audioSource.PlayOneShot(clip, volume);
    // }

    // Update is called once per frame
    void Update()
    {
        for(var i = enemy.Count - 1; i > -1; i--)
        {
            if (enemy[i] == null)
            enemy.RemoveAt(i);
            audioSource.volume = volume;
        }
        
        if(enemy.Count > 0)
        {
            if(playActionMusic && !musicChecked)
            {
                audioSource.clip = clip;
                audioSource.Play();
                playActionMusic = false;
                musicChecked = true;
            }
        }
        else if(enemy.Count == 0)
        {
            if(volume > 0)
            {
                StartCoroutine(fadeMusic());
                if(volume <= 0)
                {
                    volume = 0;
                }
            }
            audioSource.Stop();
            musicChecked = false;
        }
    }

    void checkDistanceFromEnemies()
    {
        float closestDistance = float.MaxValue;
        int closestIndex = 0;
        for(int i = 0; i < enemy.Count; i++)
        {
            if(enemy[i].position != null)
            {
                float dist = Vector3.Distance(transform.position, enemy[i].position);
                if(dist < closestDistance && enemy[closestIndex].gameObject.GetComponent<EnemyController>().enemyHealth > 0)
                {
                    closestDistance = dist;
                    closestIndex = i;

                    volume = 0.75f - (dist/25)*0.75f;
                }
            }
            else
            {
                enemy.Remove(enemy[i]);
                
                return;
            }
        }
    }

    IEnumerator fadeMusic()
    {
        yield return new WaitForSeconds(5f);
        volume -= 0.1f;
    }
}
