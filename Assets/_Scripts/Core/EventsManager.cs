using System;
using UnityEngine;

public static class EventsManager
{
    public static ActionOption<AgentType> OnPointerClick;
    public static ActionOption<Agent> OnReadyToSpawnAgent;
    public static ActionOption<Tile> OnSetPath;
}

public struct ActionOption<T>
{
    public event Action<T> OnAction;

    public void InvokeAction(T type) {
        OnAction?.Invoke(type);
    }
}

public struct ActionOption
{
    public event Action OnAction;

    public void InvokeAction() {
        OnAction?.Invoke();
    }
}