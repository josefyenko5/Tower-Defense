using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    
    private Transform[] waypoints;

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
    
    public static Transform GetNextWaypoint (int waypointIndex) {
        var waypoints = i.waypoints;
        var currentWaypoint = waypoints[waypointIndex].transform;
        return currentWaypoint;
    }

    public static int WaypointLength () {
        return i.waypoints.Length;
    }

}
