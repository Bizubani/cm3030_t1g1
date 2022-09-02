using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSelection : MonoBehaviour
{
    public RobotModel[] robotModels;
    public Transform Spot;
    private List<GameObject> robots;
    private int currentRobot;
    // Start is called before the first frame update
    void Start()
    {
        robots = new List<GameObject>();

        foreach (var robotModel in robotModels)
        {
            GameObject go = Instantiate(robotModel.URZA, Spot.position, Quaternion.identity);
            go.SetActive(false);
            go.transform.SetParent(Spot);
            robots.Add(go);
        }

        ShowRobotsFromList();
    }

    void update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClickNext();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            OnClickPrev();
        }
    }

    void ShowRobotsFromList()
    {
        robots[currentRobot].SetActive(true);
    }

    public void OnClickNext()
    {
        robots[currentRobot].SetActive(false);

        if(currentRobot < robots.Count - 1)
        {
            ++currentRobot;
        }
        else
        {
            currentRobot = 0;
        }

        ShowRobotsFromList();
    }

    public void OnClickPrev()
    {
        robots[currentRobot].SetActive(false);

        if(currentRobot == 0)
        {
            currentRobot = robots.Count - 1;
        }
        else
        {
            --currentRobot;
        }

        ShowRobotsFromList();
    }
}
