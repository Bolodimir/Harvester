using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingCard : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Price;

    private BuildMenu Menu;

    public void Initialize(Sprite icon, string name,  string price, BuildMenu menu)
    {
        Icon.sprite = icon;
        Name.text = name;
        Price.text = price;

        Menu = menu;
    }
    public void OnCardPressed()
    {
        Menu.OnBuildingPressed(Name.text);
    }
}
