using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private void OnValidate() {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquippableItem _item, out EquippableItem _previousItem){
        for(int i=0;i<equipmentSlots.Length;i++){
            if(equipmentSlots[i].EquipmentType == _item.EquipmentType){
                _previousItem = equipmentSlots[i].equipItem;
                equipmentSlots[i].equipItem = _item;
                equipmentSlots[i].RefreshUI();
                return true;
            }
        }
        _previousItem = null;
        return false;
    }

     public bool RemoveItem(EquippableItem _item){
        for(int i=0;i<equipmentSlots.Length;i++){
            if(equipmentSlots[i].EquipmentType == _item.EquipmentType){
                equipmentSlots[i].equipItem = null;
                equipmentSlots[i].RefreshUI();
                return true;
            }
        }
        return false;
    }
}
