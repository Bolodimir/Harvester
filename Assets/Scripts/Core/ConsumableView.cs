using UnityEngine;

public class ConsumableView : MapItemView
{
    [SerializeField] private GameObject[] _stages;
    [SerializeField] private GameObject _particles;

    public override void OnPressed()
    {
        base.OnPressed();
        Destroy(Instantiate(_particles,transform.position,transform.rotation),2);

        foreach(var stage in _stages)
        {
            if (stage.activeSelf)
            {
                stage.SetActive(false);
                break;
            }
        }
    }
}
