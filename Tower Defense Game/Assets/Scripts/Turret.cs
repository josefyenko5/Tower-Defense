using UnityEngine;
using YikonUtility;

public class Turret : MonoBehaviour {

    [SerializeField] private Transform head;
    [SerializeField] private Transform rangePoint;
    [SerializeField] private TurretSettingsSO turretSettingsSO;

    private Transform nearest;

    private void FixedUpdate () {
        HandleFindingTarget();
    }

    private void HandleFindingTarget () {
        HandleRotation();
        var enemyInRange = Physics.OverlapSphere(rangePoint.position, turretSettingsSO.radius, turretSettingsSO.enemyMask);
        if (enemyInRange.Length <= 0) return;
        nearest = Utility.FindClosestTarget(transform, enemyInRange);
    }

    private void HandleRotation () {
        if (nearest == null) return;
        var targetRotation = Quaternion.LookRotation(nearest.position);
        var smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*turretSettingsSO.angularSmoothing);
        var eulerAngles = smoothRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Vector3.up * eulerAngles.y);
    }

    private void OnDrawGizmosSelected () {
        if (rangePoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangePoint.position, turretSettingsSO.radius);
    }

}
