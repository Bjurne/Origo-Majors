using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {

    public Camera myCamera;
    public GridGenerator gridGenerator;
    public GameObject cameraArm;

    public Vector3 turnSpeed = new Vector3(0, 100, 0);
    private Vector3 cameraOffset = new Vector3(0, -2, 0);
    public int zoomStrength;
    
    //Temp controller
    float x;

    // camera drag variabler
    public float dragSpeed = 200;
    private Vector3 dragOrigin;
    internal bool cameraDragging = true;
    float originEulerY;
    float originEulerZ;
    private Vector3 movez;
    private Vector3 armRotationChecker;
    public float minZoom;
    public float maxZoom;
    public bool cameraControl;

    void Update ()
    {
        // if (klickar på någonting som inte är klickbart (din färgs drönare, etc.)
        if (cameraControl == true)
        {
            CameraDrag();
            CameraZoom();
        }
        else if (cameraControl == false)
        {
            IdleRotation();
        }
        //TODO: Implementera denna (i ClickListener?) och kalla enbart på detta om man inte klickar på något annat.

        //Om man vill använda knappar
        //ButtonRotation();

        //Exempel på någon typ av "stand-by"

    }

    private void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomStrength;

            if ((myCamera.transform.localPosition.x - zoom) > minZoom && (myCamera.transform.localPosition.x - zoom) < maxZoom)
            {
                myCamera.transform.localPosition -= new Vector3(zoom, 0, 0);
            }
        }
    }

    public void MoveCameraToCenter()
    {
        myCamera.transform.LookAt(transform.position + cameraOffset);
        MoveToCenter();
    }

    public void MoveToCenter ()
    {
        GridNode testNode;
        if (gridGenerator.dic.TryGetValue(gridGenerator.FindCenter(), out testNode))
        {
            transform.position = testNode.gameObject.transform.position;
        }
        else
        {
        Debug.Log("CameraHolder can't find center node!");
        }
    }

    void IdleRotation ()
    {
        transform.Rotate(turnSpeed * Time.deltaTime);
    }

    void ButtonRotation ()
    {
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            x = Input.GetAxisRaw("Horizontal");
            transform.Rotate(0, x, 0);
        }

    }

    void CameraDrag ()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }

        if (cameraDragging)
        {
            if (Input.GetMouseButtonDown(1)) //När man högerklickar hämtar den information om musens dåvarande position
            {
                dragOrigin = Input.mousePosition;
                originEulerY = transform.eulerAngles.y;
                originEulerZ = cameraArm.transform.eulerAngles.z;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 movex = new Vector3(0, pos.x * dragSpeed, 0);
            Vector3 movez = new Vector3(0, 0, (pos.y * dragSpeed) * -1);

            transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, originEulerY + movex.y, this.transform.eulerAngles.z);
            armRotationChecker = new Vector3(cameraArm.transform.eulerAngles.x, cameraArm.transform.eulerAngles.y, originEulerZ + movez.z);
            if (armRotationChecker.z > 10 && armRotationChecker.z < 80) //Kollar om kameran har tänkts roterat inom de givna värdena, uppdaterar kameran om så är fallet
            {
                cameraArm.transform.eulerAngles = armRotationChecker;
            }
        }
    }

}
