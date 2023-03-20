using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentsList : MonoBehaviour
{ 
    [SerializeField] private AgentData[] agents;
    private Dictionary<AgentType, AgentData> _agentsDictionary;
    private Agent _currentAgent;
    
    private void Awake() {
        _agentsDictionary = new Dictionary<AgentType, AgentData>();
        foreach (var agent in agents)
            _agentsDictionary[agent.Type] = agent;
        
        EventsManager.OnPointerClick.OnAction += OnPointerClickAction;
        EventsManager.OnSetPath.OnAction += OnSetPath;
    }
    private void OnDestroy() {
        EventsManager.OnPointerClick.OnAction -= OnPointerClickAction;
        EventsManager.OnSetPath.OnAction -= OnSetPath;
    }

    private void Start() {
        foreach (var agent in _agentsDictionary.Values)
            EventsManager.OnUpdateAgentCount.InvokeAction(agent.Type, agent.Count);
    }

    private void OnSetPath(Tile startTile) {
        var map = MapManager.GetCurrentMap();
        var path = map.GetRoute(startTile);
        _currentAgent.transform.position = startTile.transform.position;
        _currentAgent.SetPath(path);
        //TODO event of last cat spawned and not moving/reached goal, ready for result
        int currentCount = --_agentsDictionary[_currentAgent.Type].Count;
        EventsManager.OnUpdateAgentCount.InvokeAction(_currentAgent.Type, currentCount);
    }
    
    private void OnPointerClickAction(AgentType type) {
        if (!_agentsDictionary.TryGetValue(type, out var agentData)) 
            return;
        //TODO display spawn counts to UI panel
        if (agentData.Count <= 0) return;
        Agent agent = Instantiate(agentData.Prefab);
        EventsManager.OnReadyToSpawnAgent.InvokeAction(agent);
        _currentAgent = agent;
    }

    [System.Serializable]
    public class AgentData
    {
        public AgentType Type;
        public int Count = 1; 
        //Can be changed to scriptable objects
        public Agent Prefab;
    }
}


