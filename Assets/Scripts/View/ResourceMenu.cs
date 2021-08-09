using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMenu : MonoBehaviour
{
    [SerializeField] GameObject ResourceCardPrefab;
    [SerializeField] GameObject ResourceCardContainer;
    UIController controller;
    GameObject[] ResourceCardObjects;
    private void Start()
    {
        controller = UIController.Instance;
        OnStatsChange();
        Stats.Instance.StatsChanged += OnStatsChange;
    }
    public void OnBackButtonClick()
    {
        controller.OpenGeneralMenu();
    }
    public void OnStatsChange()
    {
        Resource[] resources = Stats.Instance.GetResources();
        if (resources == null) return;

        if (ResourceCardObjects == null)
        {
            ResourceCardObjects = new GameObject[resources.Length];
        }
        else
        {
            foreach (GameObject card in ResourceCardObjects)
            {
                Destroy(card);
            }
        }

        ResourceCardObjects = new GameObject[resources.Length];
        for (int i = 0; i < resources.Length; i++)
        {
            GameObject CurrentCard = Instantiate(ResourceCardPrefab);
            CurrentCard.transform.SetParent(ResourceCardContainer.transform, false);
            Sprite sprite = SpriteStorage.Instance.GetSpriteByName(resources[i].Name);
            CurrentCard.GetComponent<ResourceCard>().Initialize(sprite, resources[i]);
            ResourceCardObjects[i] = CurrentCard;
        }
    }
}
