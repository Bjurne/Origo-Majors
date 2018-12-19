using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlacement : MonoBehaviour {

    public GameObject clickListener;
    public int numberOfDronesSpawned;
    public int numberOfDronesToSpawnPerPlayer;
    public LayerMask waypoints;
    public GameObject selectedWaypoint;
    public GameObject dronePrefab;
    public int numberOfPlayers;
    public int numberOfDronesToSpawn;

    public StateManager stateManager;

    void Start () {
        clickListener.SetActive(false);

        try
        {
            numberOfDronesToSpawnPerPlayer = FindObjectOfType<StartupSettings>().numberOfSelectedDrones;
            numberOfPlayers = FindObjectOfType<StartupSettings>().numberOfSelectedplayers +1;
        }
        catch (Exception)
        {
            numberOfDronesToSpawnPerPlayer = 6;
            numberOfPlayers = 4;
            throw;
        }
        numberOfDronesToSpawn = numberOfDronesToSpawnPerPlayer * numberOfPlayers;
	}
    
    // Update is called once per frame
    //public void update()
    //{
    //    debug.log("runs placing ");
    //    if (input.getmousebuttonup(0))
    //    {
    //        placedrone();
    //    }
    //}

    public void PlaceDrone(Player player)
    {
        //foreach (Player Player in player)
        //{

        //}
        Vector3 clickPosition = -Vector3.one;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, waypoints))
        // och ifall selected waypoint inte är ockuperad
        {
            clickPosition = hit.point;
            selectedWaypoint = hit.collider.gameObject;
            Debug.Log("Youre clicking " + selectedWaypoint);
            bool occupied = selectedWaypoint.GetComponent<NodeContents>().occupied;

            if ((numberOfDronesSpawned < numberOfDronesToSpawn) && occupied == false)
            {
                bool teleporterPresent = selectedWaypoint.GetComponent<NodeContents>().holdingTeleporter;
                bool boosterPresent = selectedWaypoint.GetComponent<NodeContents>().holdingBoosterPickUp;

                if ((((numberOfDronesSpawned < numberOfDronesToSpawn) && occupied == false) && !teleporterPresent) && !boosterPresent)
                {
                    GameObject newDrone = Instantiate(dronePrefab, selectedWaypoint.transform.position, Quaternion.identity);
                    newDrone.transform.parent = selectedWaypoint.transform;

                    MeshRenderer[] children = newDrone.GetComponentsInChildren<MeshRenderer>();

                    foreach (MeshRenderer mesh in children)
                    {
                        mesh.material.color = GetPlayerColor(player);

                    }

                 //   newDrone.GetComponentInChildren<MeshRenderer>().material.color = GetPlayerColor(player);//set drone color and tag
                    Debug.Log(player.ToString());
                    newDrone.tag = player.ToString();

                    selectedWaypoint.GetComponent<NodeContents>().occupied = true;
                    newDrone.GetComponent<DroneLocation>().previouslyOccupiedWaypoint = selectedWaypoint;
                    newDrone.GetComponent<DroneLocation>().currentlyOccupiedWaypoint = selectedWaypoint;
                    numberOfDronesSpawned++;
                    Debug.Log(dronePrefab.name + " has been placed at " + selectedWaypoint.GetComponent<GridNode>().Coordinates);
                    stateManager.PassTurnToNextPlayer();
                }


                if (numberOfDronesSpawned == numberOfDronesToSpawn)
                {
                    placementPhaseDone();
                    
                }
            }
        }
    }

    public Color GetPlayerColor(Player player)
    {
        if (player == Player.Blue)
        {
            return Color.blue;
        }
        else if (player == Player.Green)
        {
            return Color.green;
        }
        else if (player == Player.Yellow)
        {
            return Color.yellow;
        }
        else
        {
            return Color.red;
        }
    }

    void placementPhaseDone()
    {
        Debug.Log("Placement phase is done");
        stateManager.initialPlacementIsDone = true;
        clickListener.SetActive(true);
    }
}
