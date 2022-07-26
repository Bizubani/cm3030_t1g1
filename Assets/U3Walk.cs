using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U3Walk : MonoBehaviour
{
    [SerializeField] private Animator RobotPlayerAnimator;
    [SerializeField] private string IsIdle = "IsIdle";
    [SerializeField] private string IsWalking = "IsWalking";
    [SerializeField] private string BlendWalk = "BlendWalk";
    [SerializeField] private float BlendWalkingValue = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.S))
        {
            RobotPlayerAnimator.SetBool(IsWalking, true);
            RobotPlayerAnimator.SetBool(IsIdle, false);
            RobotPlayerAnimator.SetFloat(BlendWalk, BlendWalkingValue);
        }
        else
        {

            RobotPlayerAnimator.SetBool(IsWalking, false);
            RobotPlayerAnimator.SetBool(IsIdle, true);
        }
    }
}
