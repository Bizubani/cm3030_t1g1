using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    public GameObject[] levels;
    public int activeLevel = 2;
    public int selectedLevel = 0;
    public GameObject MapMenu;
    private LevelController levelController;
    private NavRealTimeBaker navRealTimeBaker;
    private CharacterPreviewShowcase previewItem;

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
    }

    public void NextLevel()
    {
        if(MapMenu.activeSelf)
        {
            levels[selectedLevel].SetActive(false);
            selectedLevel = (selectedLevel + 1) % levels.Length;
            levels[selectedLevel].SetActive(true);
            previewItem.RotateToRotation(selectedLevel);
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
        }
    }

    public void loadLevel()
    {
        if(activeLevel != selectedLevel)
        {
            levelController = GameObject.Find("Generated Level").GetComponent<LevelController>();
            levelController.DestroyAllLevels();
            levelController.GenerateLevel(selectedLevel);
            navRealTimeBaker = GameObject.Find("Level Generator Baker").GetComponent<NavRealTimeBaker>();
            navRealTimeBaker.BakeLevel();
            activeLevel = selectedLevel;
        }
    }
}
