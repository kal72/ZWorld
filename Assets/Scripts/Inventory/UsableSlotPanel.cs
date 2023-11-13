using UnityEngine;

public class UsableSlotPanel : MonoBehaviour
{
    [SerializeField] UsableSlot[] usableSlots;

    private void OnValidate()
    {
        usableSlots = GetComponentsInChildren<UsableSlot>();
    }

    private void Update()
    {
        if (usableSlots.Length == 0) return;

        foreach(var slot in usableSlots)
        {
            if (KeyBinding.Instance.PlayerActivateSlot(slot))
                return;
        }
    }
}
