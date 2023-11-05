using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEvent<T>: ScriptableObject
{
    private UnityAction<T> onEventChannel;

    public virtual void Subscribe(UnityAction<T> onEvent)
    {
        onEventChannel += onEvent;
    }

    public virtual void Unsubscribe(UnityAction<T> onEvent)
    {
        onEventChannel -= onEvent;
    }

    public virtual void Publish(T value)
    {
        onEventChannel?.Invoke(value);
    }
}
