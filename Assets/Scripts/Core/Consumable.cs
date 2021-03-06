using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MapItem
{
    [SerializeField] private ConsumableView _consumableView;
    [SerializeField] private int HP;
    [SerializeField] private Resource Loot;

    private GridView view;

    public void GetDamage()
    {
        HP--;
        if (HP == 0)
        {
            if (!Stats.Instance.Deposit(Loot))
            {
                Stats.Instance.AddResource(Loot);
            }
            _consumableView.CreateResourcePopUp(Loot);
            view.DeleteObject(transform.position);
        }
    }

    public override void Action()
    {
        GetDamage();
    }

    public void SetView(GridView view)
    {
        this.view = view;
    }
}
