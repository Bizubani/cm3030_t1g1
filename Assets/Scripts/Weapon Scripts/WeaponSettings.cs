using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSettings : MonoBehaviour
{
    // Objects.Add(Obj);
    // Objects.remove(Obj);,
    // Objects.Insert(0,Obj);
    // Objects.RemoveAt(0);

    List<int> weaponCurrentAmmo = new List<int>();
    List<int> weaponCurrentMagazine = new List<int>();
    List<bool> weaponIsNew = new List<bool>();
    // // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            weaponCurrentAmmo.Add(0);
            weaponCurrentMagazine.Add(0);
            weaponIsNew.Add(true);
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public bool updateWeaponStatus(int weaponNumber)
    {
        if(weaponIsNew[weaponNumber] == true)
        {
            weaponIsNew[weaponNumber] = false;
            return (true);
        }
        else if(weaponIsNew[weaponNumber] == false)
        {
            return (false);
        }
        else
        {
            return (true);
        }
    }

    public void updateCurrentAmmo(int weaponNumber, int ammoAmount)
    {
        weaponCurrentAmmo[weaponNumber] = ammoAmount;
    }

    public void updateCurrentMagazine(int weaponNumber,int magazineAmount)
    {
        weaponCurrentMagazine[weaponNumber] = magazineAmount;
    }

    public int retrieveCurrentAmmo(int weaponNumber)
    {
        return weaponCurrentAmmo[weaponNumber];
    }

    public int retrieveCurrentMagazine(int weaponNumber)
    {
        return weaponCurrentMagazine[weaponNumber];
    }

    public void resetWeapons()
    {
        for(int i = 0; i < 8; i++)
        {
            weaponCurrentAmmo[i] = 0;
            weaponCurrentMagazine[i] = 0;
            weaponIsNew[i] = true;
        }
    }
}
