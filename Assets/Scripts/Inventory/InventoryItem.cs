using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    private Item item;
    private int qty;

    public InventoryItem(Item _item, int _qty)
    {
        item = _item;
        qty = _qty;
    }

    public Item GetItem() { return item; }
    public int GetQty() { return qty; }
}
