
using System.Collections.Generic;

public interface IItemContainer
{
    public void CopyFrom(List<ItemStack> _items);
    public void AddItem(Item _item, int _amount = 1);
    public bool CanAddItem(Item _item, int _amount = 1);
	public bool RemoveItem(string _itemID, int _amount = 1);
    public bool RemoveItem(ItemStack _itemStack);
    public void RemoveItem(int _index);
    public int ItemAmount(string _itemID);
    public List<ItemStack> GetItems(string _itemID);
    public List<ItemStack> GetItems();
    public void Clear();
}
