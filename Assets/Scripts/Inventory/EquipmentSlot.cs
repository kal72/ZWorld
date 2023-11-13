using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType ItemType;
    [SerializeField] Transform backImage;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (backImage == null)
            backImage = transform.Find("Background");
    }

    public void RefreshUI()
    {
        if (Item == null)
            backImage.gameObject.SetActive(true);
        else
            backImage.gameObject.SetActive(false);
    }
}
