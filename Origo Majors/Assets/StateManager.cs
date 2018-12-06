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
        gridscript.GenerateGameBoard(9); // number 9 to be changed later
        currentPlayer = Player.Blue;     // blue player starts
        rollButton.interactable = false; // rollbutton disabled
    }

    public Player currentPlayer;

    // reference other scripts
    public GridGenerator gridscript;
    public DronePlacement initialPlacement; 
    public Dice diceRoller;
    public Button rollButton;

    
    
    public bool initialPlacementIsDone = false;
    bool isDoneRolling = false; 
    bool isDoneMoving= false;


    void Update()
    {
        // place the initial drones
        if (initialPlacementIsDone == false)
        {
            Debug.Log(" started if sttement ");

            if (Input.GetMouseButtonUp(0))
            {
                initialPlacement.PlaceDrone(currentPlayer);

            }
        } 

        //intial drones are place and its time for the first real turn. time to roll dice
        if (initialPlacementIsDone == true && isDoneRolling == false )
        {
            diceRoller.Number(); // TODO: check roll number funktion button , change script to roll instead of rolling number directly
            isDoneRolling = true;
        }

        if (initialPlacementIsDone == true && isDoneRolling == true && isDoneMoving == false)
        {
            //Select all drones so we can turn them off.
            var allDrones = FindObjectsOfType<DroneLocation>();

            foreach(var drone in allDrones)
            {
                if (drone.tag == currentPlayer.ToString())
                {
                    //activate  drone
                }
                else
                {
                    //deactivate drone
                }
            }

            // moveunit - move a drone the omount rolled. 
            // if no legal moves continue; ? 
        }

        if (true)
        {
            //NextTurn();
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
