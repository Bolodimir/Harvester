using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentRecipeMenu : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Queue;
    [SerializeField] Transform ProgressBar;

    public void SetMenu(Sprite icon, string name, float productionProgress, int QueueLength)
    {
        Icon.sprite = icon;
        Name.text = name;
        Queue.text = "Items in queue: " + QueueLength;
        ProgressBar.localScale = new Vector3
        (
            productionProgress,
            ProgressBar.localScale.y,
            ProgressBar.localScale.z
        );
    }
}
