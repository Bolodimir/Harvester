using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemView : RaycastTarget
{
    [SerializeField] protected MapItem model;
    [SerializeField] private GameObject _popUpPrefab;

    private bool IsToBuild = true;
    private UIController controller;

    public virtual void Start()
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

    public void ChangeIsToBuild()
    {
        IsToBuild = false;
    }

    public void CreateResourcePopUp(Resource resource)
    {
        var popUp = Instantiate(_popUpPrefab, transform.position, transform.rotation).GetComponent<PopUp>();
        popUp.Init(resource);
    }
}
