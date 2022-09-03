using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBodyReact : MonoBehaviour
{
    [SerializeField] private Animator RobotPlayerAnimator;
    [SerializeField] private string IsSquished = "IsSquished";
    //[SerializeField] private float BlendWalkingValue = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RobotPlayerAnimator.SetBool(IsSquished, true);
        }
        else
        {

            RobotPlayerAnimator.SetBool(IsSquished, false);
        }
    }
}
