using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    private static Queue<Tile> AStar(Tile start, Tile goal) {
        Dictionary<Tile, Tile> nextTileToGoal = new Dictionary<Tile, Tile>();
        Dictionary<Tile, int> costToReachTile = new Dictionary<Tile, int>();
        PriorityQueue<Tile> frontier = new PriorityQueue<Tile>();
        frontier.Enqueue(goal, 0);
        costToReachTile[goal] = 0;

        while (frontier.Count > 0) {
            Tile curTile = frontier.Dequeue();
            if (curTile == start)
                break;
            
            foreach (Tile neighbor in MapManager.GetCurrentMap().Neighbors(curTile)) {
                int newCost = costToReachTile[curTile] + neighbor.Cost;
                if (!costToReachTile.ContainsKey(neighbor) || newCost < costToReachTile[neighbor]) {
                    if (neighbor.Type == TileType.Wall) continue;
                    costToReachTile[neighbor] = newCost;
                    int priority = newCost + Distance(neighbor, start);
                    frontier.Enqueue(neighbor, priority);
                    nextTileToGoal[neighbor] = curTile;
                }
            }
        }
        
        //Checker if starting there is starting point
        if (!nextTileToGoal.ContainsKey(start))
            return null;

        Queue<Tile> path = new Queue<Tile>();
        Tile pathTile = start;
        while (goal != pathTile) {
            pathTile = nextTileToGoal[pathTile];
            path.Enqueue(pathTile);
        }
        return path;
    }
    
    public static Queue<Tile> FindPath(Tile start, Tile end) {
        return AStar(start, end);
    }
    
    //Uses Manhattan Distance formula
    private static int Distance(Tile t1, Tile t2) {
        return Mathf.Abs(t1.X - t2.X) + Mathf.Abs(t1.Y - t2.Y);
    }
}