using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneLocation : MonoBehaviour {

    public GameObject previouslyOccupiedWaypoint = null;
    public GameObject currentlyOccupiedWaypoint = null;


    public void changeLocation () {
        if (previouslyOccupiedWaypoint != null)
        {
            previouslyOccupiedWaypoint = currentlyOccupiedWaypoint;
            Debug.Log(previouslyOccupiedWaypoint + " is saved as prev. waypoint");

            previouslyOccupiedWaypoint.GetComponent<waypointContents>().occupied = false;
            Debug.Log(previouslyOccupiedWaypoint + " is no longer occupied");

            currentlyOccupiedWaypoint = FindObjectOfType<clickListener>().selectedWaypoint;
            Debug.Log(currentlyOccupiedWaypoint + " is now the new position");

            currentlyOccupiedWaypoint.GetComponent<waypointContents>().occupied = true;
            Debug.Log(currentlyOccupiedWaypoint + " is now set to occupied");

        }
    }
}
