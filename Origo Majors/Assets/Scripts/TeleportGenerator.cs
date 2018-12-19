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
        gridNodes = gridGenerator.GetComponent<GridGenerator>().activeNodes;

        int totalNumberOfDrones = FindObjectOfType<DronePlacement>().numberOfDronesToSpawn;
        int dimensionNumber = FindObjectOfType<StateManager>().currentDimension;

        //Debug.Log("totalNumberOfDrones = " + totalNumberOfDrones);
        //Debug.Log("numberOfDrones = " + FindObjectOfType<StartupSettings>().numberOfSelectedDrones);
        //Debug.Log("numberOfPlayers = " + FindObjectOfType<StartupSettings>().numberOfSelectedplayers);
        

        if (dimensionNumber == 0) numberOfTeleports = (int)(totalNumberOfDrones / 2);
        else if (dimensionNumber == 2) numberOfTeleports = 1;
        else numberOfTeleports /= 2;

        for (int i = 0; i < numberOfTeleports;)
        {
            int randomWaypoint = UnityEngine.Random.Range(0, gridNodes.Length);
            var myNode = gridNodes[randomWaypoint].GetComponent<NodeContents>();

            bool illegalSpawnPoint = false;
            
            if ((myNode.holdingTeleporter) || myNode.occupied)
            {
                illegalSpawnPoint = true;
            }


            if (!illegalSpawnPoint)
            {
                GameObject myTeleport = Instantiate(teleportPrefab, myNode.transform.position, Quaternion.identity);
                myTeleport.transform.parent = myNode.transform;
                myNode.holdingTeleporter = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a teleporter or is occupied");
            }
        }
		
	}
}
