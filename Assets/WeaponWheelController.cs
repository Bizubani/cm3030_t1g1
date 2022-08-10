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
    private int saveWeaponCase = -1;

    private GameObject CM;

    public GameSettings gameSettings;

    AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        try 
        {
            CM = GameObject.Find("Menu");

            if (!CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                weaponWheelSelected = !weaponWheelSelected;
                audioSource.PlayOneShot(clip2,1f);
            }

            if(weaponWheelSelected)
            {
                anim.SetBool("OpenWeaponWheel", true);
                gameSettings.timeSpeed = 0.1f;
            }
            else
            {
                anim.SetBool("OpenWeaponWheel", false);
                gameSettings.timeSpeed = 1f;
            }

            switch(weaponID)
            {
                case 0: //Nothing
                    if(saveWeaponCase < 0)
                    {
                        selectedItem.sprite = noImage;
                    }
                    break;
                case 1: //Weapon 1
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 0;
                    AssignWeapons(0);
                    break;
                case 2: //Weapon 2
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 1;
                    AssignWeapons(1);
                    break;
                case 3: //Weapon 3
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 2;
                    AssignWeapons(2);
                    break;
                case 4: //Weapon 4
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 3;
                    AssignWeapons(3);
                    break;
                case 5: //Weapon 5
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 4;
                    AssignWeapons(4);
                    break;
                case 6: //Weapon 6
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 5;
                    AssignWeapons(5);
                    break;
                case 7: //Weapon 7
                    Debug.Log("Weapon 1");
                    saveWeaponCase = 6;
                    AssignWeapons(6);
                    break;
            }
        }
    }

    void AssignWeapons(int weaponSelected)
    {
        // how many children does top have?
        GameObject weapons = GameObject.Find("Weapons");
        int weaponChild = weapons.transform.childCount;
        GameObject prefab = weaponPrefabs[weaponSelected];  

        for(int i = 0;i <  weaponChild; i++)
        {
            GameObject Child = weapons.transform.GetChild(i).gameObject;
            try
            {
                removeWeapons = Child.GetComponent<RemoveWeapons>();
                removeWeapons.removeWeapons();
            }
            catch(Exception e)
            {
                Debug.Log("No Weapons In here");
            }
            GameObject clone1 = Instantiate(prefab, Child.transform.position, Child.transform.rotation, Child.transform);
        }
    }
}
