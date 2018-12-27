using System;
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
    public Material legalWarpDestinationMarker;
    public bool thisIsAQuantumLeap = false;


    public void calculateLegalWarpDestinations()
    {
        if (thisIsAQuantumLeap)
        {
            Debug.Log("Ropar på calculate Quantum Leap");
            CalculateQuantumLeap();
        }
        else
        {
            int moveRange = dice.GetComponent<Dice>().moveRange;
            //int moveRange = Random.Range(1, 5);
            //Debug.Log("MoveRange is " + moveRange);
            Vector3 dir = new Vector3();
            nodeToCheckFrom = Vector3.zero; // kolla om det behövs . behövs kanske kinte sen men funkar nu
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
                        if (Physics.Raycast(nodeToCheckFrom, dir, out hit, 100, waypoints))
                        // TODO fixa rangen till gridGenerator.nodeRadius
                        {
                            //Debug.DrawRay(currentlySelectedObject.transform.position, hit.point);
                            //Debug.Log(hit.collider.GetComponent<GridNode>().Coordinates + " has been hit by the ray");
                            if (hit.collider.GetComponent<NodeContents>().occupied)
                            {
                                //Debug.Log("The path is blocked, and this move is illegal");
                                continue;
                            }
                            if (y == moveRange - 1)
                            {
                                hit.collider.GetComponent<GridNode>().gameObject.tag = "LegalWarpDestination";
                                //Debug.Log(hit.collider.GetComponent<GridNode>().Coordinates + " has been added to legalWarpDestination");
                                //hit.transform.localScale = new Vector3(2, 2, 2);

                                //Color nodeColor = hit.collider.GetComponent<MeshRenderer>().material.color;
                                //nodeColor.a = 255f;
                                //hit.collider.GetComponent<MeshRenderer>().material.color = nodeColor;

                                hit.collider.GetComponent<MeshRenderer>().material = legalWarpDestinationMarker;
                            }
                        }
                        nodeToCheckFrom = hit.transform.position;
                    }
                    catch { }
                }

                nodeToCheckFrom = Vector3.zero;

            }
        }
    }

    private void CalculateQuantumLeap()
    {
        Debug.Log("Quantum Leap calculating");

        int moveRange = dice.GetComponent<Dice>().moveRange;
        Vector3 dir = new Vector3();
        nodeToCheckFrom = Vector3.zero;

        currentlySelectedObject = clickListener.GetComponent<ClickListener>().currentlySelectedObject;

        for (int x = 0; x < 6; x++)
        {

            if (x == 0) dir.Set(1.0f, 0.0f, 0.0f);
            if (x == 1) dir.Set(1.6f, 0.0f, 2.7f);
            if (x == 2) dir.Set(-1.6f, 0.0f, 2.7f);
            if (x == 3) dir.Set(-1.0f, 0.0f, 0.0f);
            if (x == 4) dir.Set(-1.6f, 0.0f, -2.7f);
            if (x == 5) dir.Set(1.6f, 0.0f, -2.7f);


                RaycastHit hit;
                if (nodeToCheckFrom == Vector3.zero) nodeToCheckFrom = currentlySelectedObject.transform.position;

                try
                {
                    if (Physics.Raycast(nodeToCheckFrom, dir, out hit, 100, waypoints))
                    {
                        if (hit.collider.GetComponent<NodeContents>().occupied)
                        {
                            continue;
                        }
                        else
                        {
                            hit.collider.GetComponent<GridNode>().gameObject.tag = "LegalWarpDestination";
                            hit.collider.GetComponent<MeshRenderer>().material = legalWarpDestinationMarker;
                            nodeToCheckFrom = hit.transform.position;
                            for (int y = 0; y < 6; y++)
                            {
                                if (y == 0) dir.Set(1.0f, 0.0f, 0.0f);
                                if (y == 1) dir.Set(1.6f, 0.0f, 2.7f);
                                if (y == 2) dir.Set(-1.6f, 0.0f, 2.7f);
                                if (y == 3) dir.Set(-1.0f, 0.0f, 0.0f);
                                if (y == 4) dir.Set(-1.6f, 0.0f, -2.7f);
                                if (y == 5) dir.Set(1.6f, 0.0f, -2.7f);

                                try
                                {
                                    if (Physics.Raycast(nodeToCheckFrom, dir, out hit, 100, waypoints))
                                    {
                                        if (hit.collider.GetComponent<NodeContents>().occupied)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            hit.collider.GetComponent<GridNode>().gameObject.tag = "LegalWarpDestination";
                                            hit.collider.GetComponent<MeshRenderer>().material = legalWarpDestinationMarker;
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    nodeToCheckFrom = hit.transform.position;
                }
                catch { }

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
