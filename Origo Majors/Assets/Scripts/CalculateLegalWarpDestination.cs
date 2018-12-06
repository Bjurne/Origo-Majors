using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLegalWarpDestination : MonoBehaviour {

    public GameObject currentlySelectedObject;
    public GridNode[] legalWarpNodes;
    public LayerMask waypoints;
    public GameObject dice;
    public GameObject clickListener;
    private Vector3 nodeToCheckFrom;


    public void calculateLegalWarpDestinations()
    {
        int moveRange = dice.GetComponent<Dice>().moveRange;
        //int moveRange = Random.Range(1, 5);
        //Debug.Log("MoveRange is " + moveRange);
        Vector3 dir = new Vector3();
        //dir.Set(1.0f,0.0f,0.0f);

        //Debug.Log("The distance to move is " + moveRange);

        currentlySelectedObject = clickListener.GetComponent<ClickListener>().currentlySelectedObject;

        //Debug.Log("currentlySelectedObject is located at " + currentlySelectedObject.transform.position);

        for (int x = 0; x < 6; x++)
        {
            //Debug.Log("moveRange = " + moveRange);

            if (x == 0) dir.Set(1.0f, 0.0f, 0.0f);
            if (x == 1) dir.Set(1.6f, 0.0f, 2.7f);
            if (x == 2) dir.Set(-1.6f, 0.0f, 2.7f);
            if (x == 3) dir.Set(-1.0f, 0.0f, 0.0f);
            if (x == 4) dir.Set(-1.6f, 0.0f, -2.7f);
            if (x == 5) dir.Set(1.6f, 0.0f, -2.7f);

            for (int y = 0; y < moveRange; y++)
            {
                RaycastHit hit;
                if (nodeToCheckFrom == Vector3.zero) nodeToCheckFrom = currentlySelectedObject.transform.position;

                try
                {
                    Debug.Log("Inne i Try");
                    if (Physics.Raycast(nodeToCheckFrom, dir, out hit, 100, waypoints))
                    // TODO fixa rangen till gridGenerator.nodeRadius
                    {
                        //Debug.DrawRay(currentlySelectedObject.transform.position, hit.point);
                        Debug.Log(hit.collider.GetComponent<GridNode>().Coordinates + " has been hit by the ray");
                        if (hit.collider.GetComponent<WaypointContents>().occupied)
                        {
                            Debug.Log("The path is blocked, and this move is illegal");
                            continue;
                        }
                        if (y == moveRange - 1)
                        {
                            hit.collider.GetComponent<GridNode>().gameObject.tag = "LegalWarpDestination";
                            Debug.Log(hit.collider.GetComponent<GridNode>().Coordinates + " has been added to legalWarpDestination");
                            hit.transform.localScale = new Vector3(2, 2, 2);
                        }
                    }
                    nodeToCheckFrom = hit.transform.position;
                }
                catch { }
            }

            
            


            nodeToCheckFrom = Vector3.zero;
        }
    }
            //dirXPositive  1.0, 0.0, 0.0
            //dirYPositive  1.6, 0.0, 2.7
            //dirZPositive  -1.6, 0.0, 2.7
            //dirXNegative  -1.0, 0.0, 0.0
            //dirYNegative  -1.6, 0.0, -2.7
            //dirZNegative  1.6, 0.0, -2.7
}
