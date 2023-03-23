using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    [SerializeField] private float speedFactor = 1;
    [SerializeField] private AgentType type;
    private Queue<Tile> _path;
    public AgentType Type => type;

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
            // transform.LookAt(nextTile.transform, Vector3.up); // TODO rotation on X and Y axis

            while (lerpVal < 1) {
                lerpVal += Time.deltaTime * speedFactor;
                transform.position = Vector3.Lerp(lastPosition , nextTile.transform.position , lerpVal);
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