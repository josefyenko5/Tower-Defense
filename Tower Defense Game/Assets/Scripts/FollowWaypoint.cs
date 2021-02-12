using UnityEngine;
using YikonUtility;

public class FollowWaypoint : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float stoppingDistance = .1f;
    [SerializeField] private float speed;
    private Transform nextWaypoint;

    private void Start () {
        nextWaypoint = Waypoints.GetNextWaypoint();
    }

    private void Update () {
        Utility.Distance(transform.position, nextWaypoint.position, stoppingDistance, () => {
            nextWaypoint = Waypoints.GetNextWaypoint();
        }, () => {
            rb.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, speed * Time.deltaTime);
        });
    }

}
