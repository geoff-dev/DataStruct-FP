using UnityEngine;

public static class MonoBehaviourHook {
    public static T Create<T>(Transform parent) where T : class {
        GameObject go = new GameObject("", typeof( T ));
        go.transform.SetParent(parent);
        var component = go.GetComponent<T>();
        go.name = component.ToString();
        return component;
    }
}