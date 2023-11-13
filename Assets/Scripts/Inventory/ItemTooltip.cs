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

    [SerializeField] private Vector2 offset = new Vector2(-50f, 0f);

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item _item)
    {
        if (_item == null) return;

        gameObject.SetActive(true);
        updatePosisionToMouse();

        itemIcon.sprite = _item.Icon;
        itemName.text = string.Format("<color={0}>[{1}]</color>", _item.GetColor(), _item.Name);
        itemTier.text = string.Format("<color={0}>{1}</color>", _item.GetColor(), _item.ItemClasses);
        itemType.text = _item.GetItemType();
        itemDesc.text = _item.Description;
        assignStatBonus(_item);

        //switch (_item.itemGroup)
        //{
        //    case Item.ItemGroup.Equip:

        //        break;
        //    case Item.ItemGroup.Usable:
        //        break;
        //    case Item.ItemGroup.Material:
        //        break;
        //    case Item.ItemGroup.Other:
        //        break;
        //    default:
        //        break;
        //}
    }

    public void Hide()
    {
        if (!gameObject.activeSelf) return;

        gameObject.SetActive(false);
        itemIcon.sprite = null;
        itemName.text = null;
        itemTier.text = null;
        itemType.text = null;
        itemDesc.text = null;
    }

    private void assignStatBonus(Item _item)
    {
        statBonusText.text = _item.GetDescription();
    }


    private void updatePosisionToMouse()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        var rectT = GetComponent<RectTransform>();

        //rectT.position = new Vector3(mousePosScreen.x - rectT.sizeDelta.x / 2, mousePosScreen.y - rectT.sizeDelta.y / 2, mousePosScreen.z);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectT.parent as RectTransform,
            mousePosScreen,
            null, // Since it's Screen Space - Overlay, set the camera to null
            out Vector2 mousePosCanvas
        );

        // Set the panel's anchored position to the top right of the mouse position
        rectT.anchoredPosition = new Vector2(mousePosCanvas.x - rectT.sizeDelta.x / 2 + offset.x, mousePosCanvas.y - rectT.sizeDelta.y / 2 + offset.y);
    }


}
