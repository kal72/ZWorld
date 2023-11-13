using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UsableSlot : ItemSlot, IDropHandler
{
    [Header("Shortcut")]
    public KeyCode Key;
    [Space]
    [SerializeField] TMP_Text textKey;

    [Header("Game Event Publishers")]
    public BaseItemSlotEventChannel OnItemDropChannel;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (textKey == null) textKey = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("on drop");
        if (OnItemDropChannel != null)
            OnItemDropChannel.Publish(this);
    }

    public void Activate()
    {
        if (Item == null) return;

        if (Item is UsableItem)
        {
            UsableItem usable = Item as UsableItem;
            usable.Use(Character.Instance);
            Amount -= 1;

            if(Amount == 0)
            {
                usable.Destroy();
            }
        }
    }
}
