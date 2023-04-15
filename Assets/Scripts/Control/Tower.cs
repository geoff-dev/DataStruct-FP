using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private Transform gunTr;
    [SerializeField] private Bullet bulletPf;
    [SerializeField] private float detectRadius = 2f;
    [SerializeField] private float rotateSmoothTime = 7f;
    
    [SerializeField] private float timeBetweenAtk = 1;
    private float atkTime;

    private void Start() {
        atkTime = Time.time + timeBetweenAtk;
    }

    private void Update() {
        var cols = Physics2D.OverlapCircleAll(this.transform.position, detectRadius, Data.EntityLayerMask);
        if (cols.Length <= 0)
            return;
        // Aim
        Vector3 vecToTarget = cols[0].transform.position - gunTr.transform.position;
        float angleDeg = Core.DirectionToAngle(vecToTarget, true);
        gunTr.rotation = Quaternion.Slerp(gunTr.rotation, Quaternion.Euler(0, 0, angleDeg),
            rotateSmoothTime * Time.deltaTime);
        // Fire
        if (Time.time < atkTime) return;
        Bullet bul = Instantiate(bulletPf, gunTr.position, Quaternion.identity);
        bul.SetTarget(cols[0].transform);
        atkTime = Time.time + timeBetweenAtk;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 center = this.transform.position;
        Gizmos.DrawWireSphere(center, detectRadius);
    }
}
