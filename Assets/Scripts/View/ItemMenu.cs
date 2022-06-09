using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : AbstractMenu
{
    [SerializeField] GameObject RecipeContainer;
    [SerializeField] GameObject RecipeCardPrefab;
    [SerializeField] MakeMenu makeMenu;
    [SerializeField] CurrentRecipeMenu currentRecipeMenu;

    GameObject[] RecipeCardObjects;
    Building CurrentBuilding;

    public void UpdateRecipeCards()
    {
        if (RecipeCardObjects != null)
        {
            foreach (GameObject go in RecipeCardObjects)
            {
                Destroy(go);
            }
        }

        Recipe[] recipies = CurrentBuilding.getRecipies();
        RecipeCardObjects = new GameObject[recipies.Length];
        for (int i = 0; i < recipies.Length; i++)
        {
            RecipeCardObjects[i] = Instantiate(RecipeCardPrefab);
            RecipeCardObjects[i].transform.SetParent(RecipeContainer.transform, false);
            Sprite sprite = SpriteStorage.Instance.GetSpriteByName(recipies[i].GetName());
            RecipeCardObjects[i].GetComponent<RecipeCard>().Initialize(sprite, recipies[i].GetName(), this);
        }
    }

    public void SetBuilding(Building building)
    {
        if(CurrentBuilding != null)
        {
            CurrentBuilding.BuildingChanged -= OnBuildingChanged;
        }        
        CurrentBuilding = building;
        CurrentBuilding.BuildingChanged += OnBuildingChanged;

        CancelMakeMenu();
        currentRecipeMenu.SetMenu(null, "No recipe", 0.5f, 0);
        UpdateRecipeCards();
    }

    public void OnBackButtonClick()
    {
        UIController.Instance.UIclick();
        UIController.Instance.OpenGeneralMenu();
    }

    public void OpenMakeMenuByName(string name) 
    {
        makeMenu.gameObject.SetActive(true);
        currentRecipeMenu.gameObject.SetActive(false);

        Recipe MakeRecipe = CurrentBuilding.GetRecipeByName(name);
        Sprite sprite = SpriteStorage.Instance.GetSpriteByName(name);
        makeMenu.Initialize(sprite, MakeRecipe.GetName(), MakeRecipe.GetInput(), this, MakeRecipe);
    }

    public void CancelMakeMenu()
    {
        makeMenu.gameObject.SetActive(false);
        currentRecipeMenu.gameObject.SetActive(true);
    }

    public void MakeItem(string name, int number, bool isInfinite)
    {
        CurrentBuilding.SetQueue(name, number, isInfinite);
        CancelMakeMenu();
    }

    void OnBuildingChanged(Recipe curRecipe, int QueueNumber)
    {
        string Name;
        float Progress;
        int NumberOfItems;
        Sprite sprite;
        if (curRecipe == null)
        {
            Name = "No recipe";
            Progress = 1f;
            NumberOfItems = 0;
            sprite = null;
        }
        else
        {
            Name = curRecipe.GetName();
            Progress = curRecipe.GetProgress();
            NumberOfItems = QueueNumber;
            sprite = SpriteStorage.Instance.GetSpriteByName(Name);
        }
        currentRecipeMenu.SetMenu(sprite,Name,Progress,NumberOfItems);
    }
}
