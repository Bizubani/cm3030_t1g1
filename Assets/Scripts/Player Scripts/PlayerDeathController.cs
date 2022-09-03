using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] GameObject robotBody;
    public GameObject DeathExplosion;
    public bool isPlayerDead = false;
    Rigidbody rb;

    public float playerHealth;
    public float playerHealthStart;

    public bool shieldActive = false;
    public float playerShield;
    public float playerShieldStart;
    public GameObject Shield;
    [Header("Keybinds")]
    public KeyCode shieldKey = KeyCode.Q;
    public KeyCode respawnKey = KeyCode.O;
    
    public TextMeshProUGUI playerHealthText;
    public Slider playerHealthSlider;

    public TextMeshProUGUI playerShieldText;
    public Slider playerShieldSlider;

    public static Color ColorAlive;
    public static Color ColorDamaged;
    public static Color ColorDead;
    public Light targetlight;

    public GameObject menu;

    public Transform reviveSpawnPoint;

    ////
    public PlayerCollectableStats playerCollectableStats;
    [Header("Upgrades People, Upgrades")]
    public bool healthUpgradeI = false;
    public bool healthUpgradeII = false;
    public bool healthUpgradeIII = false;

    public bool shieldUpgradeI = false;
    public bool shieldUpgradeII = false;
    public bool shieldUpgradeIII = false;

    private AudioSource audioSource;
    public AudioClip shieldONClip;
    public AudioClip shieldOFFClip;

    void Start()
    {
        ColorAlive = Color.green;
        ColorDamaged = Color.yellow;
        ColorDead = Color.red;

        audioSource = GetComponent<AudioSource>();
        playerCollectableStats = GetComponent<PlayerCollectableStats>();
        reviveSpawnPoint = GameObject.Find("Spawn Point").GetComponent<Transform>();

        transform.position = reviveSpawnPoint.position;
        rb = GetComponent<Rigidbody>();

        playerHealthStart = playerHealth;
        playerShieldStart = playerShield;

        updateHealthAndShield();
        shieldActivator();
    }

    void Update()
    {
        if(Input.GetKeyDown(respawnKey))
        {
            playerHealth = 0;
        }

        if(Input.GetKeyDown(shieldKey))
        {
            shieldActive = !shieldActive;

            shieldActivator();
        }
        else if(shieldActive == true && playerShield <= 0)
        {
            Shield.SetActive(false);
            audioSource.PlayOneShot(shieldOFFClip,1f);
            shieldActive = false;
        }

        if(playerShield < playerShieldStart && !shieldActive)
        {
            StartCoroutine(ShieldRegenerate());
        }
        else if(shieldActive)
        {
            StartCoroutine(ShieldDeplete());
        }
    }

    public void TakeDamage(int damage)
    {
        if(playerShield > 0 && shieldActive == true)
        {
            playerShield -= damage * 10;
        }
        else
        {
            playerHealth -= damage;
        }

        if(playerHealth < 0)
        {
            playerShield = 0;
        }

        if(playerShield < 0)
        {
            playerShield = 0;
        }

        if(playerHealth <= 0) 
        {
            Invoke(nameof(SelfDestruct), 0.5f);
        }

        updateHealthAndShield();
    }

    public void addToMaxHealth(int healthUpgradeNum)
    {
        if(!healthUpgradeI && healthUpgradeNum == 1)
        {
            healthUpgradeI = true;
            playerHealthStart = playerHealthStart + 15f;
        }
        
        if(!healthUpgradeII && healthUpgradeNum == 2)
        {
            healthUpgradeII = true;
            playerHealthStart = playerHealthStart + 20f;
        }
        
        if(!healthUpgradeIII && healthUpgradeNum == 3)
        {
            healthUpgradeIII = true;
            playerHealthStart = playerHealthStart + 25f;
        }

        updateHealthAndShield();
    }

    public void addToMaxShield(int shieldUpgradeNum)
    {
        if(!shieldUpgradeI && shieldUpgradeNum == 1)
        {
            shieldUpgradeI = true;
            playerShieldStart = playerShieldStart + 15f;
        }
        
        if(!shieldUpgradeII && shieldUpgradeNum == 2)
        {
            shieldUpgradeII = true;
            playerShieldStart = playerShieldStart + 20f;
        }
        
        if(!shieldUpgradeIII && shieldUpgradeNum == 3)
        {
            shieldUpgradeIII = true;
            playerShieldStart = playerShieldStart + 25f;
        }

        updateHealthAndShield();
    }

    public void addToCurrentShield(int shieldToAdd)
    {
        playerShield += shieldToAdd;

        if(playerShield > playerShieldStart)
        {
            playerShield = playerShieldStart;
        }

        updateHealthAndShield();
    }

    public void addToCurrentHealth(int healthToAdd)
    {
        playerHealth += healthToAdd;

        if(playerHealth > playerHealthStart)
        {
            playerHealth = playerHealthStart;
        }

        updateHealthAndShield();
    }

    public void updateHealthAndShield()
    {
        playerShieldText.text = Mathf.Round((playerShield/playerShieldStart * playerShieldStart)).ToString();
        playerShieldSlider.value = playerShield/playerShieldStart * 10;

        playerHealthText.text = Mathf.Round((playerHealth/playerHealthStart * playerHealthStart)).ToString();
        playerHealthSlider.value = playerHealth/playerHealthStart * 10;

        if(playerHealth >= 80/100 * playerHealthStart)
        {
            targetlight.color = ColorAlive;
        }
        else if(playerHealth < 80/100 * playerHealthStart && playerHealth >= 50/100 * playerHealthStart)
        {
            targetlight.color = ColorDamaged;
        }
        else if(playerHealth < 50/100 * playerHealthStart && playerHealth >= 0)
        {
            targetlight.color = ColorDead;
        }
    }

    public void shieldActivator()
    {
        if(shieldActive == true)
        {
            Shield.SetActive(true);
            shieldActive = true;
            audioSource.PlayOneShot(shieldONClip,1f);
        }
        else if(shieldActive == true && playerShield <= 0)
        {
            Shield.SetActive(false);
            shieldActive = false;
            audioSource.PlayOneShot(shieldOFFClip,1f);
        }
        else
        {
            Shield.SetActive(false);
            shieldActive = false;
            audioSource.PlayOneShot(shieldOFFClip,1f);
        }
    }

    public void SelfDestruct()
    {
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        isPlayerDead = true;
        Instantiate(DeathExplosion, transform.position, Quaternion.identity);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //Destroy(robotBody.transform.GetChild(0).gameObject);
        StartCoroutine(TriggerLoadingScreen());
    }

    public void revivePlayer(bool dead)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        isPlayerDead = false;
        if(playerHealth <= 0)
        {
            transform.position = reviveSpawnPoint.position;
        }

        if(dead)
        {
            playerHealth = playerHealthStart-1/5*playerHealthStart;
        }
    }

    IEnumerator TriggerLoadingScreen()
    {
        yield return new WaitForSeconds(5f);
        revivePlayer(true);
        menu.SetActive(true);
    }

    private IEnumerator ShieldDeplete()
    {
        yield return new WaitForSeconds(1f);
        playerShield -= 0.01f;
        updateHealthAndShield();
    }

    private IEnumerator ShieldRegenerate()
    {
        yield return new WaitForSeconds(1f);
        playerShield += 0.01f;
        updateHealthAndShield();
    }
}
