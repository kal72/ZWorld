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

    public bool IsItemDrag;

    public Item[] ExampleItems;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        initDummyData();
    }

    private void initDummyData(){
        for(int i=0; i<ExampleItems.Length; i++)
        {
            AddItem(ExampleItems[i], 50);
        }
    }

    public void RefreshListUI()
    {
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
        }
    }


    public void TransferToOther(BaseItemSlot itemSlot, IItemContainer itemContainer)
    {
        itemContainer.AddItem(itemSlot.Item.GetCopy(), itemSlot.Amount);
        RemoveItem(itemSlot.Index);
        RefreshListUI();
    }
}
