using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLocation : MonoBehaviour {

    public GameObject previouslyOccupiedWaypoint = null;
    public GameObject currentlyOccupiedWaypoint = null;


    public void ChangeLocation () {
        if (previouslyOccupiedWaypoint != null)
        {
            previouslyOccupiedWaypoint = currentlyOccupiedWaypoint;
            //Debug.Log(previouslyOccupiedWaypoint + " is saved as prev. waypoint");

            previouslyOccupiedWaypoint.GetComponent<NodeContents>().occupied = false;
            //Debug.Log(previouslyOccupiedWaypoint + " is no longer occupied");

            currentlyOccupiedWaypoint = FindObjectOfType<ClickListener>().selectedWaypoint;
            //Debug.Log(currentlyOccupiedWaypoint + " is now the new position");

            currentlyOccupiedWaypoint.GetComponent<NodeContents>().occupied = true;
            //Debug.Log(currentlyOccupiedWaypoint + " is now set to occupied");
        }
    }
}
