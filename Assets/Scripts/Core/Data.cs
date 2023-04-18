using UnityEngine;

public static class Data {
    // Layers
    public static readonly int TileLayer = LayerMask.NameToLayer("Tiles");
    public static readonly int TilesLayerMask = 1 << TileLayer;
    public static readonly int EntityLayer = LayerMask.NameToLayer("Entity");
    public static readonly int EntityLayerMask = 1 << EntityLayer;
    
    // Animation
    public static readonly int IDLE_ANIM = Animator.StringToHash("Idle");
    public static readonly int RUN_ANIM = Animator.StringToHash("Run");
    
    public static readonly int WALK_UP = Animator.StringToHash("Walk_Up");
    public static readonly int WALK_DOWN = Animator.StringToHash("Walk_Down");
    public static readonly int WALK_SIDE = Animator.StringToHash("Walk_Side");
    
    // Main Camera
    private static Camera _camera;
    public static Camera MainCamera => _camera = Camera.main;
}