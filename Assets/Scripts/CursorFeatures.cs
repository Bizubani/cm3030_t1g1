using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFeatures : MonoBehaviour
{
 
    public float speed = 5f;
    public bool isCursor2D = false;

    void Start() 
    {
        Cursor.visible = false;
    }
   
    private void Update() 
    {
        if(isCursor2D)
        {
            Vector2 mousePos = Input.mousePosition;
            transform.position = new Vector2(mousePos.x, mousePos.y);
        }
        else
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
    }
}
