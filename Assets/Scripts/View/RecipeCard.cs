using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeCard : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Name;

    ItemMenu itemMenu;

    public void OnMakeButtonClicked()
    {
        itemMenu.OpenMakeMenuByName(Name.text);
    }
    public void Initialize(Sprite icon, string name, ItemMenu menu)
    {
        Icon.sprite = icon;
        Name.text = name;
        itemMenu = menu;
    }
}
