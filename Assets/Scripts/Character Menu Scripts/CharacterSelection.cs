using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public GameObject CharacterMenu;
    private LoadCharacter RobotUpdate;

    public void Awake()
    {
        ResumeGame();
    }

    // Start is called before the first frame update
    public void Start()
    {
        for(int i = 0;i < 3; i++ )
        {
            if(i > 0)
            {
                characters[i].SetActive(false);
            }
        }
    }

    public void Update()
    {
        Cursor.visible = true;
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    // Update is called once per frame
    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;

        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ResumeGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        RobotUpdate = GameObject.Find("Robots").GetComponent<LoadCharacter>();
        RobotUpdate.UpdateCharacter(); 
        CharacterMenu.SetActive(false);
    }
}
