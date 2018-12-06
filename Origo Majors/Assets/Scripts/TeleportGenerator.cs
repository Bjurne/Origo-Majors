using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGenerator : MonoBehaviour {

    public GameObject teleportPrefab;
    public GridNode[] gridNodes;
    public GameObject gridGenerator;
    private int numberOfNodes;

    public int numberOfTeleports;


    public void GenerateTeleports ()
    {
        gridNodes = gridGenerator.GetComponent<GridGenerator>().nodes;

        for (int i = 0; i < numberOfTeleports;)
        {
            int randomWaypoint = UnityEngine.Random.Range(0, gridNodes.Length);
            bool illegalSpawnPoint = gridNodes[randomWaypoint].GetComponent<WaypointContents>().holdingTeleporter;
            //bool illegalSpawnPoint = waypoints[randomWaypoint].gameObject.GetComponent<waypointContents>().holdingTeleporter;
            //bool illegalSpawnPoint = gridGenerator.GetComponent<GridGenerator>().nodes[randomWaypoint].GetComponent<waypointContents>().holdingTeleporter;


            if (!illegalSpawnPoint)
            {
                Instantiate(teleportPrefab, gridNodes[randomWaypoint].transform.position, Quaternion.identity);
                gridNodes[randomWaypoint].GetComponent<WaypointContents>().holdingTeleporter = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a teleporter");
            }
        }
		
	}
}
