using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public GameObject warpBoosterPickUpPrefab;
    public GameObject mapperBoosterPickUpPrefab;
    public GameObject remapperBoosterPickUpPrefab;
    public GridNode[] waypoints;
    public GameObject gridGenerator;
    public int numberOfBoosterPickUps;

    void Start()
    {
        waypoints = gridGenerator.GetComponent<GridGenerator>().nodes;

        for (int i = 0; i < numberOfBoosterPickUps;)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
            bool illegalSpawnPoint = waypoints[randomWaypoint].GetComponent<WaypointContents>().holdingBoosterPickUp;

            if (!illegalSpawnPoint)
            {
                int randomBoosterPickUp = Random.Range(0, 3);

                if (randomBoosterPickUp == 0)
                {
                    Instantiate(warpBoosterPickUpPrefab, waypoints[randomWaypoint].transform.position, Quaternion.identity);
                }
                else if (randomBoosterPickUp == 1)
                {
                    Instantiate(mapperBoosterPickUpPrefab, waypoints[randomWaypoint].transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(remapperBoosterPickUpPrefab, waypoints[randomWaypoint].transform.position, Quaternion.identity);
                }

                waypoints[randomWaypoint].GetComponent<WaypointContents>().holdingBoosterPickUp = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a Booster Pick Up");
            }
        }
    }
}
