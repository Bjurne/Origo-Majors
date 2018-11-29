using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGenerator : MonoBehaviour {

    public GameObject teleportPrefab;
    public GameObject[] waypoints;
    public int numberOfTeleports;

    void Start () {

        for (int i = 0; i < numberOfTeleports; i++)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);

            Instantiate(teleportPrefab, waypoints[randomWaypoint].transform.position, Quaternion.identity);
        }
		
	}
}
