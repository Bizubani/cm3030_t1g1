using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSettings : MonoBehaviour
{   
    public bool isSpecial = false;
    public int SpecialNumber;
    public float itemRange;
    private float tempItemRange;
    public bool playerInItemRange;
    public bool itemRespawn = false;
    public float respawnTimeInSeconds = 10f;

    //public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;

    public enum ItemState{XP, CURRENCY, HP, AMMO, SH, NUKE, CURE, ROBOT, SOURCE, ROCKET}

    public PlayerCollectableStats playerCollectableStats;
    public PlayerDeathController playerDeathController;
    public WeaponSettings weaponSettings;
    private GameObject interactButton;

    public ItemState state = ItemState.XP;

    public int applyItem = 0;
    public bool collectedItem = false;
    private bool appliedCurrentItem = false;

    public GameObject ItemCollectionExplosion;
    public float scaleDownSpeed = 2.0f;

    public int minSize = 0;
    public int maxSize = 1;
    public int startSize = 2;
    public float lootCollectSpeed = 1;
    public bool attract = false;
    public bool collectWithButton = false;
    
    private Vector3 targetScale;
    private Vector3 baseScale;
    private int currScale;

    AudioSource audioSource;
    public AudioClip collectClip;

    // Start is called before the first frame update
    void Start()
    {   
        baseScale = transform.localScale;
        transform.localScale = baseScale * startSize;
        currScale = startSize;
        targetScale = baseScale * startSize;
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();
        weaponSettings = GameObject.Find("Weapon Settings").GetComponent<WeaponSettings>();
        playerDeathController = GameObject.Find("Player Character").GetComponent<PlayerDeathController>();
        if(collectedItem)
        {
            interactButton = gameObject.transform.GetChild(0).gameObject;
        }
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player Character").transform;
        //agent.speed = lootCollectSpeed;

        audioSource = GetComponent<AudioSource>();

        if(state == ItemState.XP)
        {
            applyItem = Random.Range(5,15);
        }

        else if(state == ItemState.CURRENCY)
        {
            applyItem = Random.Range(1,5);
        }

        else if(state == ItemState.HP)
        {
            applyItem = Random.Range(25,50);
        }

        else if(state == ItemState.AMMO)
        {
            applyItem = Random.Range(1,100);
        }

        else if(state == ItemState.SH)
        {
            applyItem = Random.Range(25,50);
        }

        else
        {
            applyItem = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInItemRange = Physics.CheckSphere(transform.position, itemRange, whatIsPlayer);

        if(state == ItemState.HP)
        {
            if(playerDeathController.playerHealth != playerDeathController.playerHealthStart)
            {
                beginCollecting();
            }
        }

        else if(state == ItemState.SH)
        {
            if(playerDeathController.playerShield != playerDeathController.playerShieldStart)
            {
                beginCollecting();
            }
        }

        else
        {
            beginCollecting();
        }
    }

    void beginCollecting()
    {
       if(!collectedItem && playerInItemRange)
        {
            if(attract)
            {
                attractToPlayer();
            }

            if(collectWithButton)
            {
                if(Input.GetKeyDown(KeyCode.X))
                {
                    collectItem();
                }
            }
            else if(playerInItemRange && !collectWithButton)
            {
                collectItem();
            }
        }

        if(collectedItem)
        {
            transform.localScale = Vector3.Lerp (transform.localScale, targetScale, scaleDownSpeed * Time.deltaTime);
            currScale--;
            currScale = Mathf.Clamp (currScale, minSize, maxSize+1);
         
            targetScale = baseScale * currScale;
        }
    }

    void collectItem()
    {
        if(!appliedCurrentItem)
        {  
            if(state == ItemState.XP)
            {
                playerCollectableStats.addToExperienceXP(applyItem);
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.CURRENCY)
            {
                playerCollectableStats.addToCurrencyScrap(applyItem);
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.HP && playerDeathController.playerHealth < playerDeathController.playerHealthStart)
            {
                playerDeathController.addToCurrentHealth(applyItem);
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.AMMO)
            {
                weaponSettings.resetWeapons();
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.SH && playerDeathController.playerShield < playerDeathController.playerShieldStart)
            {
                playerDeathController.addToCurrentShield(applyItem);
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.NUKE)
            {
                playerCollectableStats.NUKEITEM = applyItem;
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.CURE)
            {
                playerCollectableStats.CUREITEM = applyItem;
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.ROBOT)
            {
                playerCollectableStats.ROBOTITEM = applyItem;
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.SOURCE)
            {
                playerCollectableStats.SOURCEITEM = applyItem;
                collectedItem = true;
                appliedCurrentItem = true;
            }

            else if(state == ItemState.ROCKET)
            {
                playerCollectableStats.ROCKETITEM = applyItem;
                collectedItem = true;
                appliedCurrentItem = true;
            }

            if(isSpecial)
            {
                playerCollectableStats.CheckSpecialItems(SpecialNumber);
            }
        }

        if(!collectedItem)
        {
            if(collectedItem)
            {
                interactButton.SetActive(false);
            }

            Instantiate(ItemCollectionExplosion, transform.position, transform.rotation,transform);
            audioSource.PlayOneShot(collectClip,1f);

            collectedItem = true;

            if(itemRespawn)
            {
                StartCoroutine(respawnItemAfterTime(respawnTimeInSeconds));
                // gameObject.transform.localScale = new Vector3(0,0,0);
                tempItemRange = itemRange;
                itemRange = 0;
            }
            else
            {
                Destroy(gameObject,2f);
            }
        }
    }

    void attractToPlayer()
    {
        //agent.SetDestination(player.position);

        float dist = Vector3.Distance(player.position, transform.position);
        if(dist < itemRange)
        {
            Destroy(gameObject,4f);
        }
    }

    IEnumerator respawnItemAfterTime(float maxTime)
    {
        yield return new WaitForSeconds(maxTime);
        gameObject.transform.localScale = new Vector3(1,1,1);
        itemRange = tempItemRange;
        appliedCurrentItem = true;
        collectedItem = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, itemRange);
    }
}
