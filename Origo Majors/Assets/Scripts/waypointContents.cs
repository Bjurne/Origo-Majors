using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContents : MonoBehaviour {

    public bool occupied = false;
    public bool holdingTeleporter = false;
    public bool holdingBoosterPickUp = false;
    public StateManager stateManager;


    public void OnDroneEnter () {
        if (holdingBoosterPickUp && occupied)
        {
            Debug.Log("A booster has been picked up!");
            stateManager = FindObjectOfType<StateManager>();
            stateManager.currentPlayer--;
            GameObject myBooster = GetComponentInChildren<ParticleSystem>().gameObject;
            // TODO fixa något bättre att referera till än ett particelSystem, detta är wonky,
            //speciellt då Teleporters eventuellt också vill ha ett particelSystem
            Destroy(myBooster);
            holdingBoosterPickUp = false;
        }

        if (holdingTeleporter && occupied)
        {
            Debug.Log("A teleporter has been entered!");
        }
	}
}
