using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    private int randomXPAmount;
    private int randomScrapAmount;

    private int randomXPos;
    private int randomYPos;
    private int randomZPos;

    public GameObject xpOrb;
    public GameObject scrapOrb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnLoot(Transform enemyPosition)
    {
        randomXPAmount = Random.Range(0,5);
        randomScrapAmount = Random.Range(0,5);

        for(int i = 0; i < randomXPAmount; i++)
        {
            randomSpawnposition();
            Vector3 newLootPosition = new Vector3(enemyPosition.position.x + randomXPos, enemyPosition.position.y, enemyPosition.position.z + randomZPos);
            Instantiate(xpOrb,newLootPosition,Quaternion.identity);
        }

        for(int i = 0; i < randomScrapAmount; i++)
        {
            randomSpawnposition();
            Vector3 newLootPosition = new Vector3(enemyPosition.position.x + randomXPos, enemyPosition.position.y, enemyPosition.position.z + randomZPos);
            Instantiate(scrapOrb,newLootPosition,Quaternion.identity);
        }
    }

    void randomSpawnposition()
    {
        randomXPos = Random.Range(-2,2);
        //randomYPos = Random.Range(0,0);
        randomZPos = Random.Range(-2,2);
    }
}
