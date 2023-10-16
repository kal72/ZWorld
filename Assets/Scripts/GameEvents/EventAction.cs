using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventAction", menuName = "GameEvents/EventAction")]
public class EventAction : ScriptableObject
{
    private event Action onSubscribeEvent;

    public void Subscribe(Action action)
    {
        Debug.Log("subscribe");
        onSubscribeEvent += action;
    }

    public void Unsubscribe(Action action)
    {
        Debug.Log("unsubscribe");
        onSubscribeEvent -= action;
    }

    public void PublishEvent()
    {
        onSubscribeEvent?.Invoke();
    }
}
