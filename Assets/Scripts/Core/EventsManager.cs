using System;
using UnityEngine;

public static class EventsManager {
    public static ActionOption<AgentType> OnPointerClick;
    public static ActionOption<Agent> OnReadyToSpawnAgent;
    public static ActionOption<Tile> OnSetPath;
    public static ActionOption<AgentType , int> OnUpdateAgentCount;
    public static ActionOption<Collider2D[]> OnUpdateCamera;
}

public struct ActionOption<T , U> {
    public event Action<T , U> OnAction;
    public void InvokeAction(T t1 , U t2) {
        OnAction?.Invoke(t1 , t2);
    }
}

public struct ActionOption<T> {
    public event Action<T> OnAction;
    public void InvokeAction(T type) {
        OnAction?.Invoke(type);
    }
}

public struct ActionOption {
    public event Action OnAction;
    public void InvokeAction() {
        OnAction?.Invoke();
    }
}