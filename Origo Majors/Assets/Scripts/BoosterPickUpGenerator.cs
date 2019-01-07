using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickUpGenerator : MonoBehaviour {

    public AudioManager audiomanager;
    public GameObject warpBoosterPickUpPrefab;
    public GameObject throttleBoosterPickUpPrefab;
    public GameObject QuantumLeapBoosterPickUpPrefab;
    public GridNode[] gridNodes;
    public GameObject gridGenerator;
    public int numberOfBoosterPickUps;

    public int chanceToSpawnBooster;

    public void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }

    public void GenerateBoosterPickUps()
    {
        gridNodes = gridGenerator.GetComponent<GridGenerator>().activeNodes;

        int totalNumberOfDrones = FindObjectOfType<DronePlacement>().numberOfDronesToSpawn;
        int dimensionNumber = FindObjectOfType<StateManager>().currentDimension;


        if (dimensionNumber == 0) numberOfBoosterPickUps = Mathf.RoundToInt(totalNumberOfDrones * 0.75f);
        else numberOfBoosterPickUps = Mathf.RoundToInt(numberOfBoosterPickUps * 0.5f);
        

        for (int y = 0; y < numberOfBoosterPickUps; y++)
        {
            SpawnSingleBooster();
        }
    }
    
    public void SpawnSingleBooster()
    {
        for (int i = 0; i < 10;)
        {
            int randomWaypoint = UnityEngine.Random.Range(0, gridNodes.Length);
            bool illegalSpawnPoint = false;

            var myNode = gridNodes[randomWaypoint].GetComponent<NodeContents>();
            if ((myNode.holdingTeleporter) || myNode.holdingBoosterPickUp || myNode.occupied)
            {
                illegalSpawnPoint = true;
            }

            if (!illegalSpawnPoint)
            {
                int randomBoosterPickUp = UnityEngine.Random.Range(0, 4); // <---- MVP only one booster available, change random range to 3 to enable the other boosters

                if (randomBoosterPickUp == 0)
                {
                    GameObject myBooster = Instantiate(warpBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else if (randomBoosterPickUp == 1 || randomBoosterPickUp == 2)
                {
                    GameObject myBooster = Instantiate(throttleBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }
                else if (randomBoosterPickUp == 3)
                {
                    GameObject myBooster = Instantiate(QuantumLeapBoosterPickUpPrefab, myNode.transform.position, Quaternion.identity);
                    myBooster.transform.parent = myNode.transform;
                }

                myNode.holdingBoosterPickUp = true;
                i=+10;

                Debug.Log("A booster has been spawned!");
            }
            else
            {
                Debug.Log("This node is cluttered, retrying to spawn booster");
                i++; // we give each booster up to ten tries before we opt out, the board seems to be populated enough
            }
        }
    }

    public void CheckChanceToSpawnBooster()
    {
        if (chanceToSpawnBooster <= -1) chanceToSpawnBooster = 0;
        if (chanceToSpawnBooster > UnityEngine.Random.Range(1,6))
        {
            audiomanager.newBoosterSource.Play();
            SpawnSingleBooster();
            chanceToSpawnBooster = 0;
        }
    }
}
