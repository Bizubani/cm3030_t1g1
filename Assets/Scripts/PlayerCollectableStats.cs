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

    public Slider playerXPSlider;
    public int playerLevel = 0;

    public int currencyScrap = 0;
    public TextMeshProUGUI playerScrapText;

    void Start()
    {
        playerXPSlider.minValue = experienceXP; 
        playerXPSlider.maxValue = experienceXPCap;
        playerXPSlider.value = experienceXP;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        playerXPSlider.value = experienceXP;
    }

    public void addToCurrencyScrap(int addScrap)
    {
        currencyScrap += addScrap;
        playerScrapText.text = currencyScrap.ToString(); 
    }

    public void updateLootCounters()
    {
        addToExperienceXP(0);
        addToCurrencyScrap(0);
    }
}
