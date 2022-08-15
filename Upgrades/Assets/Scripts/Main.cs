using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    public int skillPoints;
    [SerializeField]
    public Text textElement;
    [SerializeField]
    public List<int> stats;
    int index;

    // Update is called once per frame
    void Update()
    {
        textElement.text =  "Skill points: " + skillPoints;        
    }

    public void UpdateStats(string type)
    {
        if (type == "Health")
        {
            stats[0] += 1;
        }
        else if(type == "Damage")
        {
            stats[1] += 1;
        }
        else if(type == "Speed")
        {
            stats[2] += 1;
        }
        else if(type == "ROF")
        {
            stats[3] += 1;
        }
        else if(type == "Power Length")
        {
            stats[4] += 1;
        }
    }
}
