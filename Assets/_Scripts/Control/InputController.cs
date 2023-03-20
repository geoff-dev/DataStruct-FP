using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameObject _spriteObj;
    private Camera _camera;
    private bool _launchAgent = false;

    private void Awake() {
        _camera = Camera.main;
        EventsManager.OnReadyToSpawnAgent.OnAction += OnReadyToSpawnAgent;
    }

    private void OnDestroy() {
        EventsManager.OnReadyToSpawnAgent.OnAction -= OnReadyToSpawnAgent;
    }

    private void OnReadyToSpawnAgent(Agent agent) {
        _spriteObj = agent.gameObject;
    }

    private void Update() {
        CheckInputs();
    }

    private void CheckInputs() {
        if (_spriteObj != null && Input.GetMouseButton(0)) {
            //TODO make cursor invisible or something
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = _camera.transform.position.z + _camera.nearClipPlane;
            _spriteObj.transform.position = mousePosition;
        }

        if (_spriteObj != null && Input.GetMouseButtonUp(0)) {
            //FIRE raycast
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, Data.TilesLayerMask);
            if (!rayHit) {
                ClearSpriteObj();
                return;
            }
            if (rayHit.transform.TryGetComponent(out Tile tile)) {
                if (!tile.StartingTile) {
                    ClearSpriteObj();
                    return;
                }
                EventsManager.OnSetPath.InvokeAction(tile);
                _spriteObj = null;
            }
        }
    }

    private void ClearSpriteObj() {
        Destroy(_spriteObj);
        _spriteObj = null;
    }
}