using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private float timeSpeed = 1f;
    // Update is called once per frame
    void Start()
    {
        timeManipulation(timeSpeed);
    }

    public void timeManipulation(float speed)
    {
        Time.timeScale = speed;
    }
}
