using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemView : MonoBehaviour
{
    private UIController controller;
    void Start()
    {
        controller = UIController.Instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnPressed();
        }
    }

    public void OnPressed()
    {
        controller.OpenItemMenu();
    }

    public void Select()
    {

    }

    public void DeSelect()
    {

    }
}
