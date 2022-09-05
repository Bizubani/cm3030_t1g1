using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MapSelection : MonoBehaviour
{
    public GameObject[] levels;
    public int activeLevel = 0;
    public int selectedLevel = 0;
    public GameObject MapMenu;
    private LevelController levelController;
    private NavRealTimeBaker navRealTimeBaker;
    private CharacterPreviewShowcase previewItem;
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        previewItem = GameObject.Find("Earth").GetComponent<CharacterPreviewShowcase>();

        for(int i = 0;i < levels.Length; i++ )
        {
            if(i > 0)
            {
                levels[i].SetActive(false);
            }
        }

        levelText.text = "Level: " + (selectedLevel + 1).ToString(); 
    }

    public void NextLevel()
    {
        if(MapMenu.activeSelf)
        {
            levels[selectedLevel].SetActive(false);
            selectedLevel = (selectedLevel + 1) % levels.Length;
            levels[selectedLevel].SetActive(true);
            previewItem.RotateToRotation(selectedLevel);
            levelText.text = "Level: "+ (selectedLevel + 1).ToString(); 
        }
        else
        {            
            levels[selectedLevel].SetActive(false);
            selectedLevel = (selectedLevel + 1) % levels.Length;
            levels[selectedLevel].SetActive(true);
            selectedLevel = (selectedLevel + 1) % levels.Length;
            levelText.text = "Level: "+ (selectedLevel + 1).ToString(); 
        }
    }

    // Update is called once per frame
    public void PreviousLevel()
    {
        if(MapMenu.activeSelf)
        {
            levels[selectedLevel].SetActive(false);
            selectedLevel--;

            if(selectedLevel < 0)
            {
                selectedLevel += levels.Length;
            }
            levels[selectedLevel].SetActive(true);
            previewItem.RotateToRotation(selectedLevel);

            levelText.text = "Level: " + (selectedLevel + 1).ToString(); 
        }
    }

    public void loadLevel()
    {
        if(activeLevel != selectedLevel)
        {
            levelController = GameObject.Find("Generated Level").GetComponent<LevelController>();
            levelController.DestroyAllLevels();
            levelController.SwitchLevel(selectedLevel);
            navRealTimeBaker = GameObject.Find("Level Generator Baker").GetComponent<NavRealTimeBaker>();
            navRealTimeBaker.BakeLevel();
            activeLevel = selectedLevel;
        }
    }
}
