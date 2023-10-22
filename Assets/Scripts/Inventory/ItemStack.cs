using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    private Item item;
    private int amount;

    public ItemStack(Item _item, int _amount = 1)
    {
        item = _item;
        amount = _amount;
    }

    public void SetAmount(int _amount){
        amount = _amount; 
    }

    public Item GetItem() { return item; }
    public int GetAmount() { return amount; }
}
