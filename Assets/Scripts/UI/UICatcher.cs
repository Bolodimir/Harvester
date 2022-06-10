using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICatcher : MonoBehaviour, IPointerDownHandler
{
    UIController Controller;

    void Start()
    {
        Controller = UIController.Instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Controller.UIclick();
    }
}
