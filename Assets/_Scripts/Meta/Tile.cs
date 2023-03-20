using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType type;
    [SerializeField] private bool startingTile;
    [SerializeField] private bool endingTile;
    private bool _init = false;
    private int _x;
    private int _y;

    public void Init() {
        if (_init) return;
        _init = true;
        
        Vector3 currentPos = this.transform.position;
        int xPos = (int)currentPos.x;
        int yPos = (int)currentPos.y;
        
        _x = xPos;
        _y = yPos;
        name = $"{type}: {_x}_{_y}";
        
        //TODO specific sprites to attach
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        switch (type) {
            case TileType.Path:
                sr.color = Color.white;
                break;
            case TileType.Wall:
                sr.color = Color.black;
                break;
        }
        if(startingTile)
            sr.color = Color.green;
        if(endingTile)
            sr.color = Color.red;
    }

    public int Cost {
        get {
            return type switch
            {
                TileType.Path => 1,
                _ => 0
            };
        }
    }

    public TileType Type => type;
    public bool StartingTile => startingTile;
    public bool EndingTile => endingTile;
    public int X => _x;
    public int Y => _y;
}

public enum TileType
{
    Path,
    Wall,
}