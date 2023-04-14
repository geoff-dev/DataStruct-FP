using System.Collections.Generic;
using UnityEngine;

public class AgentsList : MonoBehaviour {
    [SerializeField] private AgentData[ ] agents;
    private Dictionary<AgentType , AgentData> _agentsDictionary;
    private Agent _currentAgent;
    private int agentsCount = 0;

    private void Awake() {
        _agentsDictionary = new Dictionary<AgentType , AgentData>();
        agentsCount = 0;
        foreach (AgentData agent in agents) {
            _agentsDictionary[agent.Type] = agent;
            agentsCount += agent.Count;
        }
        AgentManager.AssignAgentsCount(agentsCount);
        EventsManager.OnPointerClick.OnAction += OnPointerClickAction;
        EventsManager.OnSetPath.OnAction += OnSetPath;
    }

    private void OnDestroy() {
        EventsManager.OnPointerClick.OnAction -= OnPointerClickAction;
        EventsManager.OnSetPath.OnAction -= OnSetPath;
    }

    private void Start() {
        foreach (AgentData agent in _agentsDictionary.Values)
            EventsManager.OnUpdateAgentCount.InvokeAction(agent.Type , agent.Count);
    }

    private void OnSetPath(Tile startTile) {
        Map map = MapManager.GetCurrentMap();
        Queue<Tile> path = map.GetRoute(startTile);
        _currentAgent.transform.position = startTile.transform.position;
        _currentAgent.SetPath(path);
        int currentCount = --_agentsDictionary[_currentAgent.Type].Count;
        EventsManager.OnUpdateAgentCount.InvokeAction(_currentAgent.Type ,
            currentCount); //TODO game over after all cats deployed
        agentsCount--;
        
    }

    private void OnPointerClickAction(AgentType type) {
        if (!_agentsDictionary.TryGetValue(type , out var agentData))
            return;
        if (agentData.Count <= 0) return;
        Agent agent = Instantiate(agentData.Prefab);
        EventsManager.OnReadyToSpawnAgent.InvokeAction(agent);
        _currentAgent = agent;
        
        
    }

    [System.Serializable]
    public class AgentData {
        public AgentType Type;
        public int Count = 1;
        public Agent Prefab; // Can be changed to scriptable objects
    }
}