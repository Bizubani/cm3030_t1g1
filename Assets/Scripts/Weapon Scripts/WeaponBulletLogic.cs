using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WeaponBulletLogic : MonoBehaviour
{
    //Gun Name
    public String gunName;

    //bullet
    public GameObject bullet;

    //Bullt force
    public float shootForce, upwardForce;
    
    //Gun Stats
    [Header("GUN STATS")]
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, magazineAmount;
    public bool allowButtonHold;
    
    int bulletsLeft, bulletsShot;

    //Recoil
    [Header("RECOIL")]
    public Rigidbody playerRigidBody;
    public float recoilForce;
    public CameraShake cameraShake1;
    public CameraShake cameraShake2;
    public CameraShake audioListener1;

    //Bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera playerCamera;
    public List<Transform> allAttackPoints;

    //Graphics
    [Header("GRAPHICS")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    public TextMeshProUGUI magazineDisplay;
    public TextMeshProUGUI gunNameDisplay;

    //bug fixing
    public bool allowInvoke = true;

    //Audio
    [Header("AUDIO")]
    AudioSource audioSource;

    private bool inCooldown;

    //Settings
    [Header("WEAPON SETTINGS")]
    private WeaponSettings weaponSettings;
    public int weaponNumber;
    private bool weaponStatus = true;

    private void Start()
    {
        weaponSettings = GameObject.Find("Weapon Settings").GetComponent<WeaponSettings>();
        //make sure magazine is full
        weaponStatus = weaponSettings.updateWeaponStatus(weaponNumber);
        if(weaponStatus == true)
        {
            bulletsLeft = magazineSize;
            updateWeaponSettings();
        }
        else if(weaponStatus == false)
        {
            retrieveWeaponSettings();
        }

        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();

        cameraShake1 = GameObject.Find("CM vcam2").GetComponent<CameraShake>();
        cameraShake2 = GameObject.Find("CM vcam2").GetComponent<CameraShake>();
        audioListener1 = GameObject.Find("Audio Listener").GetComponent<CameraShake>();

        playerRigidBody = GameObject.Find("Player Character").GetComponent<Rigidbody>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ammunitionDisplay = GameObject.Find("Ammo Count Text").GetComponent<TextMeshProUGUI>();
        magazineDisplay = GameObject.Find("Mag Count Text").GetComponent<TextMeshProUGUI>();
        gunNameDisplay = GameObject.Find("Gun Name Text").GetComponent<TextMeshProUGUI>();

        /////
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists
        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " | " + magazineSize / bulletsPerTap);
            if(magazineAmount <= -1)
            {
                magazineDisplay.SetText("NO AMMO");
            }
            else
            {
                magazineDisplay.SetText(magazineAmount.ToString());
            }
            gunNameDisplay.SetText("- "+ gunName + " -");
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !weaponStatus)
        {
            updateWeaponSettings();
        }
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding Input
        if(allowButtonHold)
        {
            shooting = Input.GetButton("Fire1");
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && magazineAmount > -1)
        {
            Reload();
        }
        
        //Reload automatically when trying to shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0 && magazineAmount > -1)
        {
            Reload();
        }

        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0 && magazineAmount > -1)
        {
            //Set bullets shot 0
            bulletsShot = 0;

            Shoot();
            StartCoroutine(cameraShake1.Shake(0.1f,0.1f));
            StartCoroutine(cameraShake2.Shake(0.1f,0.1f));
            StartCoroutine(audioListener1.Shake(0.1f,0.1f));
        }
    }

    private void Shoot()
    {
        //If the Cooldown coroutine is not currently running, play the sound and start a new cooldown
        if(!inCooldown)
        {
            //Play sound
            ShootSound();
            StartCoroutine(Cooldown());
        }

        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);//A ray through the middle
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {   
            targetPoint = ray.GetPoint(75); //Just a point away from the player
        }

        //calculate direction from attack Point to target Point
        for (var i = 0; i < allAttackPoints.Count; i++)
        {
            Vector3 directionWithoutSpread = targetPoint - allAttackPoints[i].position;

            //Calculate spread
            float x = UnityEngine.Random.Range(-spread, spread);
            float y = UnityEngine.Random.Range(-spread, spread);

            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Add Spread to Direction

            GameObject currentBullet = Instantiate(bullet, allAttackPoints[i].position, Quaternion.identity);

            //Rotate Bullet to shoot Direction
            currentBullet.transform.forward = directionWithSpread.normalized;
            //Use The Force Luke
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            //For bouncing Grenades
            currentBullet.GetComponent<Rigidbody>().AddForce(playerCamera.transform.up * upwardForce, ForceMode.Impulse);


            bulletsLeft--;

            if(bulletsLeft < 0)
            {
                bulletsLeft = 0;
            }

            bulletsShot++;

            //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
            if(allowInvoke)
            {
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;

                //Add recoil to player
                playerRigidBody.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
            }

            //If more than one bulletPer Tap make sure to repeat shoot function
            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking Again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        magazineAmount -= 1;
        if(magazineAmount <= -1)
        {
            bulletsLeft = 0;
        }
        else
        {
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        }
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private IEnumerator Cooldown()
    {
        //Set the cooldown flag to true, wait for the cooldown time to pass, then turn the flag to false
        inCooldown = true;
        yield return new WaitForSeconds(timeBetweenShooting);
        inCooldown = false;
    }

    private void ShootSound()
    {
        audioSource.Play();
    }

    private void retrieveWeaponSettings()
    {
        bulletsLeft = weaponSettings.retrieveCurrentAmmo(weaponNumber);
        magazineAmount = weaponSettings.retrieveCurrentMagazine(weaponNumber);
    }

    private void updateWeaponSettings()
    {
        weaponSettings.updateCurrentAmmo(weaponNumber, bulletsLeft);
        weaponSettings.updateCurrentMagazine(weaponNumber, magazineAmount);
    }
}
