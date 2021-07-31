using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MapItem
{
    [SerializeField] int HP;
    [SerializeField] Resource Loot;
    public void GetDamage()
    {
        HP--;
        if(HP == 0)
        {
            if (!Stats.Instance.Deposit(Loot))
            {
                Stats.Instance.AddResource(Loot);
            }
            Destroy(gameObject);
        }
    }

    public void Delete()
    {

    }
    public override void Action()
    {
        GetDamage();
    }
}
