using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeUI : MonoBehaviour
{
	[Header("Configuration")]
	[SerializeField] private Image itemIcon;
	[SerializeField] private TMP_Text itemName;
	[Header("References")]
	//[SerializeField] RectTransform arrowParent;
	//[SerializeField] BaseItemSlot[] itemSlots;

	[Header("Public Variables")]
	public IItemContainer ItemContainer;

	private CraftingRecipe craftingRecipe;
	public CraftingRecipe CraftingRecipe {
		get { return craftingRecipe; }
		set { SetCraftingRecipe(value); }
	}

	//public event Action<BaseItemSlot> OnPointerEnterEvent;
	// public event Action<BaseItemSlot> OnPointerExitEvent;

	private void OnValidate()
	{
		// itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
	}

	private void Start()
	{
		// foreach (BaseItemSlot itemSlot in itemSlots)
		// {
		// 	itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
		// 	itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		// }
	}

	public void OnCraftButtonClick()
	{
		if (craftingRecipe != null && ItemContainer != null)
		{
			craftingRecipe.Craft(ItemContainer);
		}
	}

	private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
	{
		craftingRecipe = newCraftingRecipe;

		if (craftingRecipe != null)
		{
			//int slotIndex = 0;
			//slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
			// arrowParent.SetSiblingIndex(slotIndex);
			//slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

			// for (int i = slotIndex; i < itemSlots.Length; i++)
			// {
			// 	itemSlots[i].transform.parent.gameObject.SetActive(false);
			// }

			if (craftingRecipe.Results.Count != 0){
				itemIcon.sprite = craftingRecipe.Results[0].Item.Icon;
				itemName.text = craftingRecipe.Results[0].Item.Name;
			}
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void Addlistener(CraftingRecipeWindow craftingRecipeWindow){
		Button button = GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate {
			craftingRecipeWindow.UpdateCraftingInfo(this);
		});
	}

	// private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	// {
	// 	for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
	// 	{
	// 		ItemAmount itemAmount = itemAmountList[i];
	// 		BaseItemSlot itemSlot = itemSlots[slotIndex];

	// 		itemSlot.Item = itemAmount.Item;
	// 		itemSlot.Amount = itemAmount.Amount;
	// 		itemSlot.transform.parent.gameObject.SetActive(true);
	// 	}
	// 	return slotIndex;
	// }
}
