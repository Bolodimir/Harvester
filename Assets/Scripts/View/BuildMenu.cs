using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    UIController Controller;
    [SerializeField] GridView view;
    public void OnBuildButtonClick()
    {
        Controller.UIclick();
        view.InitiateBuildingForPlacing("Forge");
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

    }
}
