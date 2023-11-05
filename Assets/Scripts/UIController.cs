using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
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

    public void ToggleMenu(){
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    public void OpenInventory() {
        ToggleMenu();
        InventoryPanel.SetActive(true);
        InventoryManager.Instance.RefreshListUI();
    }

    public void CloseInventory() {
        InventoryPanel.SetActive(false);
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
    }

    public void ShowTransferInventory(bool status, IItemContainer otherContainer)
    {
        TransferInventoryPanel.Show(status, otherContainer);
    }
}
