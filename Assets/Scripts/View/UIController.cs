using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    CameraBehaviour MainCamera;
    [SerializeField] GameObject GeneralMenu;
    [SerializeField] ItemMenu itemMenu;
    [SerializeField] GameObject BuildMenu;
    [SerializeField] GameObject DestroyMenu;
    [SerializeField] GameObject ResourceMenu;

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
        GeneralMenu.SetActive(false);
        itemMenu.gameObject.SetActive(false);
        BuildMenu.SetActive(false);
        DestroyMenu.SetActive(false);
        ResourceMenu.SetActive(false);

    }
    public void OpenItemMenu(Building building)
    {
        CloseAllMenus();
        itemMenu.gameObject.SetActive(true);
        itemMenu.SetBuilding(building);
    }

    public void OpenGeneralMenu()
    {
        CloseAllMenus();
        GeneralMenu.SetActive(true);
    }
    public void OpenBuildMenu()
    {
        CloseAllMenus();
        BuildMenu.SetActive(true);
    }
    public void OpenDestroyMenu()
    {
        CloseAllMenus();
        DestroyMenu.SetActive(true);
    }
    public void OpenResourceMenu()
    {
        CloseAllMenus();
        ResourceMenu.SetActive(true);
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
