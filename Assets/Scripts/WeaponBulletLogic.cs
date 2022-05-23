using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBulletLogic : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //Bullt force
    public float shootForce, upwardForce;
    
    //Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    
    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRigidBody;
    public float recoilForce;

    //Bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera twoPointFiveDimensionCamera;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing
    public bool allowInvoke = true;

    public void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists
        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " | " + magazineSize / bulletsPerTap);
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
        readyToShoot = false;

        //Find the exact hit position using a raycast
        //Ray ray = twoPointFiveDimensionCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray ray = twoPointFiveDimensionCamera.ScreenPointToRay(Input.mousePosition);//A ray through the middle
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

        //calculate direction from attack Point to target Poinnt
         Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Add Spread to Direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate Bullet to shoot Direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Use The Force Luke
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //For bouncing Grenades
        currentBullet.GetComponent<Rigidbody>().AddForce(twoPointFiveDimensionCamera.transform.up * upwardForce, ForceMode.Impulse);
        
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
}
