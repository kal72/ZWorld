using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemTier;
    [SerializeField] TMP_Text itemType;
    [SerializeField] GameObject statBonusLayout;
    [SerializeField] TMP_Text itemDesc;
    [SerializeField] Button buttonEquip;

    public void UpdateInfo(Item _item)
    {
        buttonEquip.onClick.RemoveAllListeners();
        buttonEquip.gameObject.SetActive(false);
        gameObject.SetActive(true);

        itemIcon.sprite = _item.Icon;
        itemName.text = _item.Name;
        itemTier.text = "Legendary";
        itemType.text = _item.GetItemType();
        itemDesc.text = _item.Description;

        switch (_item.itemGroup)
        {
            case Item.ItemGroup.Equip:
                assignStatBonus((EquippableItem)_item);
                buttonEquip.gameObject.SetActive(true);
                buttonEquip.onClick.AddListener(() => InventoryManager.Instance.Equip((EquippableItem)_item));
                break;
            case Item.ItemGroup.Usable:
                break;
            case Item.ItemGroup.Material:
                break;
            case Item.ItemGroup.Other:
                break;
            default:
                break;
        }
    }

    private void assignStatBonus(EquippableItem equipItem)
    {
        Transform transform = statBonusLayout.transform;
        for (int i = 0; i <= transform.childCount; i++)
        {
            if (i == 10)
            {
                break;
            }

            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (equipItem.ATKBonus != 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "- ATK +" + equipItem.ATKBonus.ToString();
        }
        if (equipItem.ATKPercentBonus != 0)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "- ATK +" + equipItem.ATKPercentBonus.ToString() + "%";
        }

        if (equipItem.CritDamagePercentBonus != 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "- Crit DMG +" + equipItem.CritDamagePercentBonus.ToString() + "%";
        }
        if (equipItem.CritRatePercentBonus != 0)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "- Crit DMG +" + equipItem.CritRatePercentBonus.ToString() + "%";
        }
        if (equipItem.DEFBonus != 0)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "- DEF +" + equipItem.DEFBonus.ToString();
        }
        if (equipItem.DEFPercentBonus != 0)
        {
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = "- DEF +" + equipItem.DEFPercentBonus.ToString() + "%";
        }
        if (equipItem.HealthBonus != 0)
        {
            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().text = "- Max HP +" + equipItem.HealthBonus.ToString();
        }
        if (equipItem.HealthPercentBonus != 0)
        {
            transform.GetChild(7).gameObject.SetActive(true);
            transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().text = "- Max HP +" + equipItem.HealthPercentBonus.ToString() + "%";
        }
        if (equipItem.ManaBonus != 0)
        {
            transform.GetChild(8).gameObject.SetActive(true);
            transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().text = "- Max Mana +" + equipItem.ManaBonus.ToString();
        }
        if (equipItem.ManaPercentBonus != 0)
        {
            transform.GetChild(9).gameObject.SetActive(true);
            transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().text = "- Max Mana +" + equipItem.ManaPercentBonus.ToString() + "%";
        }

    }
}
