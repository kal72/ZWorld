using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [Header("Game Events")]
    public BaseItemSlotEventChannel OnBeginDragChannel;
    public BaseItemSlotEventChannel OnDragChannel;
    public BaseItemSlotEventChannel OnEndDragChannel;

    private Color dragColor = new Color(1, 1, 1, 0.5f);

    Vector2 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //InventoryManager.Instance.ItemDragging = this;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
        image.color = dragColor;
        OnBeginDragChannel.Publish(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //image.transform.position = Input.mousePosition;
        //Debug.Log("on drag");
        OnDragChannel.Publish(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop item");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag");
        //image.transform.position = originalPosition;
        //InventoryManager.Instance.ItemDragging = null;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
        image.color = normalColor;
        OnEndDragChannel.Publish(this);
    }
}
