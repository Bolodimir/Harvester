using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    public static Stats Instance; //Singletone

    private Resource[] resources;

    public delegate void StatsEventHandler();
    public event StatsEventHandler StatsChanged;

    public Research[] Researches;

    public Stats()
    {        
        if(Instance == null)
        {            
            resources = new Resource[0];
        }
        Instance = this;
    }

    public bool Withdraw(Resource value) //if has enough resources, withdraws them and returns false
    {
        if (value.Number <= 0) return true;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == value.Name)
            {
                if(resources[i].Number >= value.Number)
                {
                    resources[i].Number -= value.Number;
                    StatsChanged?.Invoke();
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
    public bool Deposit(Resource value) // if has mentioned resource category adds number to it
    {
        if (value.Number <= 0) return false;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == value.Name)
            {
                resources[i].Number += value.Number;
                StatsChanged?.Invoke();
                return true;
            }
        }
        return false;
    }
    public void AddResource(Resource value) // adds resource category
    {
        Resource[] newResources = new Resource[resources.Length + 1];
        for(int i = 0; i < resources.Length; i++)
        {
            newResources[i] = resources[i];
        }
        newResources[newResources.Length - 1] = value;
        resources = newResources;
        StatsChanged?.Invoke();
    }
    public bool Check(Resource value) // Check if has enough resources
    {
        if (value.Number <= 0) return true;

        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == value.Name)
            {
                if (resources[i].Number >= value.Number)
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
    public Resource[] GetResources()
    {
        return resources;
    }
    public Resource GetOneResource(Resource value)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].Name == value.Name)
            {
                return resources[i];
            }
        }
        return new Resource("",0);
    }
}
