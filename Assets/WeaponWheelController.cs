using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    public GameObject[] weaponPrefabs;
    private Transform spawnPoint;
    private RemoveWeapons removeWeapons;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if(weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 0: //Nothing
                selectedItem.sprite = noImage;
                break;
            case 1: //Weapon 1
                Debug.Log("Weapon 1");
                AssignWeapons(0);
                break;
            case 2: //Weapon 2
                Debug.Log("Weapon 1");
                AssignWeapons(1);
                break;
            case 3: //Weapon 3
                Debug.Log("Weapon 1");
                AssignWeapons(2);
                break;
            case 4: //Weapon 4
                Debug.Log("Weapon 1");
                AssignWeapons(3);
                break;
            case 5: //Weapon 5
                Debug.Log("Weapon 1");
                AssignWeapons(4);
                break;
            case 6: //Weapon 6
                Debug.Log("Weapon 1");
                AssignWeapons(5);
                break;
            case 7: //Weapon 7
                Debug.Log("Weapon 1");
                AssignWeapons(6);
                break;
        }
    }

    void AssignWeapons(int weaponSelected)
    {
        GameObject LeftWeaponSpawn = GameObject.Find("LeftWeaponSpawn");
        GameObject RightWeaponSpawn = GameObject.Find("RightWeaponSpawn");
        GameObject CentreWeaponSpawn = GameObject.Find("CentreWeaponSpawn");

        Transform LeftWeaponSpawnTrans = GameObject.Find("LeftWeaponSpawn").GetComponent<Transform>();
        Transform RightWeaponSpawnTrans = GameObject.Find("RightWeaponSpawn").GetComponent<Transform>();
        Transform CentreWeaponSpawnTrans = GameObject.Find("CentreWeaponSpawn").GetComponent<Transform>();
        GameObject prefab = weaponPrefabs[weaponSelected];

        try
        {
            removeWeapons = LeftWeaponSpawn.GetComponent<RemoveWeapons>();
            removeWeapons.removeWeapons();
            removeWeapons = RightWeaponSpawn.GetComponent<RemoveWeapons>();
            removeWeapons.removeWeapons();
            removeWeapons = CentreWeaponSpawn.GetComponent<RemoveWeapons>();
            removeWeapons.removeWeapons();
        }
        catch(Exception e)
        {
            Debug.Log("No Weapons In here");
        }

        if(LeftWeaponSpawn)
        {
            //GameObject clone = Instantiate(prefab, LeftWeaponSpawnTrans.position, Quaternion.identity, LeftWeaponSpawnTrans,false);
            GameObject clone = Instantiate(prefab, LeftWeaponSpawnTrans.position, LeftWeaponSpawnTrans.rotation,LeftWeaponSpawnTrans);
        }
        else if(RightWeaponSpawn)
        {
            //GameObject clone1 = Instantiate(prefab, RightWeaponSpawnTrans.position, Quaternion.identity, RightWeaponSpawnTrans,false);
            GameObject clone1 = Instantiate(prefab, RightWeaponSpawnTrans.position, RightWeaponSpawnTrans.rotation, RightWeaponSpawnTrans);
        }
        else if(CentreWeaponSpawn)
        {
            //GameObject clone1 = Instantiate(prefab, RightWeaponSpawnTrans.position, Quaternion.identity, RightWeaponSpawnTrans,false);
            GameObject clone2 = Instantiate(prefab, CentreWeaponSpawnTrans.position, CentreWeaponSpawnTrans.rotation, CentreWeaponSpawnTrans);
        }
    }
}
