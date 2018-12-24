using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public GameObject warpBoosterPickUpPrefab;
    public GameObject throttleBoosterPickUpPrefab;
    public GameObject dupeBoosterPickUpPrefab;
    public GridNode[] gridNodes;
    public GameObject gridGenerator;
    public int numberOfBoosterPickUps;

    public int chanceToSpawnBooster;
    

    public void GenerateBoosterPickUps()
    {
        gridNodes = gridGenerator.GetComponent<GridGenerator>().activeNodes;

        int totalNumberOfDrones = FindObjectOfType<DronePlacement>().numberOfDronesToSpawn;
        int dimensionNumber = FindObjectOfType<StateManager>().currentDimension;


        if (dimensionNumber == 0) numberOfBoosterPickUps = (int)(totalNumberOfDrones / 2.5f);
        else numberOfBoosterPickUps += 2;

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
                int randomBoosterPickUp = Random.Range(0, 2); // <---- MVP only one booster available, change random range to 3 to enable the other boosters

                if (randomBoosterPickUp == 0)
                {
                    GameObject myBooster = Instantiate(warpBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else if (randomBoosterPickUp == 1)
                {
                    GameObject myBooster = Instantiate(throttleBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
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
                Debug.Log("This waypoint is allready holding a Booster Pick Up or is occupied");
            }
        }
    }

    public void CheckChanceToSpawnBooster()
    {
        if (chanceToSpawnBooster <= -1) chanceToSpawnBooster = 0;
        if (chanceToSpawnBooster > Random.Range(1,10))
        {
            SpawnBooster();
            chanceToSpawnBooster = 0;

            var numberOfBoostersInPlay = FindObjectsOfType<WarpBoosterScript>();
            Debug.Log("There are now " + numberOfBoostersInPlay.Length + " boosters in play");
        }
    }

    public void SpawnBooster()
    {
        for (int i = 0; i < 10;)
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
                int randomBoosterPickUp = Random.Range(0, 2); // <---- MVP only one booster available, change random range to 3 to enable the other boosters

                if (randomBoosterPickUp == 0)
                {
                    GameObject myBooster = Instantiate(warpBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else if (randomBoosterPickUp == 1)
                {
                    GameObject myBooster = Instantiate(throttleBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else
                {
                    GameObject myBooster = Instantiate(dupeBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }

                myNode.holdingBoosterPickUp = true;
                i=+10;

                Debug.Log("A booster has been spawned!");
            }
            else
            {
                Debug.Log("This node is cluttered, retrying to spawn booster");
                i++; // we give this up to ten goes before we opt out
            }
        }
    }
}
