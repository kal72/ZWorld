using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemTier;
    [SerializeField] TMP_Text itemType;
    [SerializeField] TMP_Text statBonusText;
    [SerializeField] TMP_Text itemDesc;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item _item)
    {
        gameObject.SetActive(true);
        var recTrans = GetComponent<RectTransform>();
        recTrans.anchoredPosition = Input.mousePosition;

        itemIcon.sprite = _item.Icon;
        itemName.text = string.Format("<color={0}>[{1}]</color>", _item.GetColor(), _item.Name);
        itemTier.text = string.Format("<color={0}>{1}</color>", _item.GetColor(), _item.ItemClasses);
        itemType.text = _item.GetItemType();
        itemDesc.text = _item.Description;

        switch (_item.itemGroup)
        {
            case Item.ItemGroup.Equip:
                assignStatBonus((EquippableItem)_item);
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

    public void Hide()
    {
        gameObject.SetActive(false);
        itemIcon.sprite = null;
        itemName.text = null;
        itemTier.text = null;
        itemType.text = null;
        itemDesc.text = null;
    }

    private void assignStatBonus(EquippableItem _item)
    {
        statBonusText.text = _item.GetDescription();
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}
