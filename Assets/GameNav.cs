using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNav : MonoBehaviour
{
    // Update is called once per frame
    public GameObject CharacterMenu;
    void Start()
    {
        //SceneManager.LoadScene("Character Menu", LoadSceneMode.Additive);

        CharacterMenu.SetActive(false);
    }

        // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CharacterMenu.SetActive(true);
        }
    }
}
