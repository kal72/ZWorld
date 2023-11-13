using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected Image image;
	[SerializeField] protected TMP_Text amountText;

	[Header("Game Events (optional)")]
	public BaseItemSlotEventChannel OnRightClickChannel;
    public BaseItemSlotEventChannel OnEnterClickChannel;
    public BaseItemSlotEventChannel OnExitClickChannel;
	public BaseItemSlotEventChannel OnDoubleClickChannel;


    protected bool isPointerOver;

	protected Color normalColor = Color.white;
	protected Color disabledColor = new Color(1, 1, 1, 0);

	public int Index;
	protected Item _item;
	public Item Item {
		get { return _item; }
		set {
			_item = value;
			if (_item == null && Amount != 0) Amount = 0;

			if (_item == null) {
				image.sprite = null;
				image.color = disabledColor;
				Amount = 0;
			} else {
				image.sprite = _item.Icon;
				image.color = normalColor;
			}

			if (isPointerOver)
			{
				OnPointerExit(null);
				OnPointerEnter(null);
			}
		}
	}

	private int _amount;
	public int Amount {
		get { return _amount; }
		set {
			_amount = value;
			if (_amount < 0) _amount = 0;
			if (_amount == 0 && Item != null) Item = null;

			if (amountText != null)
			{
				amountText.enabled = _item != null && _amount > 1;
				if (amountText.enabled) {
					amountText.text = _amount.ToString();
				}
			}
		}
	}

	public virtual bool CanAddStack(Item item, int amount = 1)
	{
		return Item != null && Item.ID == item.ID;
	}

	public virtual bool CanReceiveItem(Item item)
	{
		return false;
	}

	protected virtual void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();

		if (amountText == null)
			amountText = GetComponentInChildren<TextMeshProUGUI>();

		Item = _item;
		Amount = _amount;
	}

	protected virtual void OnDisable()
	{
		if (isPointerOver)
		{
			OnPointerExit(null);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        //detect right click
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
		{
            Debug.Log("right click");
            if (OnRightClickChannel != null)
				OnRightClickChannel.Publish(this);
		}

		//detect double click
		if (eventData != null && eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
		{
			Debug.Log("double click");
			if (OnDoubleClickChannel != null)
				OnDoubleClickChannel.Publish(this);
		}
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;

		if (OnEnterClickChannel != null)
            OnEnterClickChannel.Publish(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;

		if (OnExitClickChannel != null)
            OnExitClickChannel.Publish(this);
	}
}
