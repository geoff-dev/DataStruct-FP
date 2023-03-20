using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AgentType type;
    [SerializeField] private TextMeshProUGUI textMesh;
    private string _agentName;

    private void Awake() {
        EventsManager.OnUpdateAgentCount.OnAction += OnUpdateAgentCount;
        SetAgentName();
    }
    
    private void OnDestroy() {
        EventsManager.OnUpdateAgentCount.OnAction -= OnUpdateAgentCount;
    }

    private void OnUpdateAgentCount(AgentType currentAgent, int count) {
        if (type != currentAgent) 
            return;
        textMesh.text = $"{_agentName}\n{count} to deploy";
    }

    private void SetAgentName() {
        var str = type.ToString();
        _agentName = string.Concat(str.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }

    public void OnPointerDown(PointerEventData eventData) {
        EventsManager.OnPointerClick.InvokeAction(type);
    }
}