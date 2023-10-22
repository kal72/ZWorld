using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquippableItem equipItem;
    public EquipmentType EquipmentType;
    [SerializeField] Sprite defaultIcon;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => InventoryManager.Instance.ShowItemInfo(equipItem));
    }

    public void RefreshUI()
    {
        if (equipItem != null)
        {
            transform.Find("Icon").GetComponent<Image>().sprite = equipItem.Icon;
        }
        else
        {
             transform.Find("Icon").GetComponent<Image>().sprite = defaultIcon;
        }
    }
}
