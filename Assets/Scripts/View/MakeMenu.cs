using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakeMenu : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] TextMeshProUGUI NumberOfItems;

    ItemMenu itemMenu;
    private Recipe _curRecipe;

    private void Start()
    {
        Stats.Instance.StatsChanged += OnStatsChanged;
    }

    public void Initialize(Sprite icon, string name, string price, ItemMenu menu, Recipe curRecipe)
    {
        Icon.sprite = icon;
        Name.text = name;
        Price.text = price;
        itemMenu = menu;
        NumberOfItems.text = "1";
        _curRecipe = curRecipe;
    }

    public void OnBackButtonClicked()
    {
        itemMenu.CancelMakeMenu();
    }

    public void OnMakeButtonClicked()
    {
        string itemName = Name.text;
        int itemNumber = 1;
        bool itemIsInfinite = false;
        if(string.Equals(NumberOfItems.text, "Infinity"))
        {
            itemIsInfinite = true;
        }
        else
        {
            itemNumber = Convert.ToInt32(NumberOfItems.text);
        }
        itemMenu.MakeItem(Name.text, itemNumber, itemIsInfinite);
    }

    public void OnMinusButtonClicked()
    {
        if (string.Equals(NumberOfItems.text, "Infinity"))
        {
            NumberOfItems.text = "1";
        }
        else
        {
            int num = Convert.ToInt32(NumberOfItems.text);
            if (num == 1) return;
            NumberOfItems.text = (num - 1).ToString();
        }
    }

    public void OnPlusButtonClicked()
    {
        if(string.Equals(NumberOfItems.text, "Infinity"))
        {
            NumberOfItems.text = "1";
        }
        else
        {
            int num = Convert.ToInt32(NumberOfItems.text);
            if (num == 99) return;
            NumberOfItems.text = (num + 1).ToString();
        }
    }

    public void OnInfinityButtonClicked()
    {
        NumberOfItems.text = "Infinity";
    }

    private void OnStatsChanged()
    {
        Price.text = _curRecipe.GetInput();
    }
}
