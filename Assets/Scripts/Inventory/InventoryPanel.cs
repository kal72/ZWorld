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
    public BaseItemSlotEventChannel OnDropChannel;
    public BaseItemSlotEventChannel OnDropWorldChannel;
    public BaseItemSlotEventChannel OnEnterClickChannel;
    public BaseItemSlotEventChannel OnExitClickChannel;
    public VoidEventChannel OnDropVoidChannel;

    public BaseItemSlot dragItemSlot;
    Vector2 originalPosition;

    private void OnEnable() {
        OnBeginDragChannel.Subscribe(onBeginDrag);
        OnDragChannel.Subscribe(onDrag);
        OnEndDragChannel.Subscribe(onEndDrag);
        OnDropChannel.Subscribe(onDrop);
        OnDropVoidChannel.Subscribe(onDropToWorld);
        OnEnterClickChannel.Subscribe(onEnterClick);
        OnExitClickChannel.Subscribe(onExitClick);
    }

    private void OnDisable()
    {
        OnBeginDragChannel.Unsubscribe(onBeginDrag);
        OnDragChannel.Unsubscribe(onDrag);
        OnEndDragChannel.Unsubscribe(onEndDrag);
        OnDropVoidChannel.Unsubscribe(onDropToWorld);
        OnDropChannel.Unsubscribe(onDrop);
        OnEnterClickChannel.Unsubscribe(onEnterClick);
        OnExitClickChannel.Unsubscribe(onExitClick);
    }


    private void onBeginDrag(BaseItemSlot baseItemSlot)
    {
        Debug.Log("on begin drag");
        if (baseItemSlot.Item == null)
        {
            return;
        }

        dragItemSlot = baseItemSlot;
        originalPosition = Input.mousePosition;
        dragItemImage.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        dragItemImage.sprite = baseItemSlot.Item.Icon;
        dragItemImage.GetComponentInChildren<TextMeshProUGUI>().text = baseItemSlot.Amount.ToString();
        dragItemImage.gameObject.SetActive(true);

        onExitClick(null);
        InventoryManager.Instance.IsItemDrag = true;
    }

    private void onDrag(BaseItemSlot baseItemSlot)
    {
        if (baseItemSlot == null)
        {
            return;
        }

        dragItemImage.transform.position = Input.mousePosition;
    }

    private void onEndDrag(BaseItemSlot baseItemSlot)
    {
        Debug.Log("on end drag");
        dragItemImage.transform.position = originalPosition;
        dragItemImage.gameObject.SetActive(false);
        InventoryManager.Instance.IsItemDrag = false;
        dragItemSlot = null;
    }

    private void onDrop(BaseItemSlot baseItemSlot)
    {
        if (dragItemSlot == baseItemSlot || dragItemSlot.Item == null )
        {
            return;
        }

        if (baseItemSlot == null)
        {
            if (dragItemSlot is UsableSlot)
            {
                InventoryManager.Instance.AddItem(dragItemSlot.Item, dragItemSlot.Amount);
                InventoryManager.Instance.RefreshListUI();
                dragItemSlot.Item = null;
            }
            Debug.Log("drop null");
            return;
        }

        if (baseItemSlot is UsableSlot)
        {
            if (baseItemSlot.Item != null)
            {
                //swap item to slot
                Item dropItem = baseItemSlot.Item;
                int dropAmount = baseItemSlot.Amount;
                Item dragItem = dragItemSlot.Item;
                int dragAmount = dragItemSlot.Amount;

                baseItemSlot.Item = dragItem;
                baseItemSlot.Amount = dragAmount;

                if (dragItemSlot is UsableSlot)
                {
                    dragItemSlot.Item = dropItem;
                    dragItemSlot.Amount = dropAmount;
                }
                else
                {
                    InventoryManager.Instance.RemoveItem(dragItemSlot.Index);
                    InventoryManager.Instance.AddItem(dropItem, dropAmount);
                    InventoryManager.Instance.RefreshListUI();
                }
            }
            else
            {
                //add item to slot
                baseItemSlot.Item = dragItemSlot.Item;
                baseItemSlot.Amount = dragItemSlot.Amount;
                if (dragItemSlot is not UsableSlot)
                {
                    InventoryManager.Instance.RemoveItem(dragItemSlot.Index);
                    InventoryManager.Instance.RefreshListUI();
                }
                
                dragItemSlot.Item = null;
            }
        }

        onEndDrag(null);
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
        if(!InventoryManager.Instance.IsItemDrag)
            UIController.Instance.ItemTooltip.Show(itemSlot.Item);
    }

    //Hide Tooltip
    private void onExitClick(BaseItemSlot itemSlot)
    {
        if (!InventoryManager.Instance.IsItemDrag)
            UIController.Instance.ItemTooltip.Hide();
    }
}
