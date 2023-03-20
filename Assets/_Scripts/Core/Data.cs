using UnityEngine;

public static class Data
{
    private static readonly int TileLayer = LayerMask.NameToLayer("Tiles");
    public static readonly int TilesLayerMask = 1 << TileLayer;
}