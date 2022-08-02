using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotTurretMovementScript : MonoBehaviour
{
    [SerializeField] private Rigidbody PlayerCharacter;
    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] Camera PlayerCamera;

    public GameObject robotPlayer;
    public GameObject robotTurret;


    [SerializeField] private float moveSpeed;
    public float smoothTurnTime = 0.1f;
    public float smoothTurnVelocity;

    private GameObject CM;

    public Transform PlayerCursor;
    private void Start()
    {
        PlayerCharacter = GameObject.Find("Player Character").GetComponent<Rigidbody>();
        PlayerCameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        PlayerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        PlayerCursor = GameObject.Find("Player Cursor").GetComponent<Transform>();
        Cursor.visible = false;
    }

    private void Update()
    {
        try 
        {
            CM = GameObject.Find("Character Menu");

            if (CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            MovePlayer();
        }   
    }

    private void MovePlayer()
    {
        //Get Axis input, used to control movement via WASD keys.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Get Raw input for rotation of robot player.
        float bodyRotationH = Input.GetAxis("Horizontal");
        float bodyRotationV = Input.GetAxis("Vertical");

        //Get the target rotation direction and rotate towards that as speed determined rotateSpeed variable.
        Vector3 targetDirection = new Vector3(bodyRotationH, 0f, bodyRotationV).normalized; 

        if(targetDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(targetDirection.x,targetDirection.z) * Mathf.Rad2Deg + PlayerCameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveToCameraDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // PlayerCharacter.Move(moveToCameraDirection.normalized * moveSpeed * Time.deltaTime);
            robotPlayer.transform.rotation = Quaternion.LookRotation(moveToCameraDirection);
            //robotPlayer.transform.rotation = Quaternion.LookRotation(newDirection);
        
        }
        //Create an invisable Ray from the camera to the mouse cursor.
        //Also creates a new plane as the height of the robot turret, this is use to rotate the turret towards the mouse cursor.
        // Ray mouseRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        // Plane p = new Plane(Vector3.up, Vector3.up, robotTurret.transform.position);
        
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        // if(p.Raycast(mouseRay, out float hitDist))
        // {
        //     Vector3 hitPoint = mouseRay.GetPoint(hitDist);
        //     robotTurret.transform.LookAt(hitPoint);
        // }

          //Find the exact hit position using a raycast
        // Ray ray = twoPointFiveDimensionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);//A ray through the middle
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            // Debug.Log(hit.point);
            // if(hit.point.x > -25)
            // {
                robotTurret.transform.LookAt(hit.point);
                PlayerCursor.position = hit.point;
            // }
        }
    }
}
