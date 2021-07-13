using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance {get;private set;}
    [SerializeField] GameObject GeneralMenu;
    [SerializeField] GameObject ItemMenu;

    private void CloseAllMenus()
    {
        GeneralMenu.SetActive(false);
        ItemMenu.SetActive(false);
    }
    public void OpenItemMenu()
    {
        CloseAllMenus();
        ItemMenu.SetActive(true);
    }

    public void OpenGeneralMenu()
    {
        CloseAllMenus();
        GeneralMenu.SetActive(true);
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OpenGeneralMenu();
    }

    void Update()
    {
        
    }
}
