using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLegalWarpDestination : MonoBehaviour {

    public GameObject currentlySelectedObject;
    public GridNode[] legalWarpNodes;
    public LayerMask legalWarpDestination;
    public LayerMask waypoints;
    public GameObject dice;
    public GameObject clickListener;
    private Vector3 nodeToCheckFrom;


    public void calculateLegalWarpDestinations()
    {
        int moveRange = dice.GetComponent<Dice>().moveRange;
        Vector3 dir = new Vector3();
        dir.Set(1,0,0);

        currentlySelectedObject = clickListener.GetComponent<clickListener>().currentlySelectedObject;

        Debug.Log("currentlySelectedObject is located at " + currentlySelectedObject.transform.position);

        for (int x = 0; x < 6; x++)
        {
            Debug.Log("moveRange = " + moveRange);
            for (int y = 0; y < moveRange; y++)
            {
                RaycastHit hit;
                if (nodeToCheckFrom == Vector3.zero) nodeToCheckFrom = currentlySelectedObject.transform.position;

                if (Physics.Raycast(nodeToCheckFrom, dir, out hit, 3, waypoints))
                    // TODO fixa rangen till gridGenerator.nodeRadius
                {
                    //Debug.DrawRay(currentlySelectedObject.transform.position, hit.point);
                    Debug.Log( hit.collider.GetComponent<GridNode>().Coordinates + " has been hit by the ray");
                }

                nodeToCheckFrom = hit.transform.position;
            }

            dir.Set(-1,0,0);
            nodeToCheckFrom = Vector3.zero;
        }
    }
            //dirXPositive  3.1, 0.0, 0.0
            //dirYPositive  1.6, 0.0, 2.7
            //dirZPositive  -1.6, 0.0, 2.7
            //dirXNegative  -3.1, 0.0, 0.0
            //dirYNegative  -1.6, 0.0, -2.7
            //dirZNegative  1.6, 0.0, -2.7
}
