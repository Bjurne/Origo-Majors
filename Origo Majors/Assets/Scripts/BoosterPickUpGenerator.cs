using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public GameObject warpBoosterPickUpPrefab;
    public GameObject mapperBoosterPickUpPrefab;
    public GameObject remapperBoosterPickUpPrefab;
    public GameObject[] waypoints;
    public int numberOfBoosterPickUps;

    void Start()
    {

        for (int i = 0; i < numberOfBoosterPickUps; i++)
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
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
            
        }

    }
}
