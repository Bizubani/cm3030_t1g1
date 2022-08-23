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

    void Start()
    {
        //SceneManager.LoadScene("Character Menu", LoadSceneMode.Additive);
        gameSettings = GetComponent<GameSettings>();
        CharacterMenu.SetActive(false);
        WeaponWheelMenu.SetActive(false);
    }

        // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CharacterMenu.SetActive(true);
            if(CharacterMenu.activeSelf)
            {
                gameSettings.timeSpeed = 0;
            }
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
                weaponWheelController.TriggerWeaponWheel();
            }
        }
    }
}
