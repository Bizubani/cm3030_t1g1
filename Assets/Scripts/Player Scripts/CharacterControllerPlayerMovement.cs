using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public CharacterController PlayerCharacter;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject robotPlayer;
    [SerializeField] GameObject robotTurret;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed = 3.0f;

    public float smoothTurnTime = 0.1f;
    public float smoothTurnVelocity;
    public Transform playerCameraTransform;

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
        Vector3 targetDirection = new Vector3(bodyRotationH, 0f, bodyRotationV).normalized; 

        if(targetDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(targetDirection.x,targetDirection.z) * Mathf.Rad2Deg + playerCameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveToCameraDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            PlayerCharacter.Move(moveToCameraDirection.normalized * moveSpeed * Time.deltaTime);
            robotPlayer.transform.rotation = Quaternion.LookRotation(moveToCameraDirection);
        }

        Vector3 newDirection = Vector3.RotateTowards(robotPlayer.transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
                
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
