using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredDroneStorage : MonoBehaviour {

    public List<Vector4> scoredDrones;
    public GameObject dronePrefab;
    public AudioManager audiomanager;

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

                    GameObject respawnedDrone = Instantiate(dronePrefab, nodeToRespawnAt.position, Quaternion.identity);
                    respawnedDrone.transform.parent = nodeToRespawnAt;
                    respawnedDrone.tag = player.ToString();
                    
                    audiomanager.droneRespawnSource.Play();

                    MeshRenderer[] children = respawnedDrone.GetComponentsInChildren<MeshRenderer>();

                    DronePlacement dronePlacement = FindObjectOfType<DronePlacement>();

                    foreach (MeshRenderer mesh in children)
                    {
                        mesh.material.color = dronePlacement.GetPlayerColor(player);
                    }

                    nodeToRespawnAt.gameObject.GetComponent<NodeContents>().occupied = true;
                    respawnedDrone.GetComponent<DroneLocation>().previouslyOccupiedWaypoint = nodeToRespawnAt.gameObject;
                    respawnedDrone.GetComponent<DroneLocation>().currentlyOccupiedWaypoint = nodeToRespawnAt.gameObject;
                }
            }
            yield return new WaitForSeconds(1f);
        }

        scoredDrones.Clear();
        FindObjectOfType<StateManager>().pausExcecution = false;

        yield return null;
    }
}
