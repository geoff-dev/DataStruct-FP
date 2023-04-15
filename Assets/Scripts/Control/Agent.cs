using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour,IHealthHandler {
    [SerializeField] private AgentType type;
    [Header("Movement")]
    [SerializeField, Range(1f, 20f)] private float speedFactor = 1;
    [Header("Rotation")]
    [SerializeField, Range(1f, 20f)] private float smoothFactor = 3f;
    
    private Queue<Tile> _path;
    public AgentType Type => type;

    [SerializeField] private int initHealth = 4;
    private Coroutine deathCo = null;

    #region IHealthHandler

    public int CurrentHealth { get; private set; }
    public bool IsAlive => CurrentHealth > 0;
    
    public void DoDamage(int amount) {
        CurrentHealth -= amount;
        Debug.Log($"{gameObject.name} Health: {CurrentHealth}");
        if (IsAlive) return;
        // TODO death animations
        if (deathCo != null) return; // Death is already playing
        Debug.Log($"{gameObject.name} Died!");
        deathCo = StartCoroutine(DisableAgent(1f));
    }

    #endregion

    #region Path

    public void SetPath(Queue<Tile> path) {
        _path = path;
        StopAllCoroutines();
        Debug.Log($"{gameObject.name} Spawns");
        StartCoroutine(MoveAlongPath(_path));
        this.gameObject.layer = Data.EntityLayer;
    }

    private IEnumerator MoveAlongPath(Queue<Tile> path) {
        // this.transform.position = path
        CurrentHealth = initHealth;
        yield return new WaitForSeconds(1);
        Vector3 lastPosition = transform.position;
        while (path.Count > 0) {
            if (!IsAlive) {
                AgentManager.UpdateEscapeAgent(false);
                yield break;
            }
            Tile nextTile = path.Dequeue();
            float lerpVal = 0;
            Vector3 vecToTarget = nextTile.transform.position - this.transform.position;
            float angleDeg = Core.DirectionToAngle(vecToTarget, true);
            while (lerpVal < 1) {
                if (!IsAlive) {
                    AgentManager.UpdateEscapeAgent(false);
                    yield break;
                }
                lerpVal += Time.deltaTime * speedFactor;
                transform.position = Vector3.Lerp(lastPosition , nextTile.transform.position , lerpVal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angleDeg), smoothFactor * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            // yield return new WaitForSeconds(0.5f / speedFactor);
            lastPosition = nextTile.transform.position;
        }
        // Add to escape list
        AgentManager.UpdateEscapeAgent(true);
        StartCoroutine(DisableAgent(.3f));
    }

    #endregion

    IEnumerator DisableAgent(float time) {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

public interface IHealthHandler {
    int CurrentHealth { get; }
    bool IsAlive { get; }
    void DoDamage(int amount);
}

public enum AgentType {
    ElGato ,
    Dehya ,
}