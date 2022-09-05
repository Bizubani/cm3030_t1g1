using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private GameObject[] levelSections;
    [SerializeField] int numberOfSections;
    GameObject gameLevel;
    public int levelNumber;
    public bool gameStarted = true;
    private int RandomSection;
    private GameObject[] starterLevels;

    // Start is called before the first frame update
    void Awake()
    {
        GenerateLevel(gameStarted);
    }

    public void SwitchLevel(int number)
    {
        levelNumber = number;
        GenerateLevel(true);
    }

    public void GenerateLevel(bool started)
    {
        starterLevels = Resources.LoadAll<GameObject>("Starter Levels");
        if(!gameStarted)
        {
            levelNumber++;
        }

        if (levelNumber == 0)
        {
            levelSections = Resources.LoadAll<GameObject>("BluePrefabs");
        }
        if (levelNumber == 1)
        {
            levelSections = Resources.LoadAll<GameObject>("GreenPrefabs");
        }

        gameLevel = GameObject.Find("Generated Level");
        GameObject section0 = Instantiate(starterLevels[0], new Vector3(0, 0, 0), transform.rotation);
        Vector3 nextSpawn = section0.transform.Find("SpawnPoint").position;
        
        section0.transform.parent = gameLevel.transform;
        
        for (int i = 0; i < numberOfSections -1; i++)
        {
            GameObject grnSection = Instantiate(levelSections[i], nextSpawn, transform.rotation);
            nextSpawn = grnSection.transform.Find("SpawnPoint").position;

            grnSection.transform.parent = gameLevel.transform;
        }
    }

    public void DestroyAllLevels()
    {
        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}