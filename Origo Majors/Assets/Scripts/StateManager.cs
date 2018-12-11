﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Player
{
    Blue,
    Red,
    Green,
    Yellow
}

public class StateManager : MonoBehaviour {

    void Start()
    {
        // draw nodes
        //SÅHÄR KKALLAR MAN PÅ GRID GENERATOR NU
        gridGenerator.CallGridGenerator(5, 1.8f);
        currentPlayer = Player.Blue;     // blue player starts
        rollButton.interactable = false; // rollbutton disabled
    }

    public Player currentPlayer;
    public GameObject dronePlaceholderPrefab;

    // reference other scripts
    public GridGenerator gridGenerator;
    public DronePlacement initialPlacement; 
    public Dice diceRoller;
    public Button rollButton;

    public Sprite rollSprite; 

    // layers
    public LayerMask selectable;
    public LayerMask nonSelectable;

    // turn stucture bools
    public bool initialPlacementIsDone = false;
    public bool isDoneRolling = false; 
    public bool isDoneMoving= false;
    public bool isGameOver = false;

    //Player scores
    public int blueScore = 0;
    public int redScore = 0;
    public int greenScore = 0;
    public int yellowScore = 0;


    void Update()
    {
        // place the initial drones
        if (initialPlacementIsDone == false)
        {
            rollButton.interactable = false;
            //        Debug.Log(" started if sttement ");

            if (Input.GetMouseButtonUp(0))
            {
                initialPlacement.PlaceDrone(currentPlayer);
            }
        } 

        if (initialPlacementIsDone == true && isDoneRolling == false ) // time to roll the dice
        {
            // add a reset for dice roll image
            diceRoller.transform.GetChild(1).GetComponent<Image>().sprite = rollSprite;


            rollButton.interactable = true;
            
            
        }

        if (initialPlacementIsDone == true && isDoneRolling == true && isDoneMoving == false)
        {
            rollButton.interactable = false;
            Debug.Log(" Time to selct ");
            //Select all drones so we can turn them off.
            var allDrones = FindObjectsOfType<DroneLocation>();

            foreach(var drone in allDrones)
            {
                if (drone.tag == currentPlayer.ToString())
                {
                    drone.gameObject.layer = 10; // refenses to, selctable layer
                }
                else
                {
                    drone.gameObject.layer = 11; // refenses to nonselctable layer
                }
            }
        }

        if (initialPlacementIsDone == true && isDoneRolling == true && isDoneMoving == true)
        {
            Debug.Log( "next turn" );
            PassTurnToNextPlayer();
            rollButton.interactable = false; // disables button. is here for now

            isDoneRolling = false;
            isDoneMoving = false;
}
    }

    public void PassTurnToNextPlayer()
    {
        Debug.Log(" passar turen ");

        if (currentPlayer == Player.Yellow)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }
        
        CountTotalDrones();
        if (!isGameOver)
        {
            CountPlayerDrones();
        }
    }

    private void CountTotalDrones()
    {
        var allDrones = FindObjectsOfType<DroneLocation>();
        Debug.Log(allDrones.Length + " drones are still in play");
        if (allDrones.Length <= 1 && initialPlacementIsDone)
        {
            //Victory Screen
            Debug.Log("The game is over");
            isGameOver = true;
        }
    }

    private void CountPlayerDrones()
    {
        GameObject[] myDrones = GameObject.FindGameObjectsWithTag(currentPlayer.ToString()) as GameObject[];
        if (myDrones.Length <= 0 && initialPlacementIsDone)
        {
            PassTurnToNextPlayer();
        }
    }


    public void initialPlacementIsDoneTurn ()    
    {
        if (initialPlacementIsDone == true)
        {
            return;
        }
    
        initialPlacementIsDone = true;
    }

	



	
}
