using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleAimer : MonoBehaviour
{   private RobotTurretMovementScript robotTurretMovementScript;
    private GameObject aimUI;

    public float sizeX = 100f;
    public float sizeY = 100f;

    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        robotTurretMovementScript = this.GetComponent<RobotTurretMovementScript>();
        aimUI = GameObject.Find("Aim UI");
    }

    // Update is called once per frame
    void Update()
    {
        scaleChange = new Vector3(sizeX, sizeY, robotTurretMovementScript.rayCastDistance);
        aimUI.transform.localScale = scaleChange;
    }
}
