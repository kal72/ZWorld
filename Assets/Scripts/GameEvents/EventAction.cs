using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventAction", menuName = "GameEvents/EventAction")]
public class EventAction : ScriptableObject
{
    private event Action onSubscribeEvent;

    public void Subscribe(Action action)
    {
        onSubscribeEvent += action;
    }

    public void Unsubscribe(Action action)
    {
        onSubscribeEvent -= action;
    }

    public void PublishEvent()
    {
        onSubscribeEvent?.Invoke();
    }
}
