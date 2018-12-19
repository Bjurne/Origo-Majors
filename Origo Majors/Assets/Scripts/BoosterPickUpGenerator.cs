using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public GameObject warpBoosterPickUpPrefab;
    public GameObject mapperBoosterPickUpPrefab;
    public GameObject dupeBoosterPickUpPrefab;
    public GridNode[] gridNodes;
    public GameObject gridGenerator;
    public int numberOfBoosterPickUps;
    private int dimensionNumber;


    public void GenerateBoosterPickUps()
    {
        gridNodes = gridGenerator.GetComponent<GridGenerator>().activeNodes;

        int totalNumberOfDrones = (FindObjectOfType<StartupSettings>().numberOfSelectedDrones + 2) * (FindObjectOfType<StartupSettings>().numberOfSelectedplayers + 1);

        if (dimensionNumber == 0) numberOfBoosterPickUps = (int)(totalNumberOfDrones / 2.5f);
        else numberOfBoosterPickUps -= 2;

        for (int i = 0; i < numberOfBoosterPickUps;)
        {
            int randomWaypoint = Random.Range(0, gridNodes.Length);
            bool illegalSpawnPoint = false;

            var myNode = gridNodes[randomWaypoint].GetComponent<NodeContents>();
            if ((myNode.holdingTeleporter) || myNode.holdingBoosterPickUp || myNode.occupied)
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
                    GameObject myBooster = Instantiate(dupeBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
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
