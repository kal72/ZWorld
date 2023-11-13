using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinding : MonoBehaviour
{
    public static KeyBinding Instance;

    public bool IsLock;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool PlayerActivateSlot(UsableSlot slot)
    {
        if (IsLock) return false;

        if (Input.GetKeyUp(slot.Key))
        {
            slot.Activate();
            return true;
        }

        return false;
    }
}
