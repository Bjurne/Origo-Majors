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
            bool occupied = selectedWaypoint.GetComponent<waypointContents>().occupied;

            if ((numberOfDronesSpawned < numberOfDronesToSpawn) && occupied == false)
            {
                GameObject newDrone = Instantiate(dronePrefab, selectedWaypoint.transform.position, Quaternion.identity);

                //set drone color and tag
                newDrone.GetComponentInChildren<MeshRenderer>().material.color = GetPlayerColor(player);
                Debug.Log(player.ToString());
                newDrone.tag = player.ToString();

                selectedWaypoint.GetComponent<waypointContents>().occupied = true;
                newDrone.GetComponent<droneLocation>().previouslyOccupiedWaypoint = selectedWaypoint;
                newDrone.GetComponent<droneLocation>().currentlyOccupiedWaypoint = selectedWaypoint;
                numberOfDronesSpawned++;
                Debug.Log(dronePrefab.name + " has been placed at " + selectedWaypoint.GetComponent<GridNode>().coordinates);

                            stateManager.PassTurnToNextPlayer();

            }
                
            if (numberOfDronesSpawned == numberOfDronesToSpawn)
            {
                placementPhaseDone();
                Debug.Log("Placement phase is done");
                stateManager.initialPlacementIsDone = true;
            }
        }
    }

    private Color GetPlayerColor(Player player)
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
        clickListener.SetActive(true);
    }
}
