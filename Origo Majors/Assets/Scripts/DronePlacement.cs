using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlacement : MonoBehaviour {

    public GameObject clickListener;
    private int numberOfDronesSpawned;
    public int numberOfDronesToSpawn;
    public LayerMask waypoints;
    public GameObject selectedWaypoint;
    public GameObject dronePrefab;

    void Start () {
        clickListener.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, waypoints))
            // och ifall selected waypoint inte är ockuperad
            {
                clickPosition = hit.point;
                selectedWaypoint = hit.collider.gameObject;
                bool occupied = selectedWaypoint.GetComponent<waypointContents>().occupied;

                if ((numberOfDronesSpawned < numberOfDronesToSpawn) && occupied == false)
                {
                    GameObject newDrone = Instantiate(dronePrefab, selectedWaypoint.transform.position, Quaternion.identity);
                    selectedWaypoint.GetComponent<waypointContents>().occupied = true;
                    newDrone.GetComponent<droneLocation>().previouslyOccupiedWaypoint = selectedWaypoint;
                    newDrone.GetComponent<droneLocation>().currentlyOccupiedWaypoint = selectedWaypoint;
                    numberOfDronesSpawned++;
                    Debug.Log(dronePrefab.name + " has been placed at " + selectedWaypoint.name);
                }
                if (numberOfDronesSpawned == numberOfDronesToSpawn)
                {
                    placementPhaseDone();
                    Debug.Log("Placement phase is done");
                }
            }

        }
    }

    void placementPhaseDone()
    {
        clickListener.SetActive(true);
    }
}
