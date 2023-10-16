using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{

    private void OnEnable() {
        GameEvents.Instance.InventoryOpenEvent.Subscribe(visibleObject);
        GameEvents.Instance.InventoryCloseEvent.Subscribe(unVisibleObject);
    }

    // Update is called once per frame
    private void visibleObject(){
        Debug.Log("event");
        gameObject.SetActive(true);
    }

    private void unVisibleObject(){
        Debug.Log("event");
        gameObject.SetActive(false);
    }
}
