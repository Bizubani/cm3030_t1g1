using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesSettings : MonoBehaviour
{
    public GameObject[] HealthTabs;
    public GameObject[] ShieldTabs;
    public GameObject[] SpecialTabs;
    // Start is called before the first frame update
    void Start()
    {
            AssignHealthTab(0,false);
            AssignShieldTab(0,false);
            AssignSpecialTab(0,false);
    }


    public void HealthUpgrades(int upgradeNumber)
    {
        switch(upgradeNumber)
        {
            case 0: //Nothing
                break;
            case 1: //Weapon 1
                Debug.Log("Menu 0");
                AssignHealthTab(0,true);
                break;
            case 2: //Weapon 2
                Debug.Log("Menu 1");
                AssignHealthTab(1,true);
                break;
            case 3: //Weapon 3
                Debug.Log("Menu 2");
                AssignHealthTab(2,true);
                break;
        }
    }

    public void ShieldUpgrades(int upgradeNumber)
    {
        switch(upgradeNumber)
        {
            case 0: //Nothing
                break;
            case 1: //Weapon 1
                Debug.Log("Menu 0");
                AssignShieldTab(0,true);
                break;
            case 2: //Weapon 2
                Debug.Log("Menu 1");
                AssignShieldTab(1,true);
                break;
            case 3: //Weapon 3
                Debug.Log("Menu 2");
                AssignShieldTab(2,true);
                break;
        }
    }

    public void EnhancementsUpgrades(int upgradeNumber)
    {
        switch(upgradeNumber)
        {
            case 0: //Nothing
                break;
            case 1: //Weapon 1
                Debug.Log("Menu 0");
                AssignSpecialTab(0,true);
                break;
            case 2: //Weapon 2
                Debug.Log("Menu 1");
                AssignSpecialTab(1,true);
                break;
            case 3: //Weapon 3
                Debug.Log("Menu 2");
                AssignSpecialTab(2,true);
                break;
        }
    }
    /////////////////
    /////////////////

    void AssignHealthTab(int tabSelected, bool onHealth)
    {
        if(onHealth)
        {
            AssignShieldTab(0,false);
            AssignSpecialTab(0,false);
            for(int i = 0; i < HealthTabs.Length;i++)
            {
                if(i == tabSelected)
                {
                    HealthTabs[i].SetActive(true);
                }
                else if(i != tabSelected)
                {
                    HealthTabs[i].SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 0; i < HealthTabs.Length;i++)
            {
                HealthTabs[i].SetActive(false);
            }
        }
    }

    void AssignShieldTab(int tabSelected, bool onShield)
    {
        if(onShield)
        {
            AssignHealthTab(0,false);
            AssignSpecialTab(0,false);
            for(int i = 0; i < ShieldTabs.Length;i++)
            {
                if(i == tabSelected)
                {
                    ShieldTabs[i].SetActive(true);
                }
                else if(i != tabSelected)
                {
                    ShieldTabs[i].SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 0; i < ShieldTabs.Length;i++)
            {
                ShieldTabs[i].SetActive(false);
            }
        }
    }

    void AssignSpecialTab(int tabSelected, bool onSpecial)
    {
        if(onSpecial)
        {
            AssignHealthTab(0,false);
            AssignShieldTab(0,false);
            for(int i = 0; i < SpecialTabs.Length;i++)
            {
                if(i == tabSelected)
                {
                    SpecialTabs[i].SetActive(true);
                }
                else if(i != tabSelected)
                {
                    SpecialTabs[i].SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 0; i < SpecialTabs.Length;i++)
            {
                SpecialTabs[i].SetActive(false);
            }
        }
    }
}
