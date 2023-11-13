using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] EquipmentSlot[] equipmentSlots;

    [Header("Game Events Subcriber")]
    public BaseItemSlotEventChannel OnDoubleClickChannel;

    private void OnEnable()
    {
        if (OnDoubleClickChannel != null)
            OnDoubleClickChannel.Subscribe(onDoubleClick);
    }

    private void OnValidate() {
        equipmentSlots = GetComponentsInChildren<EquipmentSlot>();
    }


    private void onDoubleClick(BaseItemSlot _itemSlot)
    {
        if (_itemSlot.Item == null) return;

        // remove equipment
        if(_itemSlot is EquipmentSlot)
        {
            Debug.Log(_itemSlot.Index);
            ((EquippableItem)equipmentSlots[_itemSlot.Index].Item).Unequip(Character.Instance);
            InventoryManager.Instance.AddItem(_itemSlot.Item.GetCopy());
            equipmentSlots[_itemSlot.Index].Item = null;
            InventoryManager.Instance.RefreshListUI();
            equipmentSlots[_itemSlot.Index].RefreshUI();
            return;
        }

        //Insert or swap equipment
        if (_itemSlot.Item is EquippableItem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].ItemType.ToString() == _itemSlot.Item.GetItemType())
                {
                    if (equipmentSlots[i].Item == null)
                    {
                        equipmentSlots[i].Item = ((EquippableItem)_itemSlot.Item).GetCopy();
                        equipmentSlots[i].Index = i;
                        InventoryManager.Instance.RemoveItem(_itemSlot.Index);
                        InventoryManager.Instance.RefreshListUI();
                        ((EquippableItem)equipmentSlots[i].Item).Equip(Character.Instance);
                    }
                    else
                    {
                        ((EquippableItem)equipmentSlots[i].Item).Unequip(Character.Instance);
                        InventoryManager.Instance.AddItem(((EquippableItem)equipmentSlots[i].Item).GetCopy());
                        equipmentSlots[i].Item = ((EquippableItem)_itemSlot.Item).GetCopy();
                        equipmentSlots[i].Index = i;
                        InventoryManager.Instance.RemoveItem(_itemSlot.Index);
                        InventoryManager.Instance.RefreshListUI();
                        ((EquippableItem)equipmentSlots[i].Item).Equip(Character.Instance);
                    }

                    equipmentSlots[i].RefreshUI();
                    return;
                }
            }
        }
    }
}
