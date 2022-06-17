using UnityEngine;
using UnityEngine.UI;

public class BuildingView : MapItemView
{
    [SerializeField] private GameObject _progressUI;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _resourceIcon;

    private void Start()
    {
        if (model is Building) ((Building)model).BuildingChanged += OnBuildingChanged;

        _progressUI.SetActive(false);
    }

    private void OnBuildingChanged(Recipe recipe, int queeNumber)
    {
        if(recipe == null)
        {
            _progressUI.SetActive(false);            
        }
        else
        {
            _progressUI.SetActive(true);
            _resourceIcon.sprite = SpriteStorage.Instance.GetSpriteByName(recipe.Output.Name);
            _fill.fillAmount = recipe.GetProgress();
        }
    }
}
