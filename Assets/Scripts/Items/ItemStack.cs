using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public Item Item;
    public int Amount;

    public ItemStack(Item _item, int _amount = 1)
    {
        Item = _item;
        Amount = _amount;
    }

    public void SetAmount(int _amount){
        Amount = _amount; 
    }

    public void Increase(int _amount)
    {
        Amount += _amount;
    }

    public void Decrease(int _amount)
    {
        Amount -= _amount;
    }

    public Item GetItem() { return Item; }
    public int GetAmount() { return Amount; }
}
