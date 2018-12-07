using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContents : MonoBehaviour {

    public bool occupied = false;
    public bool holdingTeleporter = false;
    public bool holdingBoosterPickUp = false;
    
    
	public void OnDroneEnter () {
        if (holdingBoosterPickUp && occupied)
        {
            Debug.Log("A booster has been picked up!");
        }

        if (holdingTeleporter && occupied)
        {
            Debug.Log("A teleporter has been entered!");
        }
	}
}
