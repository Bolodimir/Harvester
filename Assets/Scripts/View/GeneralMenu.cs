using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMenu : MonoBehaviour
{
    UIController Controller;


    public void OnResearchButtonClick()
    {
        Controller.UIclick();
        FindObjectOfType<GridModel>().ExpandGrid(new Vector2(4, 4));
    }
    public void OnResourceButtonClick()
    {
        Controller.OpenResourceMenu();
        Controller.UIclick();
    }
    public void OnBuildButtonClick()
    {
        Controller.UIclick();
        Controller.OpenBuildMenu();
    }
    public void OnDestroyButtonClick()
    {
        Controller.UIclick();
        Controller.OpenDestroyMenu();
    }


    void Start()
    {
        Controller = UIController.Instance;
    }

    void Update()
    {
        
    }
}
