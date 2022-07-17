﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwayShake : MonoBehaviour
{
    #region variables
    public bool camShakeAcive = true; //on or off
    [Range(0, 1)]
    [SerializeField] float swayAmount;
    [SerializeField] float swayMult = 16; //the power of the shake
    [SerializeField] float swayMag = 0.8f; //the range of movment
    [SerializeField] float swayRotMag = 17f; //the rotational power
    [SerializeField] float swayDepthMag = 0.6f; //the depth multiplier
    [SerializeField] float swayDecay = 1.3f; //how quickly the shake falls off
    [SerializeField] bool InfiniteSway = true; //how quickly the shake falls off


    float timeCounter = 0; //counter stored for smooth transition
    #endregion

    #region accessors
    public float Trauma //accessor is used to keep trauma within 0 to 1 range
    {
        get
        {
            return swayAmount;
        }
        set
        {
            swayAmount = Mathf.Clamp01(value);
        }
    }
    #endregion

    #region methods
    //Get a perlin float between -1 & 1, based off the time counter.
    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2f;
    }

    //use the above function to generate a Vector3, different seeds are used to ensure different numbers
    Vector3 GetVec3()
    {
        return new Vector3(
            GetFloat(1),
            GetFloat(10),
            //deapth modifier applied here
            GetFloat(100) * swayDepthMag
            );
    }
    
    private void Update ()
    {
        if (camShakeAcive && Trauma > 0)
        {
            //increase the time counter (how fast the position changes) based off the traumaMult and some root of the Trauma
            timeCounter += Time.deltaTime * Mathf.Pow(Trauma,0.3f) * swayMult;
            //Bind the movement to the desired range
            Vector3 newPos = GetVec3() * swayMag * Trauma;
            transform.localPosition = newPos;
            //rotation modifier applied here
            transform.localRotation = Quaternion.Euler(newPos * swayRotMag);

            if(InfiniteSway)
            {
                //decay faster at higher values
                Trauma += Time.deltaTime * swayDecay * (Trauma + 0.3f);
            }
            else
            {
                //decay faster at higher values
                Trauma -= Time.deltaTime * swayDecay * (Trauma + 0.3f);
            }
        }
        else
        {
            //lerp back towards default position and rotation once shake is done
            Vector3 newPos = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(newPos * swayRotMag);
        }
    }
    #endregion
}
