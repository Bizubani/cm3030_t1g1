using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private GameObject[] levelSections;
    [SerializeField] int numberOfSections;
    GameObject gameLevel;

    // Start is called before the first frame update
    void Awake()
    {
        int biome = Random.Range(3,3);

        if (biome == 1)
        {
            levelSections = Resources.LoadAll<GameObject>("RedPrefabs");
        }
        if (biome == 2)
        {
            levelSections = Resources.LoadAll<GameObject>("GreenPrefabs");
        }
        if (biome == 3)
        {
            levelSections = Resources.LoadAll<GameObject>("BluePrefabs");
        }

        gameLevel = GameObject.Find("Generated Level");
        GameObject section0 = Instantiate(levelSections[0], new Vector3(0, 0, 0), Quaternion.identity);
        Vector3 nextSpawn = section0.transform.Find("SpawnPoint").position;
        
        section0.transform.parent = gameLevel.transform;
        
        for (int i = 0; i < numberOfSections; i++)
        {
            int section = Random.Range(1, levelSections.Length);
            GameObject grnSection = Instantiate(levelSections[section], nextSpawn, Quaternion.identity);
            nextSpawn = grnSection.transform.Find("SpawnPoint").position;

            grnSection.transform.parent = gameLevel.transform;
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