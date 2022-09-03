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

    private Vector3 tmpMousePosition;
    AudioSource turretMove;

    public Transform PlayerCursor;
    public float rayCastDistance;

    private void Start()
    {
        PlayerCharacter = GameObject.Find("Player Character").GetComponent<Rigidbody>();
        PlayerCameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        PlayerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        PlayerCursor = GameObject.Find("Player Cursor").GetComponent<Transform>();

        tmpMousePosition = Input.mousePosition;
        turretMove = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        MovePlayer();       

        if (tmpMousePosition != Input.mousePosition)
        {
            Debug.Log("Mouse moved");
            tmpMousePosition = Input.mousePosition;
        }
        else
        {
            turretMove.Play();
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

            robotPlayer.transform.rotation = Quaternion.LookRotation(moveToCameraDirection);
        }


          //Find the exact hit position using a raycast
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);//A ray through the middle
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            float dist = Vector3.Distance(hit.point, robotTurret.transform.position);
            rayCastDistance = dist;
            if(dist > 7.5)
            {
                robotTurret.transform.LookAt(hit.point);
                PlayerCursor.position = hit.point;
            }
        }
   
    }
}
