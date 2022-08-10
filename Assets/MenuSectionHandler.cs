using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSectionHandler : MonoBehaviour
{
    public static int menuID;

    private int saveMenuCase = -1;

    public GameObject[] menus;

    void Start()
    {
        SwitchMenu(1);
    }

    // Update is called once per frame
    public void SwitchMenu(int menuID)
    {
        switch(menuID)
        {
            case 0: //Nothing
                if(saveMenuCase < 0)
                {

                }
                break;
            case 1: //Weapon 1
                Debug.Log("Menu 0");
                saveMenuCase = 0;
                AssignMenu(0);
                break;
            case 2: //Weapon 2
                Debug.Log("Menu 1");
                saveMenuCase = 1;
                AssignMenu(1);
                break;
            case 3: //Weapon 3
                Debug.Log("Menu 2");
                saveMenuCase = 2;
                AssignMenu(2);
                break;
            case 4: //Weapon 4
                Debug.Log("Menu 3");
                saveMenuCase = 3;
                AssignMenu(3);
                break;
            case 5: //Weapon 5
                Debug.Log("Menu 4");
                saveMenuCase = 4;
                AssignMenu(4);
                break;
        }
    }

    void AssignMenu(int menuSelected)
    {
        for(int i = 0; i < menus.Length;i++)
        {
            if(i == menuSelected)
            {
                menus[i].SetActive(true);
            }
            else if(i != menuSelected)
            {
                menus[i].SetActive(false);
            }
        }
    }
}
