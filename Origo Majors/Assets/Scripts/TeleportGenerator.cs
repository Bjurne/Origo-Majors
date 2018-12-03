using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGenerator : MonoBehaviour {

    public GameObject teleportPrefab;
    public GameObject[] waypoints;
    public int numberOfTeleports;

    void Start () {

        for (int i = 0; i < numberOfTeleports;)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
            bool illegalSpawnPoint = waypoints[randomWaypoint].GetComponent<waypointContents>().holdingTeleporter;

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
