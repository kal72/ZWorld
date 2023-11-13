using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject CanvasBG;
    public GameObject CanvasHUD;
    public GameObject MenuPanel;
    public GameObject InventoryPanel;
    public GameObject CraftingPanel;
    public TransferInventoryPanel TransferInventoryPanel;
    public ItemTooltip ItemTooltip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleMenu();
        }

    }

    private void setCancasBG(bool status)
    {
        CanvasBG.SetActive(status);
        CanvasHUD.SetActive(!status);
        KeyBinding.Instance.IsLock = status;
    }

    public void ToggleMenu(){
        bool status = !MenuPanel.activeSelf;
        MenuPanel.SetActive(status);
        setCancasBG(status);
    }

    public void OpenInventory() {
        ToggleMenu();
        InventoryPanel.SetActive(true);
        setCancasBG(true);
        InventoryManager.Instance.RefreshListUI();
    }

    public void CloseInventory() {
        InventoryPanel.SetActive(false);
        setCancasBG(false);
    }

    public void OpenCrafting(bool status){
        if(status){
            CraftingPanel.SetActive(true);
            if (MenuPanel.activeSelf)
            {
                ToggleMenu();
            }
        }else{
            CraftingPanel.SetActive(false);
        }

        setCancasBG(status);
    }

    public void ShowTransferInventory(bool status, IItemContainer otherContainer)
    {
        TransferInventoryPanel.Show(status, otherContainer);
        setCancasBG(status);
    }
}
