using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour {

    public LayerMask selectable;
    public LayerMask waypoints;
    //public LayerMask legalWarpDestination;
    public GameObject currentlySelectedObject = null;
    public GameObject selectionMarker = null;
    public GameObject selectedWaypoint = null;
    public CalculateLegalWarpDestination legalMoves;
    private bool hasBeenMoved = false;


    void Update ()
    {
        if (Input.GetMouseButtonUp (0))
        {

            Vector3 clickPosition = -Vector3.one;
            Plane plane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;
            RaycastHit hit;
            hasBeenMoved = false;


            if (Physics.Raycast(ray, out hit, 1000f, waypoints) && currentlySelectedObject != null)
            // Vi kollar i nästa steg ifall den valda waypointen är ockuperad eller inte
            {
                clickPosition = hit.point;
                selectedWaypoint = hit.collider.gameObject;
                bool occupied = selectedWaypoint.GetComponent<WaypointContents>().occupied;

                if ((!occupied) && selectedWaypoint.tag == "LegalWarpDestination")
                {
                    currentlySelectedObject.transform.position = selectedWaypoint.transform.position;
                    selectedWaypoint.GetComponent<WaypointContents>().occupied = true;
                    currentlySelectedObject.GetComponent<DroneLocation>().ChangeLocation();
                    selectedWaypoint.GetComponent<WaypointContents>().OnDroneEnter();

                    Debug.Log(currentlySelectedObject.name + " has been moved to " + selectedWaypoint.GetComponent<GridNode>().Coordinates);

                    Vector3 directionMoved = currentlySelectedObject.GetComponent<DroneLocation>().currentlyOccupiedWaypoint.transform.position - currentlySelectedObject.GetComponent<DroneLocation>().previouslyOccupiedWaypoint.transform.position;
                    Debug.Log("direction is " + directionMoved);

                    hasBeenMoved = true;
                    selectionMarker.SetActive(false); // denna funkar inte för tillfället, av någon anledning
                    currentlySelectedObject = null;
                    selectionMarker = null;
                    selectedWaypoint = null;
                }
            }

            GridNode[] gos = GridNode.FindObjectsOfType(typeof(GridNode)) as GridNode[];
            foreach (GridNode gn in gos)
            {
                if (gn.gameObject.tag == "LegalWarpDestination")
                {
                    gn.tag = "Untagged";
                }
            }

            if (currentlySelectedObject != null)
            {
                selectionMarker.SetActive(false);
                currentlySelectedObject = null;
                selectionMarker = null;
                selectedWaypoint = null;
            }

            if ((Physics.Raycast(ray, out hit, 1000f, selectable)) && !hasBeenMoved)
                // och om pjäsen tillhör mig / current player
            {
                clickPosition = hit.point;
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

            if (plane.Raycast(ray, out distanceToPlane))
            {
                clickPosition = ray.GetPoint(distanceToPlane);
            }

            Debug.Log(clickPosition);
        }
    }

    void SetSelectedObject()
    {

    }
}
