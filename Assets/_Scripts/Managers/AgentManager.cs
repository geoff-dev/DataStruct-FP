using UnityEngine;

public static class AgentManager {
    public static int AgentCount => _agentsCount;
    static int _agentsCount;
    static int _escapeAgentCount;
    
    public static void AssignAgentsCount(int count) {
        _agentsCount = count;
        _escapeAgentCount = 0;
    }

    private static void CheckRemainingAgents() {
        if (_agentsCount <= 0) {
            Debug.Log("GAME OVER");
        }
    }
    
    public static void UpdateEscapeAgent(bool escaped) {
        if (escaped)
            _escapeAgentCount++;
        _agentsCount--;
        CheckRemainingAgents();
    }
}