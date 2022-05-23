using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacterMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;

    [SerializeField] private Transform CollisionToJumpDetection;
    [SerializeField] private LayerMask CollisionToJumpMask;

    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] Camera PlayerCamera;

    [SerializeField] private CharacterController PlayerCharacter;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;

    [SerializeField] GameObject robotPlayer;
    [SerializeField] GameObject robotTurret;
    [SerializeField] private float gravity;
    Vector3 velocity;

    public float smoothTurnTime = 0.1f;
    public float smoothTurnVelocity;

    private float resetStepOffset;

    // Update is called once per frame
    void Start()
    {
        PlayerCharacter = GetComponent<CharacterController>();
        resetStepOffset = PlayerCharacter.stepOffset;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Get Axis input, used to control movement via WASD keys.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //Get Raw input for rotation of robot player.
        float bodyRotationH = Input.GetAxisRaw("Horizontal");
        float bodyRotationV = Input.GetAxisRaw("Vertical");

        //Get the target rotation direction and rotate towards that as speed determined rotateSpeed variable.
        Vector3 targetDirection = new Vector3(bodyRotationH, 0f, bodyRotationV).normalized; 

        if(targetDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(targetDirection.x,targetDirection.z) * Mathf.Rad2Deg + PlayerCameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveToCameraDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            PlayerCharacter.Move(moveToCameraDirection.normalized * moveSpeed * Time.deltaTime);
            robotPlayer.transform.rotation = Quaternion.LookRotation(moveToCameraDirection);
        }

        //robotPlayer.transform.rotation = Quaternion.LookRotation(newDirection);
        
        //Create an invisable Ray from the camera to the mouse cursor.
        //Also creates a new plane as the height of the robot turret, this is use to rotate the turret towards the mouse cursor.
        Ray mouseRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, robotTurret.transform.position);

        if(p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            robotTurret.transform.LookAt(hitPoint);
        }


        //////////////

        ///Jump CC
        if (Input.GetButtonDown("Jump") && PlayerCharacter.isGrounded)
        {
            velocity.y = Mathf.Sqrt(Jumpforce * -2f * gravity);
        }
 
        //transform.eulerAngles = new Vector3(0.0f, 0.0, 0.0f);
 
        if (PlayerCharacter.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        velocity.y += gravity * Time.deltaTime;
        Vector3 move = transform.right * bodyRotationH + transform.forward * bodyRotationV;
 
        PlayerCharacter.Move(move * moveSpeed * Time.deltaTime + velocity * Time.deltaTime);
    }
}
