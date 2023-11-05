using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject InventoryPanel;
    public GameObject CraftingPanel;

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
             ToggleMenu();
            CraftingPanel.SetActive(true);
        }else{
            CraftingPanel.SetActive(false);
        }
    }
}
