using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    [SerializeField]
    public GameObject main;
    [SerializeField]
    public List<Image> upgradePoints;
    int skillPoints;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    public void upgradeStat()
    {
        skillPoints = main.GetComponent<Main>().skillPoints; 
        if (skillPoints > 0){
            if(index < upgradePoints.Count)
            {
                upgradePoints[index].color = Color.black;
                index += 1;
                main.GetComponent<Main>().skillPoints -= 1;
                main.GetComponent<Main>().UpdateStats(name);
            }
        }
    }
}
