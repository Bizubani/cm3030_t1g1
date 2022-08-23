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
        OptionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("You have Destroyed the Game");
        Application.Quit();
    }
}
