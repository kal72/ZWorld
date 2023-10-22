using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot
{
	// event implement IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
	// public event Action<BaseItemSlot> OnBeginDragEvent;
	// public event Action<BaseItemSlot> OnEndDragEvent;
	// public event Action<BaseItemSlot> OnDragEvent;
	// public event Action<BaseItemSlot> OnDropEvent;

	// private bool isDragging;
	// private Color dragColor = new Color(1, 1, 1, 0.5f);

	public override bool CanAddStack(Item item, int amount = 1)
	{
		return base.CanAddStack(item, amount) && Amount + amount <= item.MaximumStacks;
	}

	public override bool CanReceiveItem(Item item)
	{
		return true;
	}
}
