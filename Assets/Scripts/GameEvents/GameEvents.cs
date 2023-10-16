using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public EventAction InventoryOpenEvent;
    public EventAction InventoryCloseEvent;

    private void OnValidate() {
        if (InventoryOpenEvent == null) {
            throw new MissingFieldException();
        } 

        if (InventoryCloseEvent == null) {
            throw new MissingFieldException();
        } 
    }

    private void Awake() {
        Instance = this;
    }

    // public event Action<Item> onInvetoryTriggerSelect;
    // public void InventoryTiggerSelect(Item _item)
    // {
    //     if (onInvetoryTriggerSelect != null){
    //         onInvetoryTriggerSelect(_item);
    //         onInvetoryTriggerSelect?.Invoke(_item);
    //     }
    // }

    public void InventoryOpen(){
        InventoryOpenEvent.PublishEvent();
    }

    public void InventoryClose(){
        InventoryOpenEvent.PublishEvent();
    }
}
