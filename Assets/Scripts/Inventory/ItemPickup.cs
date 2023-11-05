using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    // Start is called before the first frame update
    void Pickup()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(gameObject);
    }

    void OnMouseDown() {
        Debug.Log("clicked");
        Pickup();
    }
}
