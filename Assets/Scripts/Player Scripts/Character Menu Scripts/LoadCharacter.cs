using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public TMP_Text labelInGame;
    public TMP_Text labelInMenu;
    private Transform Robot;

    // Start is called before the first frame update
    public void UpdateCharacter()
    {
        Robot = GameObject.Find("Robots").GetComponent<Transform>();
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];

        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity, Robot);
        labelInGame.text = prefab.name;
        labelInMenu.text = prefab.name;
    }
}
