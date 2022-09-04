using UnityEngine;
// using UnityEngine.InputSystem;

public class CinemachineCameraSwitcher : MonoBehaviour
{
    [SerializeField]
    // private InputAction myAction;
    private Animator animator;
    private bool normalCamera = false;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // private void OnEnable()
    // {
    //     myAction.Enable();
    // }

    // private void OnDisable()
    // {
    //     myAction.Disable();
    // }
    // // Start is called before the first frame update
    void Start()
    {
        // myAction.performed += _ => SwitchState();\
    }

    // // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchState();
        }

        if(Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if(normalCamera)
        {
            animator.Play("NormalCamera");
        }
        else
        {
            animator.Play("ZoomedShakeCamera");
        }
        normalCamera = !normalCamera;
    }
}
