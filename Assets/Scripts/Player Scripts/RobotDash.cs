using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private RigidBodyPlayerMovement RBPM;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Dashing")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVelocity = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private AudioSource DashSound;
    public GameObject dashEffect;
    public float dashStaminaAmount = 0;
    public float dashStaminaAmountStart = 25;

        
    //public TextMeshProUGUI playerDashText;
    public Slider playerDashSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        RBPM = GetComponent<RigidBodyPlayerMovement>();

        DashSound = GetComponent<AudioSource>();
        dashStaminaAmount = dashStaminaAmountStart;
        updateDash();
    }

    private void Update()
    {
        if(Input.GetKeyDown(dashKey) && dashStaminaAmount > 5)
        {
            Dash();
            DashSound.Play();
            Instantiate(dashEffect,transform.position,Quaternion.identity);
            updateDash();
        }
        
        if(dashStaminaAmount < dashStaminaAmountStart)
        {
            StartCoroutine(DashRegenerate());
        }

        if(dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if(dashCdTimer > 0) 
        {
            return;
        }
        else
        {
            dashCdTimer = dashCd;
        }

        RBPM.dashing = true;
        dashStaminaAmount -= 5f;

        Transform forwardTransform;

        if(useCameraForward)
        {
            forwardTransform = playerCam;
        }
        else
        {
            forwardTransform = orientation;
        }

        Vector3 direction = GetDirection(forwardTransform);
        
        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;
        
        if(disableGravity)
        {
            rb.useGravity = false;
        }

        delayedForceToApply = forceToApply;

        Invoke(nameof(DelayedDashForce), 0.025f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        if(resetVelocity)
        {
            rb.velocity = Vector3.zero;
        }

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        RBPM.dashing = false;

        if(disableGravity)
        {
            rb.useGravity = true;
        }
    }

    private Vector3 GetDirection(Transform forwardTransform)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if(allowAllDirections)
        {
            direction = forwardTransform.forward * verticalInput + forwardTransform.right * horizontalInput;
        }
        else
        {
            direction = forwardTransform.forward;
        }

        if(verticalInput == 0 && horizontalInput == 0)
        {
            direction = forwardTransform.forward;
        }

        return direction.normalized;
    }

    void updateDash()
    {
        playerDashSlider.value = dashStaminaAmount/dashStaminaAmountStart * 10;
    }

    private IEnumerator DashRegenerate()
    {
        yield return new WaitForSeconds(1f);
        dashStaminaAmount += 0.05f;
        updateDash();
    }
}
