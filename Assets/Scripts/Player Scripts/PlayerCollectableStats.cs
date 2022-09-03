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

    public Slider playerXPSlider;
    public int playerLevel = 0;

    public int currencyScrap = 0;
    public int totalCurrencyScrapCollected = 0;

    public TextMeshProUGUI playerScrapText;
    public TextMeshProUGUI playerTotalScrapCollectedText;

    public int playerPodsCollected = 0;
    public TextMeshProUGUI playerTotalPodsCollectedText;

    public int playerEnemyKills = 0;
    public TextMeshProUGUI playerTotalEnemyKillsText;

    public int playerBossKills = 0;
    public TextMeshProUGUI playerTotalBossKillsText;

    ////
    public int NUKEITEM = 0;
    public GameObject NukeObject;

    public int CUREITEM = 0;
    public GameObject CureObject;

    public int ROBOTITEM = 0;
    public GameObject RobotObject;

    public int SOURCEITEM = 0;
    public GameObject SourceObject;

    public int ROCKETITEM = 0;
    public GameObject RocketObject;

    void Start()
    {
        playerXPSlider.minValue = experienceXP; 
        playerXPSlider.maxValue = experienceXPCap;
        playerXPSlider.value = experienceXP;
    }

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
        playerXPSlider.value = experienceXP;
    }

    public void addToCurrencyScrap(int addScrap)
    {
        totalCurrencyScrapCollected += addScrap;
        currencyScrap += addScrap;
        playerScrapText.text = currencyScrap.ToString(); 
        playerTotalScrapCollectedText.text = "Scrap Collected: " + totalCurrencyScrapCollected.ToString();
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

    public void updateLootCounters()
    {
        addToExperienceXP(0);
        addToCurrencyScrap(0);
        addToPodsCollect(0);
        addToEnemiesKilled(0);
        addToBossesKilled(0);
    }
}
