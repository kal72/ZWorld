using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TransferInventoryPanel : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] Transform LeftContent;
    [SerializeField] Transform RightContent;
    [SerializeField] GameObject ItemPrefab;
    [Space]
    [Header("Container")]
    [SerializeField] IItemContainer otherContainer;

    [Header("Game Event Subscribers")]
    public BaseItemSlotEventChannel OnRightClickChannel;
    public BaseItemSlotEventChannel OnEnterClickChannel;
    public BaseItemSlotEventChannel OnExitClickChannel;

    private bool isLock = true;

    private void OnEnable()
    {
        isLock = false;
        OnRightClickChannel.Subscribe(onRightClick);
        OnEnterClickChannel.Subscribe(onEnterClick);
        OnExitClickChannel.Subscribe(onExitClick);
    }

    private void OnDisable()
    {
        isLock = true;
        OnRightClickChannel.Unsubscribe(onRightClick);
        OnEnterClickChannel.Unsubscribe(onEnterClick);
        OnExitClickChannel.Unsubscribe(onExitClick);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLock && Input.GetKeyUp(KeyCode.Escape))
        {
            Show(false, null);
        }
    }

    public void Show(bool _status, IItemContainer _otherContainer)
    {
        if (_status && _otherContainer == null) return;

        gameObject.SetActive(_status);
        otherContainer = _otherContainer;

        if (_status)
        {
            RefreshUI();
        }
        else
        {
            destroyAllContent();
        }
    }

    public void RefreshUI()
    {
        destroyAllContent();

        //update other inventory list
        var otherItems = otherContainer.GetItems();
        for (int i=0; i<otherItems.Count; i++)
        {
            var itemStack = otherItems[i];
            GameObject obj = Instantiate(ItemPrefab, LeftContent);

            var itemSlot = obj.GetComponent<ItemSlot>();
            itemSlot.Item = itemStack.Item;
            itemSlot.Amount = itemStack.Amount;
            itemSlot.Index = i;
        }

        //update my inventory list
        var inventoryItems = InventoryManager.Instance.GetItems();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var itemStack = inventoryItems[i];
            GameObject obj = Instantiate(ItemPrefab, RightContent);

            var itemSlot = obj.GetComponent<ItemSlot>();
            itemSlot.Item = itemStack.Item;
            itemSlot.Amount = itemStack.Amount;
            itemSlot.Index = i;
        }
    }

    private void destroyAllContent()
    {
        foreach (Transform item in LeftContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in RightContent)
        {
            Destroy(item.gameObject);
        }
    }

    //Transfer item
    private void onRightClick(BaseItemSlot itemSlot)
    {
        Debug.Log(itemSlot.transform.parent.name);
        if (itemSlot.transform.parent.name == RightContent.name)
        {
            otherContainer.AddItem(itemSlot.Item.GetCopy(), itemSlot.Amount);
            InventoryManager.Instance.RemoveItem(itemSlot.Index);
        } else if (itemSlot.transform.parent.name == LeftContent.name)
        {
            InventoryManager.Instance.AddItem(itemSlot.Item.GetCopy(), itemSlot.Amount);
            otherContainer.RemoveItem(itemSlot.Index);
        }

        RefreshUI();
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
