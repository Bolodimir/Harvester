using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    CameraBehaviour MainCamera;
    [Header("Menus")]
    [SerializeField] private MainMenu _generalMenu;
    [SerializeField] private ItemMenu _itemMenu;
    [SerializeField] private BuildMenu _buildMenu;
    [SerializeField] private DestroyMenu _destroyMenu;
    [SerializeField] private ResourceMenu _resourceMenu;
    [SerializeField] private ResearchMenu _researchMenu;
    [SerializeField] private DebugMenu _debugMenu;
    [SerializeField] private GameObject _castlePopUp;
    [Header("Components")]
    [SerializeField] private GridModel _gridModel;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OpenGeneralMenu();
        MainCamera = Camera.main.gameObject.GetComponent<CameraBehaviour>();
        _gridModel.CastleBuilt += OpenCastlePopUp;
    }
    public void CloseAllMenus()
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

    public void OpenCastlePopUp()
    {
        _castlePopUp.SetActive(true);
    }

    public void CloseCastlePopUp()
    {
        _castlePopUp.SetActive(false);
    }
}
