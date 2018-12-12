﻿using System.Collections;
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


            GameObject myBooster = GetComponentInChildren<BoosterScript>().gameObject;

            //Det vi behöver göra här är att kolla vilken typ av booster som blivit upplockad.
            //tex genom att kolla (myBooster.name), eller döpa om "BoosterScript" till tre olika namn och ge de olika
            //Booster prefabsen varsitt, och sedan GetComponentInChildren<ETT UTAV BOOSTER-NAMNEN>()
            // sedan if myBooster is Booster a - do this
            // if myBooster is Booster b - do that
            // if myBooster is Booster c - do them other things


            Destroy(myBooster);
            holdingBoosterPickUp = false;

            stateManager.currentPlayer--;

            var allDrones = FindObjectsOfType<DroneLocation>();

            foreach (var drone in allDrones)
            {
                if (drone.tag == stateManager.currentPlayer.ToString())
                {
                    drone.gameObject.layer = 10; // refenses to, selctable layer
                }
                else
                {
                    drone.gameObject.layer = 11; // refenses to nonselctable layer
                }
            }
        }

        if (holdingTeleporter && occupied)
        {
            Debug.Log("A teleporter has been entered!");
            stateManager = FindObjectOfType<StateManager>();
            if (stateManager.currentPlayer == Player.Blue)
            {
                stateManager.blueScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.blueScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Red)
            {
                stateManager.redScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.redScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Green)
            {
                stateManager.greenScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.greenScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Yellow)
            {
                stateManager.yellowScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.yellowScore + " points!");
            }
            GameObject myTeleporter = GetComponentInChildren<TeleporterScript>().gameObject;
            Destroy(myTeleporter);
            holdingTeleporter = false;
            occupied = false;

            GameObject myDrone = GetComponentInChildren<DroneLocation>().gameObject;
            myDrone.GetComponent<DroneLocation>().currentlyOccupiedWaypoint = null;
            myDrone.GetComponent<DroneLocation>().previouslyOccupiedWaypoint = null;
            Destroy(myDrone);
        }
	}
}
