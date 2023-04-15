using System.Collections.Generic;
using UnityEngine;

public class PathTester : MonoBehaviour {
    public Agent Agent;
    public Tile StartingPath;

    [ContextMenu("Test Path")]
    public void TestPath() {
        Map map = MapManager.GetCurrentMap();
        Queue<Tile> path = map.GetRoute(StartingPath);
        Agent.transform.position = StartingPath.transform.position;
        Agent.SetPath(path);
    }

    public void AssignPath(Tile startTile) {
        //TODO to get the agent also
        Map map = MapManager.GetCurrentMap();
        Queue<Tile> path = map.GetRoute(startTile);

        Agent.transform.position = startTile.transform.position;
        Agent.SetPath(path);
    }
}