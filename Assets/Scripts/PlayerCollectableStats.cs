using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCollectableStats : MonoBehaviour
{
    // Start is called before the first frame update
    public int experienceXP = 0;
    public int experienceXPCap = 50;
    public TextMeshProUGUI playerXPText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerLevelMenuText;
    public TextMeshProUGUI playerTotalOrbsCollectedText;
    public TextMeshProUGUI playerTotalOrbsCollectedTextInMenu;
    public int count = 0;

    public Slider playerXPSlider;
    public int playerLevel = 0;

    public int currencyScrap = 0;
    public int totalCurrencyScrapCollected = 0;

    public TextMeshProUGUI playerScrapText;
    public TextMeshProUGUI playerTotalScrapCollectedText;
    public TextMeshProUGUI playerTotalScrapCollectedTextInMenu;

    public int playerPodsCollected = 0;
    public TextMeshProUGUI playerTotalPodsCollectedText;

    public int playerEnemyKills = 0;
    public TextMeshProUGUI playerTotalEnemyKillsText;

    public int playerBossKills = 0;
    public TextMeshProUGUI playerTotalBossKillsText;

    ////
    public GameObject[] SpecialItems;
    public int NUKEITEM = 0;
    public int CUREITEM = 0;
    public int ROBOTITEM = 0;
    public int SOURCEITEM = 0;
    public int ROCKETITEM = 0;

    void Start()
    {
        playerXPSlider.minValue = experienceXP; 
        playerXPSlider.maxValue = experienceXPCap;
        playerXPSlider.value = experienceXP;

        for(int i = 0; i < SpecialItems.Length; i++)
        {
            SpecialItems[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void addToExperienceXP(int addXP)
    {
        experienceXP += addXP;

        playerXPText.text = "+"+ addXP.ToString(); 

        if(experienceXP >= experienceXPCap)
        {
            experienceXPCap = experienceXPCap * 3;
            playerLevel++;

            playerXPSlider.minValue = experienceXP; 
            playerXPSlider.maxValue = experienceXPCap;
        }
        playerLevelText.text = "Level | "+ playerLevel.ToString(); 
        playerLevelMenuText.text = "Level | "+ playerLevel.ToString(); 
        playerTotalOrbsCollectedText.text = "Orbs Collected: " + totalCurrencyScrapCollected.ToString();
        playerTotalOrbsCollectedTextInMenu.text = "/ " + totalCurrencyScrapCollected.ToString();
        playerXPSlider.value = experienceXP;
    }

    public void addToCurrencyScrap(int addScrap)
    {
        totalCurrencyScrapCollected += addScrap;
        currencyScrap += addScrap;
        playerScrapText.text = currencyScrap.ToString(); 
        playerTotalScrapCollectedText.text = "Scrap Collected: " + totalCurrencyScrapCollected.ToString();
        playerTotalScrapCollectedTextInMenu.text = "/ " + totalCurrencyScrapCollected.ToString();
    }

    public void addToPodsCollect(int addPods)
    {
        playerPodsCollected += addPods;
        playerTotalPodsCollectedText.text = "Loot Pods Found: " + playerPodsCollected.ToString();
    }

    public void addToEnemiesKilled(int addEnemy)
    {
        playerEnemyKills += addEnemy;
        playerTotalEnemyKillsText.text = "Enemies Destroyed: " + playerEnemyKills.ToString();
    }

    public void addToBossesKilled(int addBoss)
    {
        playerBossKills += addBoss;
        playerTotalBossKillsText.text = "Bosses Irradicated: " + playerBossKills.ToString();
    }

    public void CheckSpecialItems(int itemNumber)
    {
        SpecialItems[itemNumber].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void isMissionComplete()
    {
        for(int i = 0; i < 5; i++)
        {
            if(SpecialItems[i].transform.GetChild(0).gameObject.activeSelf)
            {
                count += 1;
            }
        }

        if(count < 5)
        {
            count = 0;
        }
    }

    public void updateLootCounters()
    {
        addToExperienceXP(0);
        addToCurrencyScrap(0);
        addToPodsCollect(0);
        addToEnemiesKilled(0);
        addToBossesKilled(0);
    }
}
