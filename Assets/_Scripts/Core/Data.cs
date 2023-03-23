using UnityEngine;

public static class Data {
    // Layers
    private static readonly int TileLayer = LayerMask.NameToLayer("Tiles");
    public static readonly int TilesLayerMask = 1 << TileLayer;
    
    // Main Camera
    private static Camera _camera;
    public static Camera MainCamera => _camera ??= Camera.main;
}