using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuNav : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CharacterMenu;
    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(true);
        }
    }
}
