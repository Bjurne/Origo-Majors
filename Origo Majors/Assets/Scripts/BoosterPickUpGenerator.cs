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
        gridNodes = gridGenerator.GetComponent<GridGenerator>().activeNodes;

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
                int randomBoosterPickUp = Random.Range(0, 1); // <---- MVP only one booster available, change random range to 3 to enable the other boosters

                if (randomBoosterPickUp == 0)
                {
                    GameObject myBooster = Instantiate(warpBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else if (randomBoosterPickUp == 1)
                {
                    GameObject myBooster = Instantiate(mapperBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else
                {
                    GameObject myBooster = Instantiate(remapperBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
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
