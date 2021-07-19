using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundCatcher : MonoBehaviour, IPointerDownHandler
{
    UIController Controller;

    void Start()
    {
        Controller = UIController.Instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Controller.NoUIclick();
    }
}
