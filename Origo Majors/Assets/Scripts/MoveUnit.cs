using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUnit : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    RaycastHit hit;


	public void OnBeginDrag (PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
	}

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }


    void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            transform.position = touchPosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            if (Physics.Raycast(Camera.main.transform.position, Input.mousePosition, out hit))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                transform.position = mousePosition;
            }
        }

        
    }
}
