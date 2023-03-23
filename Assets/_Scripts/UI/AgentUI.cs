using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentUI : MonoBehaviour , IPointerDownHandler {
    [SerializeField] private AgentType type;
    private TextMeshProUGUI _textMesh;
    private string _agentName;

    private void Awake() {
        Init();
        EventsManager.OnUpdateAgentCount.OnAction += OnUpdateAgentCount;
    }

    private void OnDestroy() {
        EventsManager.OnUpdateAgentCount.OnAction -= OnUpdateAgentCount;
    }

    private void OnUpdateAgentCount(AgentType currentAgent , int count) {
        if (type != currentAgent)
            return;
        _textMesh.text = $"{_agentName}\n{count} to deploy";
    }

    public void OnPointerDown(PointerEventData eventData) {
        EventsManager.OnPointerClick.InvokeAction(type);
    }

    private void Init() {
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        var str = type.ToString();
        _agentName = string.Concat(str.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}