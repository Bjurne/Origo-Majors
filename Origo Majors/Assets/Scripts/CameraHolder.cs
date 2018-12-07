using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {

    public Camera myCamera;
    public GridGenerator gridGenerator;

    Vector3 turnSpeed = new Vector3(0, 100, 0);
    Vector3 cameraOffset = new Vector3(0, -7, 0);

    void Start ()
    {
        myCamera.transform.LookAt(transform.position + cameraOffset);
        transform.position = MoveToCenter();
    }

    void Update ()
    {
        transform.Rotate(turnSpeed * Time.deltaTime);
    }

    public Vector3 MoveToCenter ()
    {
        Vector3 cameraHeight = new Vector3(0, 0, 0);
        GridNode testNode;
        if (gridGenerator.dic.TryGetValue(gridGenerator.FindCenter(), out testNode))
        {
            return testNode.gameObject.transform.position + cameraHeight;
        }
        Debug.Log("Debug!");
        return Vector3.zero;
    }


}
