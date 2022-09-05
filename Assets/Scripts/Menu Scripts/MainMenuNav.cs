using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    private GameObject OptionsMenu;

    void Start()
    {
        OptionsMenu = GameObject.Find("Options Menu");
        gameObject.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("You have Destroyed the Game");
        Application.Quit();
    }
}
