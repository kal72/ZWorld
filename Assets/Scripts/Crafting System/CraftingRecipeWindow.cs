using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeWindow : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDesc;
    //[SerializeField] RectTransform recipeUIParent;
    [SerializeField] Button buttonCraft;
    [SerializeField] BaseItemSlot[] itemSlots;

    [SerializeField] CraftingRecipeUI craftingRecipeUICurrent;

    private void OnValidate()
	{
		itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
	}

    public void UpdateCraftingInfo(CraftingRecipeUI craftingRecipeUI){

        Item item = craftingRecipeUI.CraftingRecipe.Results[0].Item;
        itemIcon.sprite = item.Icon;
        itemName.text = item.Name;
        itemDesc.text = item.Description;
        
		buttonCraft.onClick.RemoveAllListeners();
        buttonCraft.onClick.AddListener(craftingRecipeUI.OnCraftButtonClick);

        int slotIndex = 0;
		slotIndex = SetSlots(craftingRecipeUI.CraftingRecipe.Materials, slotIndex);

		for (int i = slotIndex; i < itemSlots.Length; i++)
		{
			itemSlots[i].transform.gameObject.SetActive(false);
		}

        craftingRecipeUICurrent = craftingRecipeUI;
    }

    private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
		for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
		{
			ItemAmount itemAmount = itemAmountList[i];
			BaseItemSlot itemSlot = itemSlots[slotIndex];

			itemSlot.Item = itemAmount.Item;
			itemSlot.Amount = itemAmount.Amount;
			itemSlot.transform.gameObject.SetActive(true);
		}
		return slotIndex;
	}
}
