using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public Resource[] Input;
    public Resource Output;
    public float TimeToProduce;

    private Building _building;
    private float _timeInProduction;
    private float _productionProgress; // [0;1]    

    public bool _productionStarted = false;
    public Recipe(Resource[] input, Resource output, float TTP, Building building)
    {
        Input = new Resource[input.Length];
        for(int i = 0; i < input.Length; i++)
        {
            Input[i] = input[i];
        }
        _building = building;
        Output = output;
        TimeToProduce = TTP;
    }

    public void SetBuilding(Building building)
    {
        _building = building;
    }

    public string GetName()
    {
        return Output.Name;
    }

    public float GetProgress()
    {
        return _productionProgress;
    }

    public string GetInput()
    {
        string result = string.Empty;
        foreach (Resource res in Input)
        {
            result += $"{res.Number} {res.Name} ({Stats.Instance.GetOneResource(res).Number}) \n";
        }
        return result;
    }

    public Recipe Copy()
    {
        return new Recipe(Input, Output, TimeToProduce, _building);
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
        _productionStarted = true;
        return true;
    } 
    
    public void UpdateProduction(float deltaTime)
    {
        if (!_productionStarted) return;
        _timeInProduction += deltaTime;
        _productionProgress = _timeInProduction / TimeToProduce;
        if(_productionProgress >= 1)
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
        _building.RecipeFinished();
    }

    public void CancelProduction()
    {
        foreach (Resource res in Input)
        {
            Stats.Instance.Deposit(res);
        }
    }
}
