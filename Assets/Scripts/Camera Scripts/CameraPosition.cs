using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public float RotationSpeed = 0;
    public Transform CameraTarget,ThirdPersonCharacter;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        CamControl();
    }
    // Update is called once per frame
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, 0,0); //-35,60

        transform.LookAt(CameraTarget);
        CameraTarget.rotation = Quaternion.Euler(0, 0, 0);
        ThirdPersonCharacter.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
