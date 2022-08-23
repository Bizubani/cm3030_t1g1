using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public float RotationSpeed = 0;
    public Transform CameraTarget,ThirdPersonCharacter;
    float mouseX, mouseY;
    public bool rotateCamera = false;
    public bool cameraFollowPositionX = false;
    public bool cameraFollowCurrentPosition = false;

    private void Update()
    {
        if(cameraFollowPositionX == true)
        {
            Vector3 playerPosition = new Vector3(CameraTarget.position.x, 0, 0); 
            transform.position = playerPosition;
        }
        
        if(cameraFollowCurrentPosition == true)
        {
            transform.position = CameraTarget.position;
        }
    }

    void LateUpdate()
    {
        if(rotateCamera == true)
        {
            CamControl();
        }
    }
    //Update is called once per frame
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, 0,0); //-35,60

        transform.LookAt(CameraTarget);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        ThirdPersonCharacter.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
