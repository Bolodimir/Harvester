using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MapItem
{
    public delegate void BuildingEventHandler(Recipe curRecipe, int QueueNumber);
    public event BuildingEventHandler BuildingChanged;

    [SerializeField] private Resource[] Cost;
    [SerializeField] private Recipe[] recipies;
    private RecipeQueue CurrentQueue;
    private Recipe curRecipe;

    Recipe CurrentRecipe
    {
        get
        {
            return curRecipe;
        }
        set
        {
            curRecipe = value;
            if(curRecipe != null)
            {
                curRecipe.StartProduction();
                BuildingChanged?.Invoke(CurrentRecipe,CurrentQueue.number);
            }
        }
    }

    private void Start()
    {
        foreach(Recipe rec in recipies)
        {
            rec.SetBuilding(this);
        }
    }

    private void Update()
    {
        if (curRecipe != null)
        {
            if (!curRecipe._productionStarted) curRecipe.StartProduction();
            curRecipe.UpdateProduction(Time.deltaTime);
            if (CurrentQueue != null)
            {
                BuildingChanged?.Invoke(CurrentRecipe, CurrentQueue.number);
            }
        }
    }

    public override void Action()
    {
        UIController.Instance.OpenItemMenu(this);
    }

    public void SetQueue(RecipeQueue queue)
    {
        SetQueue(queue.recipe.GetName(), queue.number, queue.isInfinite);
    }

    public void SetQueue(string Name, int Number, bool isInfinite)
    {
        foreach(Recipe curRec in recipies)
        {
            if (string.Equals(Name, curRec.GetName()))
            {
                CurrentQueue = new RecipeQueue();
                CurrentQueue.recipe = curRec;
                CurrentQueue.number = Number;
                CurrentQueue.isInfinite = isInfinite;

                CurrentRecipe = CurrentQueue.recipe.Copy();
            }
        }
    }  
    
    public void RecipeFinished()
    {
        if (!CurrentQueue.isInfinite)
        {
            CurrentQueue.number--;
        }
        if(CurrentQueue.number <= 0)
        {
            CurrentQueue = null;
            CurrentRecipe = null;
            BuildingChanged?.Invoke(CurrentRecipe, 0);
        }
        else
        {
            CurrentRecipe = CurrentQueue.recipe.Copy();
        }
    }

    public void CancelQueue()
    {
        CurrentRecipe.CancelProduction();
        CurrentQueue = null;
        CurrentRecipe = null;
    }

    public RecipeQueue GetCurrentQueue()
    {
        return CurrentQueue;
    }

    public Recipe GetRecipeByName(string name)
    {
        foreach(Recipe recipe in recipies)
        {
            if (string.Equals(recipe.GetName(), name))
            {
                return recipe;
            }
        }
        return null;
    }

    public Recipe[] getRecipies()
    {
        return recipies;
    }

    public string GetPriceString()
    {
        string result = string.Empty;
        foreach (Resource res in Cost)
        {
            result += $"{res.Number} {res.Name} ({Stats.Instance.GetOneResource(res).Number}) \n";
        }
        return result;
    }

    public Resource[] GetPrice()
    {
        return Cost;
    }
}
