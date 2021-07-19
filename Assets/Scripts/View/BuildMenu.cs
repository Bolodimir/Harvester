using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    UIController Controller;
    public void OnBuildButtonClick()
    {
        Controller.UIclick();
    }
    public void OnBackButtonClick()
    {
        Controller.UIclick();
        Controller.OpenGeneralMenu();
    }
    private void Start()
    {
        Controller = UIController.Instance;
    }

    private void OnEnable()
    {
        print("enable");
    }
}
