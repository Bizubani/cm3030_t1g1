using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFeatures : MonoBehaviour
{
    public float speed = 5f;
    private void Update() {

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
}
