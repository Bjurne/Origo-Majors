using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredDroneStorage : MonoBehaviour {

    public List<Vector4> scoredDrones;
    public GameObject dronePrefab;

    public void SpawnScoredDrones()
    {

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

                    GameObject respawnedDrone = Instantiate(dronePrefab, nodeToRespawnAt.position, Quaternion.identity);
                    respawnedDrone.transform.parent = nodeToRespawnAt;
                    respawnedDrone.tag = player.ToString();


                    MeshRenderer[] children = respawnedDrone.GetComponentsInChildren<MeshRenderer>();

                    DronePlacement dronePlacement = FindObjectOfType<DronePlacement>();

                    foreach (MeshRenderer mesh in children)
                    {
                        mesh.material.color = dronePlacement.GetPlayerColor(player);
                    }
                }
            }
        }

        scoredDrones.Clear();

    }
}
