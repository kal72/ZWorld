using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private void OnValidate() {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquipmentItem _item, out EquipmentItem _previousItem){
        for(int i=0;i<equipmentSlots.Length;i++){
            if(equipmentSlots[i].ItemType == _item.itemType){
                _previousItem = equipmentSlots[i].equipItem;
                equipmentSlots[i].equipItem = _item;
                equipmentSlots[i].RefreshUI();
                return true;
            }
        }
        _previousItem = null;
        return false;
    }

     public bool RemoveItem(EquipmentItem _item){
        for(int i=0;i<equipmentSlots.Length;i++){
            if(equipmentSlots[i].ItemType == _item.itemType){
                equipmentSlots[i].equipItem = null;
                equipmentSlots[i].RefreshUI();
                return true;
            }
        }
        return false;
    }
}
