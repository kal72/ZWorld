using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropItemArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Object")]
    [SerializeField] Image image;

    [Header("Game Events")]
    public VoidEventChannel OnDropChannel;

    private Color dropColor = new Color(1, 1, 1, 0.2f);
    private Color normalColor = new Color(1, 1, 1, 0f);

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("on drop area");
        image.color = normalColor;
        if (OnDropChannel != null)
            OnDropChannel.Publish();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InventoryManager.Instance.IsItemDrag)
            image.color = dropColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InventoryManager.Instance.IsItemDrag)
            image.color = normalColor;
    }
}
