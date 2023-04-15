using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] private float buffer = 2;

    private void Awake() {
        EventsManager.OnUpdateCamera.OnAction += OnUpdateCamera;
    }

    private void OnDestroy() {
        EventsManager.OnUpdateCamera.OnAction -= OnUpdateCamera;
    }

    private void OnUpdateCamera(Collider2D[] coliders) {
        var (center, size) = CalculateOrthoSize(coliders);
        Data.MainCamera.transform.position = center;
        Data.MainCamera.orthographicSize = size;

        (Vector3 center, float size) CalculateOrthoSize(Collider2D[] cols) {
            Bounds bounds = new Bounds();
            foreach (var col in cols)
                bounds.Encapsulate(col.bounds);
            bounds.Expand(buffer);
            float vertical = bounds.size.y;
            float horizontal = bounds.size.x * Data.MainCamera.pixelHeight / Data.MainCamera.pixelWidth;
            float size = Mathf.Max(horizontal, vertical) * 0.5f;
            Vector3 center = bounds.center + new Vector3(0, 0, -10);
            return (center, size);
        }
    }
}