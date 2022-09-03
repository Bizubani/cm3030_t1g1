using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int id;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;

    public TextMeshProUGUI CurrentBullets;
    public TextMeshProUGUI CurrentMagazine;

    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;
    public GameObject weaponWheel;

    private WeaponSettings weaponSettings;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = weaponWheel.GetComponent<AudioSource>();

        weaponSettings = GameObject.Find("Weapon Settings").GetComponent<WeaponSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = id;
        audioSource.Play();
    }

    public void Deselected()
    {
        selected = false;
        WeaponWheelController.weaponID = 0;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover",true);
        itemText.text = itemName;
        CurrentBullets.text = weaponSettings.retrieveCurrentAmmo(id-1).ToString() + " | ";
        CurrentMagazine.text = weaponSettings.retrieveCurrentMagazine(id-1).ToString();
    }

    public void HoverExit()
    {
        anim.SetBool("Hover",false);
        itemText.text = "";
    }
}
