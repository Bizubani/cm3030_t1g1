using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewShowcase : MonoBehaviour
{
    public float speed = 1f;
    public bool itemRotate = true;
    public Transform[] levelLocations;
    public int levelNumber;

    private void Update() 
    {
        if(itemRotate)
        {
            RotateItem();
        }
        else
        {
            RotateToRotation(levelNumber);
        }
    }

    void RotateItem()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, 10 * Time.deltaTime * speed);
        }

        else if(Input.GetKey(KeyCode.D))
        {
        transform.Rotate(Vector3.down, 10 * Time.deltaTime * speed);
        }

        else
        {
            transform.Rotate(Vector3.down, 1 * Time.deltaTime * speed);
        }
    }

    public void RotateToRotation(int levelNumber)
    {
        if(transform.rotation == levelLocations[levelNumber].rotation)
        {
            transform.rotation = levelLocations[levelNumber].rotation;
            itemRotate = true;
        }
        else
        {
            itemRotate = false;
            Vector3 myNewRotation = new Vector3(0,-1,0);
            transform.Rotate(myNewRotation, 10 * Time.deltaTime * speed);
        }
    }
}

