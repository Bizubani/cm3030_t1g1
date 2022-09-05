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

    [SerializeField] public CharacterController PlayerCharacter;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Jumpforce;

    [SerializeField] private float gravity;
    Vector3 velocity;

    public float smoothTurnTime = 0.1f;
    public float smoothTurnVelocity;

    private float resetStepOffset;

    public Vector3 move;

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
        // //Get Raw input for rotation of robot player.
        float bodyRotationH = Input.GetAxisRaw("Horizontal");
        float bodyRotationV = Input.GetAxisRaw("Vertical");

        ///Jump CC
        if (Input.GetButtonDown("Jump") && PlayerCharacter.isGrounded)
        {
            velocity.y = Mathf.Sqrt(Jumpforce * -2f * gravity);
        }
 
        if (PlayerCharacter.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        velocity.y += gravity * Time.deltaTime;
        move = transform.right * bodyRotationH + transform.forward * bodyRotationV;
        PlayerCharacter.Move(move * moveSpeed * Time.deltaTime + velocity * Time.deltaTime);
    }
}
