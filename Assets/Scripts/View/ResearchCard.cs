using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResearchCard : MonoBehaviour
{
    [SerializeField] private Transform ProgressBar;
    [SerializeField] private GameObject ProgressUnitPrefab;
    [SerializeField] private TextMeshProUGUI ResearchName;
    [SerializeField] private Color DefaultColor;
    [SerializeField] private Color FilledColor;


    private ResearchMenu _menu;
    private Image[] ProgressUnits;

    public void Initialize(string ResearchName, int MaxProgress, ResearchMenu menu)
    {
        _menu = menu;
        this.ResearchName.text = ResearchName;
        ProgressUnits = new Image[MaxProgress];
        for(int i = 0; i < MaxProgress;i++)
        {
            Transform newUnit= Instantiate(ProgressUnitPrefab).transform;
            newUnit.SetParent(ProgressBar, false);
            ProgressUnits[i] = newUnit.GetComponent<Image>();
        }
    }
    public void OnBuyButtonClicked()
    {
        _menu.BuyUpgrade(ResearchName.text);
        int Progress = 0;
        foreach (Research rsch in Stats.Instance.Researches)
        {
            if (string.Equals(ResearchName.text, rsch.GetName()))
            {
                Progress = rsch.GetProgress();
            }
        }
        for(int i = 0; i < Progress; i++)
        {
            ProgressUnits[i].color = FilledColor;
        }
    }
}
