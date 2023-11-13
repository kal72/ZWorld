using UnityEngine;
using UnityEngine.EventSystems;

public class ItemListUI : MonoBehaviour, IDropHandler
{
    [Header("Game Event Publishers")]
    public BaseItemSlotEventChannel OnItemDropChannel;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("item list on drop");
        if (OnItemDropChannel != null)
            OnItemDropChannel.Publish(null);
    }
}
