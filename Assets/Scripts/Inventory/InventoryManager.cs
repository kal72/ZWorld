using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour, IItemContainer
{
    public static InventoryManager Instance;
    public EquipmentPanel EquipmentPanel;
    public ItemDetailPanel ItemDetailPanel;
    public List<ItemStack> inventoryItems = new List<ItemStack>();
    public Transform ItemContent;
    public GameObject ItemPrefab;
    public GameObject ItemDetail;
    public GameObject ObjItemSelected = null;

    public Item ExampleLog;
    public Item ExampleOre;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        init();
    }

    private void init(){
        AddItem(ExampleOre, 100);
        AddItem(ExampleLog, 100);
    }
    // Start is called before the first frame update
    public void AddItem(Item _item, int amount = 1)
    {
        Debug.Log("new item - "+_item.ID);
        inventoryItems.Add(new ItemStack(_item, amount));
    }

    public void RemoveItem(ItemStack _item)
    {
        inventoryItems.Remove(_item);
    }

    public bool RemoveItem(Item _item, int _amount = 1)
    {
        ItemStack itemStack = GetItem(_item);
        if (itemStack != null) {
            int totalAmount = itemStack.GetAmount() - _amount;
            if (_item.IsStackable){
                if (totalAmount > 0){
                    itemStack.SetAmount(totalAmount);
                    return true;
                }
            }

            return inventoryItems.Remove(itemStack);
        }
            
        return false;
    }

    public int ItemCount(Item _item){
        ItemStack itemStack = GetItem(_item);
        if (itemStack != null){
            return itemStack.GetAmount();
        } 

        return 0;
    }

    public bool CanAddItem(Item _item, int _amount){
        ItemStack itemStack = GetItem(_item);
        if (itemStack == null){
            return true;
        }

        if(_item.MaximumStacks <= (itemStack.GetAmount() + _amount)){
            return true;
        }

        return false;
    }

    public ItemStack GetItem(Item _item)
	{
		foreach (ItemStack inventoryItem in inventoryItems)
		{
			if (inventoryItem.GetItem().ID == _item.ID)
			{
				return inventoryItem;
			}
		}

		return null;
	}

    public void RefreshListUI()
    {
        ObjItemSelected = null;
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var itemStack in inventoryItems)
        {
            GameObject obj = Instantiate(ItemPrefab, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = itemStack.GetItem().Icon;
            var itemQty = obj.transform.Find("ItemQty").GetComponent<TextMeshProUGUI>();
            itemQty.text = "<mark=#00000090>"+itemStack.GetAmount()+"</mark>";
            obj.GetComponent<Button>().onClick.AddListener(() => SelectItem(itemStack, obj));

            if (ObjItemSelected == null)
            {
                SelectItem(itemStack, obj);
            }
        }
    }

    public void SelectItem(ItemStack _itemStack, GameObject _obj)
    {
        if (ObjItemSelected != _obj)
        {
            if (ObjItemSelected != null)
            {
                ObjItemSelected.transform.Find("ItemSelected").gameObject.SetActive(false);
            }
            _obj.transform.Find("ItemSelected").gameObject.SetActive(true);
            ObjItemSelected = _obj;

            ItemDetailPanel.UpdateInfo(_itemStack.GetItem());
        }
    }

    public void Equip(EquippableItem _item)
    {
        Debug.Log("Equip");

        EquippableItem previousItem;
        ItemStack itemStack = GetItem(_item); 
        if (itemStack != null)
        {
            RemoveItem(itemStack);
            if (EquipmentPanel.AddItem(_item, out previousItem))
            {
                if (previousItem != null)
                {
                    AddItem(previousItem);
                }
            }
            else
            {
                AddItem(_item);
            }

            RefreshListUI();
        }
    }

    public void Unequip(EquippableItem _item)
    {
        AddItem(_item);
        RefreshListUI();
    }

    public void ShowItemInfo(Item _item)
    {
        if (_item != null)
            ItemDetailPanel.UpdateInfo(_item);
        Debug.Log("ShowItemInfo");
    }
}
