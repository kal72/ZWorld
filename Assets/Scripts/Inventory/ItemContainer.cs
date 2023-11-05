using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    [SerializeField] protected List<ItemStack> items = new List<ItemStack>();
    [SerializeField] protected int maxSlots = 0;

    public virtual void AddItem(Item _item, int _amount = 1)
    {
        var itemStack = getLeastItems(_item.ID);
        if (itemStack != null)
        {
            int remains = itemStack.Item.MaximumStacks - itemStack.Amount;
            int index = items.IndexOf(itemStack);
            if (_amount <= remains)
            {
                itemStack.Increase(_amount);
                items[index] = itemStack;
            }
            else
            {

                itemStack.Increase(remains);
                items[index] = itemStack;
                remains = _amount - remains;
                items.Add(new ItemStack(_item, remains));
            }

            return;
        }

        items.Add(new ItemStack(_item, _amount));
    }

    public virtual bool CanAddItem(Item _item, int _amount = 1)
    {
        int spendSize = maxSlots - items.Count;
        if (!_item.IsStackable)
        {
            return spendSize >0 ? true : false;
        }
        
        var result = getLeastItems(_item.ID);
        
        if (result != null)
        {
            int totalAmount = result.Amount + _amount;
            int totalMaxAmount = result.Item.MaximumStacks + (result.Item.MaximumStacks * spendSize);

            if (totalAmount <= totalMaxAmount)
            {
                return true;
            }
        }
        else
        {
            int totalMaxAmount = _item.MaximumStacks * spendSize;
            if (_amount <= totalMaxAmount)
            {
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        items.Clear();
    }

    public virtual void CopyFrom(List<ItemStack> _items)
    {
        items = _items;
    }

    public virtual List<ItemStack> GetItems(string _itemID)
    {
        return items.FindAll(x => x.Item.ID == _itemID);
    }

    public virtual List<ItemStack> GetItems()
    {
        return items;
    }

    public virtual int ItemAmount(string _itemID)
    {
        int amount = 0;
        items.ForEach(x => {
            if (x.Item.ID == _itemID) amount += x.Amount;
        });

        return amount;
    }

    public virtual bool RemoveItem(string _itemID, int _amount = 1)
    {
        bool result = false;
        var itemStacks = GetItems(_itemID);
        foreach(ItemStack itemStack in itemStacks)
        {
            if (_amount < itemStack.Amount)
            {
                int index = items.IndexOf(itemStack);
                itemStack.Decrease(_amount);
                items[index] = itemStack;

                result = true;
                break;
            }
            else
            {
                RemoveItem(itemStack);
                var remains = _amount - itemStack.Amount;
                if (remains == 0)
                {
                    result = true;
                    break;
                }
            }
        }

        return result;
    }

    public virtual bool RemoveItem(ItemStack _itemStack)
    {
        return items.Remove(_itemStack);
    }

    public void RemoveItem(int _index)
    {
       items.RemoveAt(_index);
    }

    private ItemStack getLeastItems(string _itemID)
    {
       return items.Find(x => x.Item.ID == _itemID && x.Amount < x.Item.MaximumStacks);
    }
}

