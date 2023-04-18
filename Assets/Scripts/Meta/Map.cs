using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    [SerializeField] private int sizeX; // 10
    [SerializeField] private int sizeY; // 17
    private Tile[ ] _tiles;
    private Tile[,] _grid;
    private Dictionary<Tile, Tile[ ]> _neighborDictionary;
    private List<Tile> _startingTilesList;
    private Tile _endTile;
    public Tile[ ] Neighbors(Tile tile) => _neighborDictionary[tile];


    private void OnDestroy() {
        MapManager.UnregisterMap(this);
    }
    
    private void Start() {
        PopulateMap();
        // EventsManager.OnUpdateCamera.InvokeAction(_tileCols.ToArray());

    }

    private void PopulateMap() {
        _tiles = this.GetComponentsInChildren<Tile>();
        _startingTilesList = new List<Tile>();
        // sizeX = x;
        // sizeY = y;
        _grid = new Tile[sizeX, sizeY];
        _neighborDictionary = new Dictionary<Tile, Tile[ ]>();
        InitMap();
        MapManager.RegisterMap(this);
    }

    private void InitMap() {
        foreach (var tile in _tiles) {
            tile.Init();
            int x = tile.X;
            int y = tile.Y;
            _grid[x, y] = tile;

            if (tile.StartingTile)
                _startingTilesList.Add(tile);
            if (tile.EndingTile)
                _endTile = tile;
        }

        // Add Neighbors
        for (int y = 0 ; y < sizeY ; y++) {
            for (int x = 0 ; x < sizeX ; x++) {
                List<Tile> neighbors = new List<Tile>();
                if (y < sizeY - 1)
                    neighbors.Add(_grid[x, y + 1]);
                if (x < sizeX - 1)
                    neighbors.Add(_grid[x + 1, y]);
                if (y > 0)
                    neighbors.Add(_grid[x, y - 1]);
                if (x > 0)
                    neighbors.Add(_grid[x - 1, y]);
                _neighborDictionary.Add(_grid[x, y], neighbors.ToArray());
            }
        }
    }

    private bool IsStartTile(Tile tile) {
        return _startingTilesList.Contains(tile);
    }

    public Queue<Tile> GetRoute(Tile startTile) {
        return !IsStartTile(startTile) ? null : Pathfinding.FindPath(startTile, _endTile);
    }
}