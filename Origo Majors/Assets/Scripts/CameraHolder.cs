using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {

    public Camera myCamera;
    public GridGenerator gridGenerator;

    public Vector3 turnSpeed = new Vector3(0, 100, 0);
    Vector3 cameraOffset = new Vector3(0, -7, 0);
    
    //Temp controller
    float x;

    // camera drag variabler
    public float dragSpeed = 200;
    private Vector3 dragOrigin;
    internal bool cameraDragging = true;
    float originEulerY;

    void Update ()
    {
        // if (klickar på någonting som inte är klickbart (din färgs drönare, etc.)
        CameraDrag();
        //TODO: Implementera denna (i ClickListener?) och kalla enbart på detta om man inte klickar på något annat.

        //Om man vill använda knappar
        //ButtonRotation();

        //Exempel på någon typ av "stand-by"
        //IdleRotation();
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

            if (Input.GetMouseButtonDown(1))
            {
                dragOrigin = Input.mousePosition;
                originEulerY = transform.eulerAngles.y;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(0, pos.x * dragSpeed, 0);

            transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, originEulerY + move.y, this.transform.eulerAngles.z);
        }
    }


}
