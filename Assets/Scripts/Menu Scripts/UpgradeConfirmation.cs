﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeConfirmation : MonoBehaviour
{
    public int scrapPrice;
    public int orbPrice;
    private bool scrapValid = false;
    private bool orbsValid = false;

    public int upgradeType;

    private bool upgradePurchased = false;

    private PlayerCollectableStats playerCollectableStats;
    private PlayerDeathController playerDeathController;
    private RigidBodyPlayerMovement rigidBodyPlayerMovement;

    public TextMeshProUGUI textPurchased;

    // Start is called before the first frame update
    void Start()
    {
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();
        playerDeathController = GameObject.Find("Player Character").GetComponent<PlayerDeathController>();
        rigidBodyPlayerMovement = GameObject.Find("Player Character").GetComponent<RigidBodyPlayerMovement>();
    }

    public void checkScrap(int scrapAmount)
    {
        if(playerCollectableStats.currencyScrap >= scrapAmount)
        {
            playerCollectableStats.currencyScrap -= scrapAmount;
            scrapValid = true;
        }
        else
        {
            scrapValid = false;
        }
    }

    public void checkOrbs(int orbsAmount)
    {
        if(playerCollectableStats.experienceXP >= orbsAmount)
        {
            playerCollectableStats.experienceXP -= orbsAmount;
            orbsValid = true;
        }
        else
        {
            orbsValid = false;
        }
    }

    public void confirmPurchase(int upgradeVersion)
    {
        if(upgradePurchased == false)
        {
            checkScrap(scrapPrice);
            checkOrbs(orbPrice);
            playerCollectableStats.updateLootCounters();

            if(scrapValid && orbsValid)
            {
                if(upgradeType == 1)
                {
                    playerDeathController.addToMaxHealth(upgradeVersion);
                }

                else if(upgradeType == 2)
                {
                    playerDeathController.addToMaxShield(upgradeVersion);
                }
                else if(upgradeType == 3)
                {
                    rigidBodyPlayerMovement.addToMoveSpeed(100);
                }
                else if(upgradeType == 4)
                {
                    rigidBodyPlayerMovement.addToDashSpeed(100);
                }
                else if(upgradeType == 5)
                {
                    rigidBodyPlayerMovement.addToJumpForce(40);
                }
                textPurchased.text = "PURCHASED";
                upgradePurchased = true;
            }
        }
    }
}
