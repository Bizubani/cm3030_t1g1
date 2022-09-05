using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWeapons : MonoBehaviour
{
    public void removeWeapons()
    {
        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
