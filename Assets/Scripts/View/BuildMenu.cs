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
            card.Initialize(buildings[i].Icon, buildings[i].Name, buildings[i].GetPriceString(), this) ;
        }
    }
    public void OnBuildingPressed(string BuildingName)
    {
        Controller.UIclick();
        Building newBuilding= model.GetItem(BuildingName).GetComponent<Building>();
        if (newBuilding == null) return;
        foreach(Resource res in newBuilding.GetPrice())
        {
            if (!Stats.Instance.Check(res))
            {
                return;
            }
        }
        foreach (Resource res in newBuilding.GetPrice())
        {
            Stats.Instance.Withdraw(res);
        }
        view.CancelBuildingMode();
        view.InitiateBuildingForPlacing(BuildingName);
    }
}