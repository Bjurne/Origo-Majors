﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickListener : MonoBehaviour {

    public LayerMask selectable;
    public LayerMask waypoints;
    public StateManager stateManager;
    public GameObject currentlySelectedObject = null;
    public GameObject selectionMarker = null;
    public GameObject selectedWaypoint = null;
    public CalculateLegalWarpDestination legalMoves;
    private bool hasBeenMoved = false;
    private Ray ray;
    private RaycastHit hit;
    //private Vector3 clickPosition = -Vector3.one;
    private Plane plane = new Plane(Vector3.up, 0f);
    public Material hexGridMaterial;

    private bool inMotion = false;



    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(FindObjectOfType<StartupSettings>().gameObject);
            SceneManager.LoadScene("patriktest");
        }

        if (Input.GetMouseButtonUp (0))
        {
            hasBeenMoved = false;
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            ClickForMovement();

            ClearLegalWarpDestinations();

            ClearCurrentlySelected();

            ClickForSelection();

            ClickForPlane(); // This is currently only for debugging. Might come in handy for Drag'n'Drop stuff
            
            //Debug.Log(clickPosition);
        }
    }


    private void ClickForMovement()
    {
        if (Physics.Raycast(ray, out hit, 1000f, waypoints) && currentlySelectedObject != null)
        // Vi kollar i nästa steg ifall den valda waypointen är ockuperad eller inte
        {
            //clickPosition = hit.point;
            selectedWaypoint = hit.collider.gameObject;
            bool occupied = selectedWaypoint.GetComponent<NodeContents>().occupied;

            if ((!occupied) && selectedWaypoint.tag == "LegalWarpDestination")
            {
                MoveDrone();
            }
        }
    }

    private void MoveDrone()
    {
        inMotion = true;
        StartCoroutine(AnimateDrone(currentlySelectedObject, selectedWaypoint.transform.position));

        selectedWaypoint.GetComponent<NodeContents>().occupied = true;

    }

    public void ClearLegalWarpDestinations()
    {
        GridNode[] gos = GridNode.FindObjectsOfType(typeof(GridNode)) as GridNode[];
        foreach (GridNode gn in gos)
        {
            if (gn.gameObject.tag == "LegalWarpDestination")
            {
                gn.GetComponent<MeshRenderer>().material = hexGridMaterial ;
                gn.tag = "Untagged";
            }
        }
    }

    public void ClearCurrentlySelected()
    {
        if (currentlySelectedObject != null)
        {
            if (selectionMarker != null) selectionMarker.SetActive(false);

            if (!inMotion)
            {
            currentlySelectedObject = null;
            selectionMarker = null;
            selectedWaypoint = null;
            }
        }
    }

    private void ClickForSelection()
    {
        if ((Physics.Raycast(ray, out hit, 1000f, selectable)) && !hasBeenMoved)
        // och om pjäsen tillhör mig / current player
        {
            //clickPosition = hit.point;
            currentlySelectedObject = hit.rigidbody.gameObject;
            selectionMarker = currentlySelectedObject.transform.GetChild(0).gameObject;
            selectionMarker.SetActive(true);
            if (currentlySelectedObject != null)
            {
                Debug.Log("Currently selected object is " + currentlySelectedObject.name);
                Debug.Log("The Selection Marker of Currently selected object is " + selectionMarker.name);
            }
            legalMoves.calculateLegalWarpDestinations();
        }
        else
        {
            //currentlySelectedObject.transform.GetChild(0).gameObject.SetActive(false);
            //currentlySelectedObject = null;
            Debug.Log("Currently selected object is none");
            Debug.Log("The Selection Marker of Currently selected object is none");
        }
    }

    private void ClickForPlane()
    {
        float distanceToPlane;
        if (plane.Raycast(ray, out distanceToPlane))
        {
            //clickPosition = ray.GetPoint(distanceToPlane);
        }
    }

    private IEnumerator AnimateDrone(GameObject SelectedThing, Vector3 moveToPos)
    {
        bool notDoneMoving = true;
        float stepCounter = 0;
        Vector3 originalPos;
        originalPos = SelectedThing.transform.position;

        while (notDoneMoving)
        {
            if (SelectedThing.transform.position == moveToPos)
            {
                notDoneMoving = false;
            }
            SelectedThing.transform.position = Vector3.Lerp(originalPos, moveToPos, stepCounter);
            stepCounter += 0.05f;
            yield return null;
        }
        Debug.Log("Movement complete, calling some neat functions");

        currentlySelectedObject.transform.parent = selectedWaypoint.transform;

        currentlySelectedObject.GetComponent<DroneLocation>().ChangeLocation();
        selectedWaypoint.GetComponent<NodeContents>().OnDroneEnter();

        Debug.Log(currentlySelectedObject.name + " is being moved to " + selectedWaypoint.GetComponent<GridNode>().Coordinates);

        hasBeenMoved = true;
        selectionMarker.SetActive(false);
        selectionMarker = null;
        currentlySelectedObject = null;
        selectedWaypoint = null;

        inMotion = false;
        stateManager.isDoneMoving = true;
    }

}
