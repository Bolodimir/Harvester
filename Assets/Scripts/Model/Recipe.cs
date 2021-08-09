using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    string Name;

    public Resource[] Input;
    public Resource Output;
    public float TimeToProduce;
    Building building;
    float TimeInProduction;
    float ProductionProgress; // [0;1]    

    bool ProductionStarted = false;
    public Recipe(Resource[] input, Resource output, float TTP, Building building)
    {
        Input = new Resource[input.Length];
        for(int i = 0; i < input.Length; i++)
        {
            Input[i] = input[i];
        }
        this.building = building;
        Output = output;
        TimeToProduce = TTP;

        Name = Output.Name;
    }
    public bool StartProduction()
    {
        for(int i = 0; i < Input.Length; i++)
        {
            if(Stats.Instance.Check(Input[i]))
            {
                continue;
            }
            return false;
        }
        for (int i = 0; i < Input.Length; i++)
        {
            
            Stats.Instance.Withdraw(Input[i]);
            UpdateProduction(0);
        }
        ProductionStarted = true;
        return true;
    }    
    public void UpdateProduction(float deltaTime)
    {
        if (ProductionStarted == false) return;
        TimeInProduction += deltaTime;
        ProductionProgress = TimeInProduction / TimeToProduce;
        if(ProductionProgress >= 1)
        {
            FinishProduction();
        }
    }
    public void FinishProduction()
    {
        if(!Stats.Instance.Deposit(Output))
        {
            Stats.Instance.AddResource(Output);
        }
        building.RecipeFinished();
    }
    public Recipe Copy()
    {
        return new Recipe(Input, Output, TimeToProduce, building);
    }
    public string GetName()
    {
        return Output.Name;
    }
    public float GetProgress()
    {
        return ProductionProgress;
    }
    public void CancelProduction()
    {
        foreach(Resource res in Input)
        {
            Stats.Instance.Deposit(res);
        }
    }
    public string GetInput()
    {
        string result = string.Empty;
        foreach(Resource res in Input)
        {
            result += $"{res.Number} {res.Name} ({Stats.Instance.GetOneResource(res).Number}) \n";
        }
        return result;
    }
    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
