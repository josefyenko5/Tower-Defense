using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    
    private Transform[] waypoints;
    private int waypointIndex;

    #region Singleton
    public static Waypoints i;
    private void Singleton () {
        if (i != null && i != this) {
            Destroy(this.gameObject);
        }else {
            i = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    private void Awake() {
        Singleton();
        waypoints = transform.Cast<Transform>().ToArray();
    }

    private void Start () {
        waypointIndex = 0;
    }
    
    public static Transform GetNextWaypoint () {
        var waypoints = i.waypoints;
        var waypointIndex = i.waypointIndex;
        var currentWaypoint = waypoints[waypointIndex].transform;
        i.waypointIndex = (i.waypointIndex + 1) % waypoints.Length;
        return currentWaypoint;
    }

}
