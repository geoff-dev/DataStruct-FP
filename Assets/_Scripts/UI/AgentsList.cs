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

    private void OnSetPath(Tile startTile) {
        var map = MapManager.GetCurrentMap();
        var path = map.GetRoute(startTile);
        _currentAgent.transform.position = startTile.transform.position;
        _currentAgent.SetPath(path);
    }
    
    private void OnPointerClickAction(AgentType type) {
        if (!_agentsDictionary.TryGetValue(type, out var agentData)) 
            return;
        //TODO display spawn counts to UI panel
        // if (agentData.Count <= 0) return;
        // agentData.Count--;
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


