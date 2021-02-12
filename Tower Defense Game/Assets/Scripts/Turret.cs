using UnityEngine;
using System.Linq;
using YikonUtility;

public class Turret : MonoBehaviour {

    [SerializeField] private Transform head;
    [SerializeField] private Transform rangePoint;
    [SerializeField] private float radius;
    [SerializeField] private float angularSmoothing;
    [SerializeField] private LayerMask enemyMask;

    private void FixedUpdate () {
        HandleFindingTarget();
    }

    private void HandleFindingTarget () {
        var enemyInRange = Physics.OverlapSphere(rangePoint.position, radius, enemyMask);
        if (enemyInRange.Length <= 0) return;
        var nearest = Utility.FindClosestTarget(transform, enemyInRange);
        var dir = (nearest.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(dir);
        var smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, angularSmoothing * Time.deltaTime);
        transform.rotation = smoothRotation;
    }

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangePoint.position, radius);
    }

}
