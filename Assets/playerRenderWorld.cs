using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRenderWorld : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
