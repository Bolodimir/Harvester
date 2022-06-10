using TMPro;
using UnityEngine;

public class AddResourcesMenu : AbstractMenu
{
    [SerializeField] private TextMeshProUGUI _numberText;

    private string _resourceType = "Wood";
    private int _resourceNumber = 1;

    public void AddResources()
    {
        Resource toAdd = new Resource(_resourceType, _resourceNumber);
        if (!Stats.Instance.Deposit(toAdd))
        {
            Stats.Instance.AddResource(toAdd);
        }
    }

    public void ChangeType(string type)
    {
        _resourceType = type;
    }

    public void ChangeNumber(int number)
    {
        _resourceNumber = number;
        _numberText.text = _resourceNumber.ToString();
    }

    public void Increment() => ChangeNumber(_resourceNumber + 1);

    public void Decrement() => ChangeNumber(_resourceNumber - 1);
}
