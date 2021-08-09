using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemView : RaycastTarget
{
    [SerializeField]MapItem model;
    bool IsToBuild = true;
    private UIController controller;
    void Start()
    {
        controller = UIController.Instance;
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
