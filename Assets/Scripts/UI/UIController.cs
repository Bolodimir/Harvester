using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    CameraBehaviour MainCamera;
    [SerializeField] MainMenu _generalMenu;
    [SerializeField] ItemMenu _itemMenu;
    [SerializeField] BuildMenu _buildMenu;
    [SerializeField] DestroyMenu _destroyMenu;
    [SerializeField] ResourceMenu _resourceMenu;
    [SerializeField] ResearchMenu _researchMenu;
    [SerializeField] DebugMenu _debugMenu;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OpenGeneralMenu();
        MainCamera = Camera.main.gameObject.GetComponent<CameraBehaviour>();
    }
    private void CloseAllMenus()
    {
        _generalMenu.Close();
        _itemMenu.Close();
        _buildMenu.Close();
        _destroyMenu.Close();
        _resourceMenu.Close();
        _researchMenu.Close();
        _debugMenu.Close();
    }
    public void OpenItemMenu(Building building)
    {
        CloseAllMenus();
        _itemMenu.gameObject.SetActive(true);
        _itemMenu.SetBuilding(building);
    }

    public void OpenGeneralMenu()
    {
        CloseAllMenus();
        _generalMenu.Open();
    }

    public void OpenBuildMenu()
    {
        CloseAllMenus();
        _buildMenu.Open();
    }

    public void OpenDestroyMenu()
    {
        CloseAllMenus();
        _destroyMenu.Open();
    }

    public void OpenResourceMenu()
    {
        CloseAllMenus();
        _resourceMenu.Open();
    }

    public void OpenResearchMenu()
    {
        CloseAllMenus();
        _researchMenu.Open();
    }

    public void OpenDebugMenu()
    {
        CloseAllMenus();
        _debugMenu.Open();
    }

    public void NoUIclick()
    {
        MainCamera.UnLockControls();
    }

    public void UIclick()
    {
        MainCamera.LockControls();
    } 
}
