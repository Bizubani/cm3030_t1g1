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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pNoise = Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        robotAnimation.SetFloat(blend, pNoise);
    }
}
