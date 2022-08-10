using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuTabController : MonoBehaviour
{   
    public int menuid;
    private bool selectedMenu = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        selectedMenu = true;
        MenuSectionHandler.menuID = menuid;
    }

    public void Deselected()
    {
        selectedMenu = false;
        MenuSectionHandler.menuID = 0;
    }
}
