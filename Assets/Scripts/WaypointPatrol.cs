using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [Tooltip("List of waypoint game objects for the enemy to move to, in order.")]
    public List<Transform> waypoints;
    [Tooltip("Speed of enemy")]
    public float speed = 1;

    private int curpointidx;
    private int lastpointidx;

    // Start is called before the first frame update
    void Start()
    {
        curpointidx = 0;
        lastpointidx = waypoints.Count - 1; //Assuming that count is the size of the list 

        // Safety check to make sure nothing is null, if something is then destroy gameobject
        foreach (var point in waypoints)
        {
            if (point == null)
            {
                Destroy(gameObject);
                print("WAYPOINT BOI HAS NULL WAYPOINT FOR SOME REASON: DESTROYING");
                throw new MissingReferenceException();
            }

            if (point.position.z != 0)
                point.position = new Vector3(point.position.x, point.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint(waypoints[curpointidx]);
    }

    // Moves this transform towards destination by step units
    private void MoveToWaypoint(Transform destination)
    {

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, step);

        transform.right = destination.position - transform.position;

        if (transform.position == destination.position)
        {
            curpointidx++;
        }

        if (curpointidx > lastpointidx)
            curpointidx = 0;

    }
}
