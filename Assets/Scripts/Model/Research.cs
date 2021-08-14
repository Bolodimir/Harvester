using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Research
{
    [SerializeField] private int _maxProgress;
    [SerializeField] private string _name;
    [SerializeField] private float _increaseValue;
    [SerializeField] private Resource _cost;

    private int _currentProgress;
    private float _value;

    Research()
    {
        _currentProgress = 0;
        _value = 0;
    }
    public float GetValue()
    {
        return _value;
    }
    public string GetName()
    {
        return _name;
    }
    public Resource GetCost()
    {
        return _cost;
    }
    public int GetMaxProgress()
    {
        return _maxProgress;
    }
    public int GetProgress()
    {
        return _currentProgress;
    }
    public void Upgrade()
    {
        if (_currentProgress == _maxProgress) return;
        _currentProgress++;
        _value += _increaseValue;
        if (Stats.Instance.Check(_cost))
        {
            Stats.Instance.Withdraw(_cost);
        }
    }
}
