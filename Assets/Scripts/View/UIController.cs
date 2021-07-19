using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    CameraBehaviour MainCamera;
    [SerializeField] GameObject GeneralMenu;
    [SerializeField] GameObject ItemMenu;
    [SerializeField] GameObject BuildMenu;

    private void CloseAllMenus()
    {
        GeneralMenu.SetActive(false);
        ItemMenu.SetActive(false);
        BuildMenu.SetActive(false);
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
    public void OpenBuildMenu()
    {
        CloseAllMenus();
        BuildMenu.SetActive(true);
    }
    public void NoUIclick()
    {
        OpenGeneralMenu();
        MainCamera.UnLockControls();
    }
    public void UIclick()
    {
        MainCamera.LockControls();
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        OpenGeneralMenu();
        MainCamera = Camera.main.gameObject.GetComponent<CameraBehaviour>();
    }

    void Update()
    {
        
    }
}
