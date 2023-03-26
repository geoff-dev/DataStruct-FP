using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class ScreenGizmos {
    public static void DisplayTransformInfo(Transform target, Vector3 textOffset, Color textColor) {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = textColor;
        var position = target.position;
        Vector3 pos = position + textOffset;
        Quaternion quatRot = target.rotation;
        Vector3 vecRot = quatRot.eulerAngles;
        string msg = $"Entity: {target.name} \nPosition: {position} \nRotation: {vecRot}";
#if UNITY_EDITOR
        Handles.Label(pos, msg, style);
#endif
    }

    public static void Display(Vector3 textPos, Color textColor, params object[ ] args) {
        if (args.Length == 0) return;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = textColor;
        string msg = args.Aggregate("", (current, obj) => current + $"\n {obj}"); // Same as foreach
#if UNITY_EDITOR
        Handles.Label(textPos, msg, style);
#endif
    }
}