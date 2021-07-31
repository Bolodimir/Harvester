using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    UIController Controller;
    [SerializeField] GameObject CardOfBuilding;
    [SerializeField] GridView view;
    [SerializeField] GridModel model;

    [SerializeField] GameObject BuildingContainer;


    public void OnBuildButtonClick()
    {
        if (view.IsBuildingMode())
        {
            view.ChoosePlaceForBuilding();
        }
        Controller.UIclick(); 
    }
    public void OnBackButtonClick()
    {
        view.CancelBuildingMode();
        Controller.UIclick();
        Controller.OpenGeneralMenu();
    }
    private void Start()
    {
        Controller = UIController.Instance;
        Building[] buildings = model.GetBuildings();
        for(int i = 0; i < buildings.Length; i++)
        {
            GameObject cardObject = Instantiate(CardOfBuilding);
            BuildingCard card = cardObject.GetComponent<BuildingCard>();

            cardObject.transform.SetParent(BuildingContainer.transform,false);
            card.Initialize(buildings[i].Icon, buildings[i].Name, buildings[i].Description, buildings[i].Name, this) ;
        }
    }
    public void OnBuildingPressed(string BuildingName)
    {
        Controller.UIclick();
        view.CancelBuildingMode();
        view.InitiateBuildingForPlacing(BuildingName);
    }
}
