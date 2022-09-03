using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSectionHandler : MonoBehaviour
{
    public static int menuID;
    public static int canvasMenuID;

    private int saveMenuCase = -1;
    private int saveCanvasCase = -1;

    public GameObject[] menus;
    public GameObject[] canvasMenus;

    void Start()
    {
        SwitchMenu(1);
        for(int i = 0; i < menus.Length;i++)
        {
            canvasMenus[i].SetActive(false);
        }
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

    public void SwitchCanvasMenu(int canvasMenuID)
    {
        switch(canvasMenuID)
        {
            case 0: //Nothing
                if(saveCanvasCase < 0)
                {

                }
                break;
            case 1: //Weapon 1
                Debug.Log("Menu 0");
                saveCanvasCase = 0;
                AssignCanvasMenu(0);
                break;
            case 2: //Weapon 2
                Debug.Log("Menu 1");
                saveCanvasCase = 1;
                AssignCanvasMenu(1);
                break;
            case 3: //Weapon 3
                Debug.Log("Menu 2");
                saveCanvasCase = 2;
                AssignCanvasMenu(2);
                break;
            case 4: //Weapon 4
                Debug.Log("Menu 3");
                saveCanvasCase = 3;
                AssignCanvasMenu(3);
                break;
            case 5: //Weapon 5
                Debug.Log("Menu 4");
                saveCanvasCase = 4;
                AssignCanvasMenu(4);
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

    void AssignCanvasMenu(int menuSelected)
    {
        for(int i = 0; i < canvasMenus.Length;i++)
        {
            if(i == menuSelected)
            {
                canvasMenus[i].SetActive(true);
            }
            else if(i != menuSelected)
            {
                canvasMenus[i].SetActive(false);
            }
        }
    }
}
