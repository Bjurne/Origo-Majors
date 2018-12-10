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

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public float outerLeft = -10f;
    public float outerRight = 10f;


    void Start ()
    {
        myCamera.transform.LookAt(transform.position + cameraOffset);
        transform.position = MoveToCenter();
    }

    void Update ()
    {

        CameraDrag();

        //Regular ol button-input rotation
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            x = Input.GetAxisRaw("Horizontal");
            CameraRotation(x);
        }
        //IdleRotation();
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

    void IdleRotation ()
    {
        transform.Rotate(turnSpeed * Time.deltaTime);
    }

    void CameraRotation (float x)
    {
        transform.Rotate(0, x, 0);
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

            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(0, pos.x * dragSpeed, 0);

            if (move.x > 0f)
            {
                if (this.transform.position.x < outerRight)
                {
                    transform.Rotate(move);
                }
            }
            else
            {
                if (this.transform.position.x > outerLeft)
                {
                    transform.Rotate(move);
                }
            }
        }
    }


}
