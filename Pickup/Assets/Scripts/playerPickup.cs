using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPickup : MonoBehaviour
{
    int count;
    public void pickup(string type)
    {
        count += 1;
        Debug.Log(count);
        Debug.Log(type);
    }
}