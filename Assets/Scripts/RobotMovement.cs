using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject robotPlayer;
    [SerializeField] GameObject robotTurret;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get Axis input, used to control movement via WASD keys.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Get Raw input for rotation of robot player.
        float bodyRotationH = Input.GetAxisRaw("Horizontal");
        float bodyRotationV = Input.GetAxisRaw("Vertical");

        //Get the target rotation direction and rotate towards that as speed determined rotateSpeed variable.
        Vector3 targetDirection = new Vector3(bodyRotationH, 0, bodyRotationV); 
        Vector3 newDirection = Vector3.RotateTowards(robotPlayer.transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
        robotPlayer.transform.rotation = Quaternion.LookRotation(newDirection);
        
        //Move the player globally via WASD input
        transform.Translate(new Vector3 (h, 0, v) * moveSpeed * Time.deltaTime, Space.World);
        
        //Create an invisable Ray from the camera to the mouse cursor.
        //Also creates a new plane as the height of the robot turret, this is use to rotate the turret towards the mouse cursor.
        Ray mouseRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, robotTurret.transform.position);

        if(p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            robotTurret.transform.LookAt(hitPoint);
        }

    }
}
