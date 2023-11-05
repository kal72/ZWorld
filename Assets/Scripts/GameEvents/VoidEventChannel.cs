using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "GameEvents/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject
{
    private UnityAction onSubscribeEvent;

    public void Subscribe(UnityAction action)
    {
        onSubscribeEvent += action;
    }

    public void Unsubscribe(UnityAction action)
    {
        onSubscribeEvent -= action;
    }

    public void Publish()
    {
        onSubscribeEvent?.Invoke();
    }
}
