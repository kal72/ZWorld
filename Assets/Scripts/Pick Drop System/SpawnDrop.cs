using UnityEngine;

public class SpawnDrop : MonoBehaviour
{
    [Header("Objects")]
    public Transform SpawnPoint;
    public GameObject PouchPrefab;
    [Space]
    [Header("Game Events")]
    public BaseItemSlotEventChannel DropOnWorldChannel;

    [SerializeField] private IItemContainer itemContainer;

    private void OnEnable()
    {
        Debug.Log("subscribe spawndrop");
        DropOnWorldChannel.Subscribe(OnDropWorld);
    }

    private void OnDisable()
    {
        Debug.Log("unsubscribe spawndrop");
        DropOnWorldChannel.Unsubscribe(OnDropWorld);
    }

    void OnDropWorld(BaseItemSlot itemSlot)
    {
        Debug.Log("onDropWorld");
        Debug.Log(itemContainer);
        if (itemContainer == null)
        {
            var obj = Instantiate(PouchPrefab, SpawnPoint.position, Quaternion.identity);
            itemContainer = obj.GetComponent<ItemContainer>();
        }

        InventoryManager.Instance.TransferToOther(itemSlot, itemContainer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pouch")
        {
            itemContainer = other.GetComponent<ItemContainer>();
            Debug.Log("colider pouch enter");
            Debug.Log(itemContainer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pouch")
        {
            itemContainer = null;
            Debug.Log("colider pouch exit");
        }
    }
}
