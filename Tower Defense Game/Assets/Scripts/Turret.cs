using UnityEngine;
using YikonUtility;

public class Turret : MonoBehaviour {

    [SerializeField] private Transform head;
    [SerializeField] private Transform rangePoint;
    [SerializeField] private TurretSettingsSO turretSettingsSO;

    private Transform nearest;
    private float nextTimeToAttack;

    private void FixedUpdate () {
        HandleFindingTarget();
        HandleAttacking();
    }

    private void HandleFindingTarget () {
        HandleRotation();
        var enemyInRange = Physics.OverlapSphere(rangePoint.position, turretSettingsSO.radius, turretSettingsSO.enemyMask);
        if (enemyInRange.Length <= 0) return;
        nearest = Utility.FindClosestTarget(transform, enemyInRange);
    }

    private void HandleRotation () {
        if (nearest == null) return;
        var dir = (nearest.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(dir);
        var smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*turretSettingsSO.angularSmoothing);
        var eulerAngles = smoothRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Vector3.up * eulerAngles.y);
    }

    private void HandleAttacking () {
        if (nearest == null) return;
        if (Time.time > nextTimeToAttack) {
            nextTimeToAttack = Time.time + turretSettingsSO.fireRate;
            var healthSystem = nearest.Find("HealthSystem").GetComponent<HealthSystem>();
            healthSystem.TakeDamage(turretSettingsSO.damage);
        }
    }

    private void OnDrawGizmosSelected () {
        if (rangePoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rangePoint.position, turretSettingsSO.radius);
    }

}
