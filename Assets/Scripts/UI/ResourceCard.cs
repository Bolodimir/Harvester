using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCard : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI ResourceName;
    [SerializeField] TextMeshProUGUI ResourceCount;

    public void Initialize(Sprite icon, Resource resource)
    {
        Icon.sprite = icon;
        ResourceName.text = resource.Name;
        ResourceCount.text = resource.Number.ToString();
    }
}
