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
    private PlayerCollectableStats playerCollectableStats;
    public bool hasInteractSign = false;
    public GameObject interactSign;

    public bool hasFakeItemsInside = false;
    public GameObject cubeItems;

    private bool playerInLootRange;
    public bool IsLootBox = false;
    public Transform lootSpawnPoint;

    public bool hasAnimator = false;
    [SerializeField] private Animator PodAnimator;
    [SerializeField] private string IsOpen = "IsOpen";
    [SerializeField] private string IsIdle = "IsIdle";

    public LayerMask whoToWaitFor;
    private bool HasBeenCollected = false;
    public int openRange;

    // Start is called before the first frame update
    void Start()
    {
        if(hasAnimator)
        {
            PodAnimator.SetBool(IsOpen, false);
            PodAnimator.SetBool(IsIdle, true);
        }
        if(hasInteractSign)
        {
            interactSign = transform.GetChild(0).gameObject;
        }
        if(hasFakeItemsInside)
        {
            cubeItems = transform.GetChild(1).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLootBox)
        { 
            playerInLootRange = Physics.CheckSphere(transform.position, openRange, whoToWaitFor);
            if(playerInLootRange && !HasBeenCollected)
            {
                interactSign.SetActive(true);
            }
            else
            {
                interactSign.SetActive(false);
            }

            if(Input.GetKeyDown(KeyCode.X) && playerInLootRange && !HasBeenCollected)
            {
                PodAnimator.SetBool(IsOpen, true);
                PodAnimator.SetBool(IsIdle, false);
                playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();
                playerCollectableStats.addToPodsCollect(1);
                spawnLoot(lootSpawnPoint);
                interactSign.SetActive(false);
                cubeItems.SetActive(false);
                HasBeenCollected = true;
            }
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, openRange);
    }
}
