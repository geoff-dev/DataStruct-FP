public static class MapManager {
    private static Map _currentMap;

    public static Map GetCurrentMap() {
        return _currentMap;
    }

    public static void RegisterMap(Map map) {
        if (_currentMap != map && _currentMap == null)
            _currentMap = map;
    }

    public static void UnregisterMap(Map map) {
        if (_currentMap == map)
            _currentMap = null;
    }
}