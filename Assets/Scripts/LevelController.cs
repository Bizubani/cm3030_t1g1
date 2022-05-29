using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private GameObject[] levelSections;
    [SerializeField] int numberOfSections;

    // Start is called before the first frame update
    void Start()
    {
        int biome = Random.Range(1, 4);
        numberOfSections = 4;

        if (biome == 1)
        {
            levelSections = Resources.LoadAll<GameObject>("RedPrefabs");
        }
        else if (biome == 2)
        {
            levelSections = Resources.LoadAll<GameObject>("GreenPrefabs");
        }
        else if (biome == 3)
        {
            levelSections = Resources.LoadAll<GameObject>("BluePrefabs");
        }


        GameObject section0 = Instantiate(levelSections[0], new Vector3(0, 0, 0), Quaternion.identity);
        Vector3 nextSpawn = section0.transform.Find("SpawnPoint").position;

        for (int i = 0; i < numberOfSections; i++)
        {
            int section = Random.Range(1, levelSections.Length);
            GameObject grnSection = Instantiate(levelSections[section], nextSpawn, Quaternion.identity);
            nextSpawn = grnSection.transform.Find("SpawnPoint").position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("LevelGenPrototype");
        }
    }
}