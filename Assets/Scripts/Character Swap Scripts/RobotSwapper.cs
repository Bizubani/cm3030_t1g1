using UnityEngine;

public class RobotSwapper : MonoBehaviour
{
    public int selectedRobot;

    // Start is called before the first frame update
    void Start()
    {
        SelectRobot();
    }

    void Update()
    {
        int previousSelectedRobot = selectedRobot;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedRobot >= transform.childCount - 1)
            {
                selectedRobot = 0;
            }
            else
            {
                selectedRobot++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedRobot <= 0)
            {
                selectedRobot = transform.childCount - 1;
            }
            else
            {
                selectedRobot--;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedRobot = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedRobot = 1;
        }

        if(previousSelectedRobot != selectedRobot)
        {
            SelectRobot();
        }
    }

    // Update is called once per frame
    private void SelectRobot()
    {
        int i = 0;
        foreach (Transform robot in transform)
        {
            if (i == selectedRobot)
            {
                robot.gameObject.SetActive(true);
            }
            else
            {
                robot.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
