using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject InventoryPanel;

    public void ToggleMenu(){
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    public void OpenInventory() {
        InventoryPanel.SetActive(true);
        InventoryManager.Instance.RefreshListUI();
    }

    public void CloseInventory() {
        InventoryPanel.SetActive(false);
    }
}
