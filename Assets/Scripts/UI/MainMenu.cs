using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : AbstractMenu
{
    UIController Controller;    

    void Start()
    {
        Controller = UIController.Instance;
    }

    void Update()
    {
        
    }

    public void OnResearchButtonClick()
    {
        Controller.OpenResearchMenu();
        Controller.UIclick();
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
}
