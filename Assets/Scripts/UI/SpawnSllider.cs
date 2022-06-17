using UnityEngine;
using UnityEngine.UI;

public class SpawnSllider : MonoBehaviour
{
    [SerializeField] private Image _slider;
    [SerializeField] private GridModel _model;

    private void Update()
    {
        _slider.fillAmount = _model.GetSpawnProgress();
    }
}
