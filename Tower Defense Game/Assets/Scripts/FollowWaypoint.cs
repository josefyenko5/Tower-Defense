using UnityEngine;
using YikonUtility;

public class FollowWaypoint : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private EnemySettingsSO enemySettingsSO;
    private Transform nextWaypoint;

    private void Start () {
        nextWaypoint = Waypoints.GetNextWaypoint();
    }

    private void Update () {
        Utility.Distance(transform.position, nextWaypoint.position, enemySettingsSO.stoppingDistance, () => {
            nextWaypoint = Waypoints.GetNextWaypoint();
        }, () => {
            rb.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, enemySettingsSO.speed * Time.deltaTime);
        });
    }

}
