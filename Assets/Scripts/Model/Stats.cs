using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public static Stats Instance; //Singletone

    private Resource[] resources;

    public Stats()
    {
        Instance = this;
        resources = new Resource[0];
    }

    public bool Withdraw(string Name, int Number) //if has enough resources, withdraws them and returns false
    {
        if (Number <= 0) return false;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == Name)
            {
                if(resources[i].Number >= Number)
                {
                    resources[i].Number -= Number;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
    public bool Deposit(string Name, int Number) // if has mentioned resource category adds number to it
    {
        if (Number <= 0) return false;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == Name)
            {
                resources[i].Number += Number;
                return true;
            }
        }
        return false;
    }
    public void AddResource(string Name, int Number) // adds resource category
    {
        Resource[] newResources = new Resource[resources.Length + 1];
        for(int i = 0; i < resources.Length; i++)
        {
            newResources[i] = resources[i];
        }
        newResources[newResources.Length - 1] = new Resource(Name, Number);
    }
    public bool Check(string Name, int Number) // Check if has enough resources
    {
        if (Number <= 0) return false;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == Name)
            {
                if (resources[i].Number >= Number)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
