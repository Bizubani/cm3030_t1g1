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
        if (levelNumber == 2)
        {
            levelSections = Resources.LoadAll<GameObject>("RedPrefabs");
        }

        gameLevel = GameObject.Find("Generated Level");
        GameObject section0 = Instantiate(levelSections[0], new Vector3(0, 0, 0), Quaternion.identity);
        Vector3 nextSpawn = section0.transform.Find("SpawnPoint").position;
        
        section0.transform.parent = gameLevel.transform;
        
        for (int i = 0; i < numberOfSections -1; i++)
        {
            if(i == 0)
            {
                RandomSection = Random.Range(1,1);
            }

            if(i == 1)
            {
                RandomSection = Random.Range(2,2);
            }
            int section = RandomSection;
            GameObject grnSection = Instantiate(levelSections[section], nextSpawn, Quaternion.identity);
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

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown("space"))
    //     {
    //         SceneManager.LoadScene("Project URZA Game Test Scene");
    //     }
    // }
}