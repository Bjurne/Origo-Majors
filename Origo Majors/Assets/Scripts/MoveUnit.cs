using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour {
    public GameObject unitToMove;

	void Start () {
		
	}
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            unitToMove = this.gameObject;
            unitToMove.transform.position = (Input.mousePosition);
        }
    }
}
