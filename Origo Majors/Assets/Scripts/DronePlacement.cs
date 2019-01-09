using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlacement : MonoBehaviour {

    public GameObject clickListener;
    public GameObject diceCanvas;
    public AudioManager audiomanager;
    public int numberOfDronesSpawned;
    public int numberOfDronesToSpawnPerPlayer;
    public LayerMask waypoints;
    public GameObject selectedWaypoint;
    public GameObject dronePrefab;
    public int numberOfPlayers;
    public int numberOfDronesToSpawn;

    public GameObject droneModel01;
    public GameObject droneModel02;
    public GameObject droneModel03;
    public GameObject droneModel04;

    public StateManager stateManager;

    void Start () {
        clickListener.SetActive(false);

        try
        {
            numberOfDronesToSpawnPerPlayer = FindObjectOfType<StartupSettings>().numberOfSelectedDrones;
            numberOfPlayers = FindObjectOfType<StartupSettings>().numberOfSelectedplayers +1;
        }
        catch (Exception e)
        {
            numberOfDronesToSpawnPerPlayer = 6;
            numberOfPlayers = 4;
        }
        numberOfDronesToSpawn = numberOfDronesToSpawnPerPlayer * numberOfPlayers;
	}    //    }
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
                    if (stateManager.currentPlayer == Player.Blue) dronePrefab = droneModel01;
                    if (stateManager.currentPlayer == Player.Red) dronePrefab = droneModel02;
                    if (stateManager.currentPlayer == Player.Green) dronePrefab = droneModel03;
                    if (stateManager.currentPlayer == Player.Yellow) dronePrefab = droneModel04;

                    GameObject newDrone = Instantiate(dronePrefab, selectedWaypoint.transform.position, Quaternion.identity);
                    newDrone.transform.parent = selectedWaypoint.transform;

                    ParticleSystem particleSpawn = newDrone.transform.GetChild(2).GetComponent<ParticleSystem>();
                    particleSpawn.startColor = GetPlayerColor(player);

                    audiomanager.droneRespawnSource.Play();

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
        diceCanvas.SetActive(true);
        stateManager.initialPlacementIsDone = true;
        clickListener.SetActive(true);
    }
}
