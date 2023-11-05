
public interface IItemContainer
{
	public bool CanAddItem(Item _item, int _amount = 1);
	public void AddItem(Item _item, int _amount = 1);
	public bool RemoveItem(Item _item, int _amount = 1);
    public ItemStack GetItem(Item _item);
	public int ItemCount(Item _item);
	//void Clear();
}
