using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredDroneStorage : MonoBehaviour {

    public List<Vector4> scoredDrones;
    public GameObject dronePrefab;
    public AudioManager audiomanager;

    public GameObject droneModel01;
    public GameObject droneModel02;
    public GameObject droneModel03;
    public GameObject droneModel04;

    public IEnumerator SpawnScoredDrones()
    {
        //FindObjectOfType<StateManager>().pausExcecution = true;
        for (int i = 0; i < scoredDrones.Count; i++)
        {
            Player player = (Player)scoredDrones[i].x;
            int x = (int)scoredDrones[i].y;
            int y = (int)scoredDrones[i].z;
            int z = (int)scoredDrones[i].w;

            GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
            foreach (var gridNode in gridGenerator.activeNodes)
            {
                Vector3 nodeCoordinates = gridNode.Coordinates;
                if (nodeCoordinates.x == x && nodeCoordinates.y == y && nodeCoordinates.z == z)
                {
                    Transform nodeToRespawnAt = gridNode.gameObject.transform;

                    //dronePrefab = StartupSettings.selectedDroneModel(player);
                    //TODO reference startupsettings for drone model
                    StateManager stateManager = FindObjectOfType<StateManager>();

                    SetPlayerDroneModel(player);


                    GameObject respawnedDrone = Instantiate(dronePrefab, nodeToRespawnAt.position, Quaternion.identity);
                    respawnedDrone.transform.parent = nodeToRespawnAt;
                    respawnedDrone.tag = player.ToString();
                    
                    DronePlacement dronePlacement = FindObjectOfType<DronePlacement>();

                    ParticleSystem particleSpawn = respawnedDrone.transform.GetChild(2).GetComponent<ParticleSystem>();
                    particleSpawn.startColor = dronePlacement.GetPlayerColor(player);

                    audiomanager.droneRespawnSource.Play();

                    MeshRenderer[] children = respawnedDrone.GetComponentsInChildren<MeshRenderer>();


                    foreach (MeshRenderer mesh in children)
                    {
                        mesh.material.color = dronePlacement.GetPlayerColor(player);
                    }

                    nodeToRespawnAt.gameObject.GetComponent<NodeContents>().occupied = true;
                    respawnedDrone.GetComponent<DroneLocation>().previouslyOccupiedWaypoint = nodeToRespawnAt.gameObject;
                    respawnedDrone.GetComponent<DroneLocation>().currentlyOccupiedWaypoint = nodeToRespawnAt.gameObject;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        scoredDrones.Clear();
        //FindObjectOfType<StateManager>().pausExcecution = false;

        //FindObjectOfType<TeleportGenerator>().GenerateTeleports();
        //FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();

        yield return null;
    }

    private void SetPlayerDroneModel(Player player)
    {
        StartupSettings startUpSettings = FindObjectOfType<StartupSettings>();
        StateManager stateManager = FindObjectOfType<StateManager>();

        Player currentPlayer = player;

        if (currentPlayer == Player.Blue)
        {
            if (startUpSettings.player1DroneSelected == 0) dronePrefab = droneModel01;
            else if (startUpSettings.player1DroneSelected == 1) dronePrefab = droneModel02;
            else if (startUpSettings.player1DroneSelected == 2) dronePrefab = droneModel03;
            else if (startUpSettings.player1DroneSelected == 3) dronePrefab = droneModel04;
        }

        else if (currentPlayer == Player.Red)
        {
            if (startUpSettings.player2DroneSelected == 0) dronePrefab = droneModel01;
            else if (startUpSettings.player2DroneSelected == 1) dronePrefab = droneModel02;
            else if (startUpSettings.player2DroneSelected == 2) dronePrefab = droneModel03;
            else if (startUpSettings.player2DroneSelected == 3) dronePrefab = droneModel04;
        }

        else if (currentPlayer == Player.Green)
        {
            if (startUpSettings.player3DroneSelected == 0) dronePrefab = droneModel01;
            else if (startUpSettings.player3DroneSelected == 1) dronePrefab = droneModel02;
            else if (startUpSettings.player3DroneSelected == 2) dronePrefab = droneModel03;
            else if (startUpSettings.player3DroneSelected == 3) dronePrefab = droneModel04;
        }

        else if (currentPlayer == Player.Yellow)
        {
            if (startUpSettings.player4DroneSelected == 0) dronePrefab = droneModel01;
            else if (startUpSettings.player4DroneSelected == 1) dronePrefab = droneModel02;
            else if (startUpSettings.player4DroneSelected == 2) dronePrefab = droneModel03;
            else if (startUpSettings.player4DroneSelected == 3) dronePrefab = droneModel04;
        }
    }
}
