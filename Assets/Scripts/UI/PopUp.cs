using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _textComponent;

    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _fadeOutFraction; // [0;1]

    private float _creationTime;

    private void Start()
    {
        _creationTime = Time.time;
    }

    private void Update()
    {
        if (_lifeTime < Time.time - _creationTime) Destroy(gameObject);

        float lifeFraction = (Time.time - _creationTime) / _lifeTime;

        if(lifeFraction > _fadeOutFraction)
        {
            float fadeOut = (lifeFraction - _fadeOutFraction) / (1 - _fadeOutFraction);
            fadeOut = 1 - fadeOut;
            _canvasGroup.alpha = fadeOut;
        }

        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    public void Init(Resource resource)
    {
        _icon.sprite = SpriteStorage.Instance.GetSpriteByName(resource.Name);
        float total = Stats.Instance.GetOneResource(resource).Number;
        _textComponent.text = resource.Number.ToString() +"(" + total + ")";
    }
}
