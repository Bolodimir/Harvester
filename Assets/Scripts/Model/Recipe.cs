using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    Building building;
    Resource[] Input;
    Resource Output;
    float TimeToProduce;
    float TimeInProduction;
    float ProductionProgress; // [0;1]    

    public Recipe(Resource[] input, Resource output, float TTP)
    {
        Input = new Resource[input.Length];
        for(int i = 0; i < input.Length; i++)
        {
            Input[i] = input[i];
        }

        Output = output;
        TimeToProduce = TTP;
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
        // Add to the list of active recipies
        return true;
    }    
    public void UpdateProduction(float deltaTime)
    {
        TimeInProduction += deltaTime;
        ProductionProgress = TimeInProduction / TimeToProduce;
        if(ProductionProgress >= 1)
        {
            FinishProduction();
        }
    }
    public void FinishProduction()
    {
        if(Stats.Instance.Deposit(Output))
        {
            return;
        }
        else
        {
            Stats.Instance.AddResource(Output);
        }
    }
}
