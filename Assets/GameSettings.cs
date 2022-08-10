using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public float timeSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed;
    }
}
