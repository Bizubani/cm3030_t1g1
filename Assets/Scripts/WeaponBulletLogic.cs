using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WeaponBulletLogic : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //Bullt force
    public float shootForce, upwardForce;
    
    //Gun Stats
    [Header("GUN STATS")]
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
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

    //bug fixing
    public bool allowInvoke = true;

    private GameObject CM;

    //Audio
    [Header("AUDIO")]
    AudioSource audioSource;

    private bool inCooldown;

    public void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        cameraShake1= GameObject.Find("CM vcam1").GetComponent<CameraShake>();
        cameraShake2 = GameObject.Find("CM vcam2").GetComponent<CameraShake>();
        audioListener1 = GameObject.Find("Audio Listener").GetComponent<CameraShake>();

        playerRigidBody = GameObject.Find("Player Character").GetComponent<Rigidbody>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ammunitionDisplay = GameObject.Find("AmmoCountText").GetComponent<TextMeshProUGUI>();

        /////
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        try 
        {
            CM = GameObject.Find("Menu");

            if (CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            MyInput();

            //Set ammo display, if it exists
            if(ammunitionDisplay != null)
            {
                ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " | " + magazineSize / bulletsPerTap);
            }
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
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
        
        //Reload automatically when trying to shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot 0
            bulletsShot = 0;

            Shoot();
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
        StartCoroutine(cameraShake1.Shake(0.10f,0.3f));
        StartCoroutine(cameraShake2.Shake(0.10f,0.3f));
        StartCoroutine(audioListener1.Shake(0.10f,0.3f));

        readyToShoot = false;

        //Find the exact hit position using a raycast
        //Ray ray = twoPointFiveDimensionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
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

            //Instantiate muzzle flash, if you have one
            /*
            if(muzzleFlash != null)
            {
                Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            }*/

            bulletsLeft--;
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
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
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
}
