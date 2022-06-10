using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchMenu : AbstractMenu
{
    private UIController controller;

    [SerializeField] private Transform ResearchContainer;
    [SerializeField] private ResearchCard CardPrefab;

    public void Start()
    {
        controller = UIController.Instance;
        for(int i = 0; i < Stats.Instance.Researches.ResearchList.Count; i++)
        {
            Research rsch = Stats.Instance.Researches.ResearchList[i];
            ResearchCard card = Instantiate(CardPrefab);
            card.transform.SetParent(ResearchContainer,false);
            card.Initialize(rsch.GetName(), rsch.GetMaxProgress(), this);
        }
    }

    public void OnBackButtonClick()
    {
        controller.OpenGeneralMenu();
    }

    public bool TryBuyUpgrade(string name)
    {
        var research = Stats.Instance.Researches.Find(name);
        if (research != null) return research.TryUpgrade();
        return false;
    }
}
