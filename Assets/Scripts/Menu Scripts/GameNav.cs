using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNav : MonoBehaviour
{
    // Update is called once per frame
    public GameSettings gameSettings;
    [Header("Character Menu")]
    public GameObject CharacterMenu;

    [Header("Weapon Wheel Menu")]
    public GameObject WeaponWheelMenu;
    public WeaponWheelController weaponWheelController;

    [Header("Mission Stats Menu")]
    public GameObject MissionStatsMenu;

    void Start()
    {
        //SceneManager.LoadScene("Character Menu", LoadSceneMode.Additive);
        CharacterMenu.SetActive(false);
        WeaponWheelMenu.SetActive(false);
        MissionStatsMenu.SetActive(false);
        Cursor.visible = false;
    }

        // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !CharacterMenu.activeSelf)
        {
            setMenu(true);
        }
        else if(Input.GetKeyDown(KeyCode.C) && CharacterMenu.activeSelf)
        {
            setMenu(false);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(WeaponWheelMenu.activeSelf)
            {
                weaponWheelController.TriggerWeaponWheel();
                WeaponWheelMenu.SetActive(false);
            }
            else if(!CharacterMenu.activeSelf)
            {
                WeaponWheelMenu.SetActive(true);
                Cursor.visible = false;
                weaponWheelController.TriggerWeaponWheel();
            }
        }
    }

    public void setMenu(bool menuState)
    {
        CharacterMenu.SetActive(menuState);
        Cursor.visible = false;
        if(CharacterMenu.activeSelf)
        {
            gameSettings.timeManipulation(0);
        }
        if(!CharacterMenu.activeSelf)
        {
            gameSettings.timeManipulation(1);
        }
    }
}
