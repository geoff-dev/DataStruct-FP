using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    [SerializeField] private AgentType type;
    [Header("Movement")]
    [SerializeField, Range(1f, 20f)] private float speedFactor = 1;
    [Header("Rotation")]
    [SerializeField, Range(1f, 20f)] private float smoothFactor = 3f;
    
    private Queue<Tile> _path;
    public AgentType Type => type;

    private void OnDrawGizmos() {
        ScreenGizmos.DisplayTransformInfo(this.transform, Vector2.up, Color.green);
    }

    public void SetPath(Queue<Tile> path) {
        _path = path;
        StopAllCoroutines();
        StartCoroutine(MoveAlongPath(_path));
    }

    private IEnumerator MoveAlongPath(Queue<Tile> path) {
        // this.transform.position = path
        yield return new WaitForSeconds(1);
        Vector3 lastPosition = transform.position;
        while (path.Count > 0) {
            Tile nextTile = path.Dequeue();
            float lerpVal = 0;
            Vector3 vecToTarget = nextTile.transform.position - this.transform.position;
            float angleDeg = Core.DirectionToAngle(vecToTarget, true);
            while (lerpVal < 1) {
                lerpVal += Time.deltaTime * speedFactor;
                transform.position = Vector3.Lerp(lastPosition , nextTile.transform.position , lerpVal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angleDeg), smoothFactor * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            // yield return new WaitForSeconds(0.5f / speedFactor);
            lastPosition = nextTile.transform.position;
        }
    }
}

public enum AgentType {
    ElGato ,
    Dehya ,
}