using UnityEngine;

public class InputController : MonoBehaviour {
    private Transform _spriteTr;

    private void Awake() {
        EventsManager.OnReadyToSpawnAgent.OnAction += OnReadyToSpawnAgent;
    }

    private void OnDestroy() {
        EventsManager.OnReadyToSpawnAgent.OnAction -= OnReadyToSpawnAgent;
    }

    private void OnReadyToSpawnAgent(Agent agent) {
        _spriteTr = agent.gameObject.transform;
    }

    private void Update() {
        UpdateInput();
    }

    private void UpdateInput() {
        // TODO input checks if still in game
        
        if (_spriteTr == null) 
            return;
        PointAndDragSprite();
        DropSprite();
    }

    private void PointAndDragSprite() {
        if (!Input.GetMouseButton(0))
            return;
        Vector3 mousePos = Data.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = Data.MainCamera.transform.position.z + Data.MainCamera.nearClipPlane;
        _spriteTr.transform.position = mousePos;
    }

    private void DropSprite() {
        if (!Input.GetMouseButtonUp(0))
            return;
        Ray ray = Data.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray , Mathf.Infinity , Data.TilesLayerMask);
        if (!rayHit) {
            ClearSpriteObj();
            return;
        }
        if (!rayHit.transform.TryGetComponent(out Tile tile))
            return;
        if (!tile.StartingTile) {
            ClearSpriteObj();
            return;
        }
        EventsManager.OnSetPath.InvokeAction(tile); // TODO wait before spawn
        _spriteTr = null;
    }

    private void ClearSpriteObj() {
        Destroy(_spriteTr.gameObject);
        _spriteTr = null;
    }
}