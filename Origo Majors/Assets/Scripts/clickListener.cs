using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickListener : MonoBehaviour {

    public LayerMask selectable;
    public LayerMask waypoints;
    public GameObject currentlySelectedObject = null;
    public GameObject selectionMarker = null;
    public GameObject selectedWaypoint = null;
    //public GameObject previouslyOccupiedWaypoint = null;


    void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {

            Vector3 clickPosition = -Vector3.one;
            Plane plane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, waypoints) && currentlySelectedObject != null)
            // och ifall selected waypoint = legal waypoint destination
            {
                clickPosition = hit.point;
                selectedWaypoint = hit.collider.gameObject;
                bool occupied = selectedWaypoint.GetComponent<waypointContents>().occupied;

                if (!occupied)
                {
                    currentlySelectedObject.transform.position = selectedWaypoint.transform.position;
                    selectedWaypoint.GetComponent<waypointContents>().occupied = true;
                    currentlySelectedObject.GetComponent<droneLocation>().changeLocation();

                    Debug.Log(currentlySelectedObject.name + " has been moved to " + selectedWaypoint.name);

                    selectionMarker.SetActive(false); // denna funkar inte för tillfället, av någon anledning
                    currentlySelectedObject = null;
                    selectionMarker = null;
                    selectedWaypoint = null;
                }
            }

            if (currentlySelectedObject != null)
            {
                selectionMarker.SetActive(false);
                currentlySelectedObject = null;
                selectionMarker = null;
                selectedWaypoint = null;
            }

            if (Physics.Raycast(ray, out hit, 1000f, selectable))
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

    //void SetSelectedObject()
    //{

    //}
}
