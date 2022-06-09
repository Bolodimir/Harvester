using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenu : AbstractMenu
{
    UIController Controller;

    [SerializeField] GridModel model;
    [SerializeField] GridView view;
    void Start()
    {
        Controller = UIController.Instance;
    }
    void Update()
    {
        
    }
    public void OnDestroyButtonClicked()
    {
        view.ChooseBuildingForDestroying();
    }
    public void OnBackButtonClicked()
    {
        view.CancelDestroyMode();
        Controller.UIclick();
        Controller.OpenGeneralMenu();
    }
    private void OnEnable()
    {
        view.InitiateDestroyMode();
    }
}
