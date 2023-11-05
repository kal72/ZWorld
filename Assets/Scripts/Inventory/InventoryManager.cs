using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : ItemContainer
{
    public static InventoryManager Instance;
    public EquipmentPanel EquipmentPanel;
    public Transform ItemContent;
    public GameObject ItemPrefab;
    public GameObject ItemDetail;
    public GameObject PouchPrefab;
    public GameObject ObjItemSelected = null;

    public bool IsItemDrag;

    public Item ExampleLog;
    public Item ExampleOre;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        initDummyData();
    }

    private void initDummyData(){
        AddItem(ExampleOre, 100);
        AddItem(ExampleLog, 100);
    }

    public void RefreshListUI()
    {
        ObjItemSelected = null;
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var itemStack in items)
        {
            GameObject obj = Instantiate(ItemPrefab, ItemContent);

            var baseItemSlot = obj.GetComponent<ItemSlot>();
            baseItemSlot.Item = itemStack.Item;
            baseItemSlot.Amount = itemStack.Amount;
            baseItemSlot.Index = items.IndexOf(itemStack);

            obj.GetComponent<Button>().onClick.AddListener(delegate { Debug.Log("Click"); });
            Debug.Log("item");

            //if (ObjItemSelected == null)
            //{
            //    SelectItem(itemStack, obj);
            //}
        }
    }

    public void SelectItem(ItemStack _itemStack, GameObject _obj)
    {
        Debug.Log("click");
        if (ObjItemSelected != _obj)
        {
            if (ObjItemSelected != null)
            {
                ObjItemSelected.transform.Find("ItemSelected").gameObject.SetActive(false);
            }
            _obj.transform.Find("ItemSelected").gameObject.SetActive(true);
            ObjItemSelected = _obj;
        }
    }

    public void Equip(ItemStack _itemStack)
    {
        Debug.Log("Equip");

        EquippableItem previousItem;
        
        if (_itemStack != null)
        {
            RemoveItem(_itemStack);
            if (EquipmentPanel.AddItem((EquippableItem)_itemStack.Item, out previousItem))
            {
                if (previousItem != null)
                {
                    AddItem(previousItem);
                }
            }
            else
            {
                AddItem(_itemStack.Item);
            }

            RefreshListUI();
        }
    }

    public void Unequip(EquippableItem _item)
    {
        AddItem(_item);
        RefreshListUI();
    }

    public void ShowItemInfo(ItemStack _item)
    {
        Debug.Log("ShowItemInfo");
    }

    public void TransferToOther(BaseItemSlot itemSlot, IItemContainer itemContainer)
    {
        itemContainer.AddItem(itemSlot.Item.GetCopy(), itemSlot.Amount);
        RemoveItem(itemSlot.Index);
        RefreshListUI();
    }
}
