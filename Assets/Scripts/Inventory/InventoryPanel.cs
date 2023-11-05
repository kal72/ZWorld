using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] Image dragItemImage;

    [Header("Game Events")]
    public BaseItemSlotEventChannel OnBeginDragChannel;
    public BaseItemSlotEventChannel OnDragChannel;
    public BaseItemSlotEventChannel OnEndDragChannel;
    public BaseItemSlotEventChannel OnDropWorldChannel;
    public BaseItemSlotEventChannel OnEnterClickChannel;
    public BaseItemSlotEventChannel OnExitClickChannel;
    public VoidEventChannel OnDropChannel;

    Vector2 originalPosition;
    BaseItemSlot dragItemSlot;

    private void OnEnable() {
        OnBeginDragChannel.Subscribe(onBeginDrag);
        OnDragChannel.Subscribe(onDrag);
        OnEndDragChannel.Subscribe(onEndDrag);
        OnDropChannel.Subscribe(onDropToWorld);
        OnEnterClickChannel.Subscribe(onEnterClick);
        OnExitClickChannel.Subscribe(onExitClick);
    }

    private void OnDisable()
    {
        OnBeginDragChannel.Unsubscribe(onBeginDrag);
        OnDragChannel.Unsubscribe(onDrag);
        OnEndDragChannel.Unsubscribe(onEndDrag);
        OnDropChannel.Unsubscribe(onDropToWorld);
        OnEnterClickChannel.Unsubscribe(onEnterClick);
        OnExitClickChannel.Unsubscribe(onExitClick);
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

    private void onBeginDrag(BaseItemSlot baseItemSlot)
    {
        Debug.Log("on begin drag");
        dragItemSlot = baseItemSlot;
        originalPosition = Input.mousePosition;
        dragItemImage.transform.position = Input.mousePosition;
        dragItemImage.sprite = baseItemSlot.Item.Icon;
        dragItemImage.GetComponentInChildren<TextMeshProUGUI>().text = baseItemSlot.Amount.ToString();
        dragItemImage.gameObject.SetActive(true);

        InventoryManager.Instance.IsItemDrag = true;
    }

    private void onDrag(BaseItemSlot baseItemSlot)
    {
        dragItemImage.transform.position = Input.mousePosition;
    }

    private void onEndDrag(BaseItemSlot baseItemSlot)
    {
        Debug.Log("on end drag");
        dragItemSlot = null;
        dragItemImage.transform.position = originalPosition;
        dragItemImage.gameObject.SetActive(false);
        InventoryManager.Instance.IsItemDrag = false;
    }

    private void onDropToWorld()
    {
        Debug.Log("Drop to world");
        dragItemImage.sprite = null;
        dragItemImage.gameObject.SetActive(false);
        if (dragItemSlot != null)
            OnDropWorldChannel.Publish(dragItemSlot);
    }

    //Show Tooltip
    private void onEnterClick(BaseItemSlot itemSlot)
    {
        UIController.Instance.ItemTooltip.Show(itemSlot.Item);
    }

    //Hide Tooltip
    private void onExitClick(BaseItemSlot itemSlot)
    {
        UIController.Instance.ItemTooltip.Hide();
    }
}
