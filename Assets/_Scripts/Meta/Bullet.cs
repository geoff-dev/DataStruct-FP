using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Transform target;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float distThresh = 1f;
    
    public void SetTarget(Transform tr) {
        target = tr;
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget() {
        while (Vector3.Distance(target.position, this.transform.position) > distThresh) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if(target.TryGetComponent(out IHealthHandler handler)){
            handler?.DoDamage(1);
        }
        Destroy(this.gameObject);
    }
    
    
}