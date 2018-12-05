using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGenerator : MonoBehaviour {

    public GameObject teleportPrefab;
    public GridNode[] waypoints;
    public GameObject gridGenerator;
    private int numberOfNodes;

    public int numberOfTeleports;


    void Start () {

        waypoints = gridGenerator.GetComponent<GridGenerator>().nodes;

        for (int i = 0; i < numberOfTeleports;)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
            bool illegalSpawnPoint = waypoints[randomWaypoint].GetComponent<waypointContents>().holdingTeleporter;
            //bool illegalSpawnPoint = waypoints[randomWaypoint].gameObject.GetComponent<waypointContents>().holdingTeleporter;
            //bool illegalSpawnPoint = gridGenerator.GetComponent<GridGenerator>().nodes[randomWaypoint].GetComponent<waypointContents>().holdingTeleporter;


            if (!illegalSpawnPoint)
            {
                Instantiate(teleportPrefab, waypoints[randomWaypoint].transform.position, Quaternion.identity);
                waypoints[randomWaypoint].GetComponent<waypointContents>().holdingTeleporter = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a teleporter");
            }
        }
		
	}
}
