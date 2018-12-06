using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public GameObject warpBoosterPickUpPrefab;
    public GameObject mapperBoosterPickUpPrefab;
    public GameObject remapperBoosterPickUpPrefab;
    public GridNode[] gridNodes;
    public GameObject gridGenerator;
    public int numberOfBoosterPickUps;

    public void GenerateBoosterPickUps()
    {
        gridNodes = gridGenerator.GetComponent<GridGenerator>().nodes;

        for (int i = 0; i < numberOfBoosterPickUps;)
        {
            int randomWaypoint = Random.Range(0, gridNodes.Length);
            bool illegalSpawnPoint = false;

            var myNode = gridNodes[randomWaypoint].GetComponent<WaypointContents>();
            if ((myNode.holdingTeleporter) || myNode.holdingBoosterPickUp)
            {
                illegalSpawnPoint = true;
            }

            if (!illegalSpawnPoint)
            {
                int randomBoosterPickUp = Random.Range(0, 3);

                if (randomBoosterPickUp == 0)
                {
                    Instantiate(warpBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                }
                else if (randomBoosterPickUp == 1)
                {
                    Instantiate(mapperBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(remapperBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                }

                myNode.holdingBoosterPickUp = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a Booster Pick Up");
            }
        }
    }
}
