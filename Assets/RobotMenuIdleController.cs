using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMenuIdleController : MonoBehaviour
{
    public Animator robotAnimation;
    public string blend = "Blend";
    // Distance covered per second along X axis of Perlin plane.
    float xScale = 0.1f;    
    // Start is called before the first frame update

    private bool CharacterMenuOn;
    void Start()
    {
        stopAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if(CharacterMenuOn == true)
        {
            float pNoise = Mathf.PerlinNoise(Time.time * xScale, 0.0f);
            robotAnimation.SetFloat(blend, pNoise);
        }
    }

    public void triggerAnimation()
    {
        CharacterMenuOn = true;
        robotAnimation.enabled = true;
    }

    public void stopAnimation()
    {
        CharacterMenuOn = false;
        robotAnimation.enabled = false;
    }
}
