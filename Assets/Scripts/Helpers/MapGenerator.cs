using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Texture2D mapTexture;
    [SerializeField] private ColorToPrefab[] colorMappings;
    private Map _map;
    private List<Collider2D> _tileCols;

    private void Start() {
        _tileCols = new List<Collider2D>();
        _map = GetComponent<Map>();
        GenerateMap();
        _map.PopulateMap(mapTexture.width, mapTexture.height);
        EventsManager.OnUpdateCamera.InvokeAction(_tileCols.ToArray());
    }

    private void GenerateMap() {
        for (int x = 0 ; x < mapTexture.width ; x++) {
            for (int y = 0 ; y < mapTexture.height ; y++) {
                GenerateTile(x,y);
            }
        }
    }

    private void GenerateTile(int x, int y) {
        Color pixelColor = mapTexture.GetPixel(x, y);
        if (pixelColor.a == 0)
            return;
        
        foreach (var colorMap in colorMappings) {
            if (colorMap.Color != pixelColor)
                continue;
            Vector2 pos = new Vector2(x, y);
            GameObject go = Instantiate(colorMap.Prefab, pos, Quaternion.identity, transform);
            _tileCols.Add(go.GetComponent<Collider2D>());
        }
    }
}

[System.Serializable]
public class ColorToPrefab {
    public Color Color;
    public GameObject Prefab;
}
