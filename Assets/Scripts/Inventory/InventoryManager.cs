using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public EquipmentPanel EquipmentPanel;
    public ItemDetailPanel ItemDetailPanel;
    public List<Item> inventoryItems = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject ItemDetail;
    public GameObject ObjItemSelected = null;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Add(Item _item)
    {
        inventoryItems.Add(_item);
    }

    public void Remove(Item _item)
    {
        inventoryItems.Remove(_item);
    }

    public void RefreshListUI()
    {
        ObjItemSelected = null;
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in inventoryItems)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = item.Icon;
            obj.GetComponent<Button>().onClick.AddListener(() => SelectItem(item, obj));

            if (ObjItemSelected == null)
            {
                SelectItem(item, obj);
            }
        }
    }

    public void SelectItem(Item _item, GameObject _obj)
    {
        if (ObjItemSelected != _obj)
        {
            if (ObjItemSelected != null)
            {
                ObjItemSelected.transform.Find("ItemSelected").gameObject.SetActive(false);
            }
            _obj.transform.Find("ItemSelected").gameObject.SetActive(true);
            ObjItemSelected = _obj;

            ItemDetailPanel.UpdateInfo(_item);
        }
    }

    public void Equip(EquipmentItem _item)
    {
        Debug.Log("Equip");

        EquipmentItem previousItem;
        if (inventoryItems.Remove(_item))
        {
            if (EquipmentPanel.AddItem(_item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventoryItems.Add(previousItem);
                }
            }
            else
            {
                inventoryItems.Add(_item);
            }
            RefreshListUI();
        }
    }

    public void Unequip(EquipmentItem _item)
    {
        if (EquipmentPanel.RemoveItem(_item))
        {
            inventoryItems.Add(_item);
            RefreshListUI();
        }
    }

    public void ShowItemInfo(Item _item)
    {
        if (_item != null)
            ItemDetailPanel.UpdateInfo(_item);
        Debug.Log("ShowItemInfo");
    }

}
