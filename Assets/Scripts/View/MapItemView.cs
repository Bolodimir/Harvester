using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemView : RaycastTarget
{
    bool IsToBuild = true; 
    private UIController controller;
    MapItem model;
    void Start()
    {
        controller = UIController.Instance;
        model = GetComponentInParent<MapItem>();
    }

    void Update()
    {

    }

    public override void OnPressed()
    {
        if (!IsToBuild)
        {
            model.Action();
        }
    }
    public void Select()
    {

    }
    public void DeSelect()
    {

    }
    public void ChangeIsToBuild()
    {
        IsToBuild = false;
    }
}
