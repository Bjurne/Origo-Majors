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

        for (int i = 0; i < numberOfTeleports;)
        {
            int randomWaypoint = UnityEngine.Random.Range(0, gridNodes.Length);
            var myNode = gridNodes[randomWaypoint].GetComponent<NodeContents>();
            bool illegalSpawnPoint = myNode.holdingTeleporter;


            if (!illegalSpawnPoint)
            {
                GameObject myTeleport = Instantiate(teleportPrefab, myNode.transform.position, Quaternion.identity);
                myTeleport.transform.parent = myNode.transform;
                myNode.holdingTeleporter = true;
                i++;
            }
            else
            {
                Debug.Log("This waypoint is allready holding a teleporter");
            }
        }
		
	}
}
