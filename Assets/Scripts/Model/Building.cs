using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MapItem
{
    [SerializeField] Resource[] Cost;
    [SerializeField] private Recipe[] recipies;
    Recipe CurrentRecipe;

    public void StartRecipe()
    {

    }

    public void Delete()
    {
        
    }
}
