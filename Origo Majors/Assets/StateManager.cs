using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	void Start () {
        gridscript.GenerateGameBoard(9); // number 9 to be changed later
        

    }

    
    public GridGenerator gridscript;
    public DronePlacement initialPlacement; 
    public Dice diceRoller; 

    public int NumberOfplayers = 4;
    public int CurrentPlayerId = 0;

    // todo : add boolians in each phase and add them here

    bool initialPlacementIsDone = false;
    bool isDoneRolling = false; 
    bool isDoneMoving= false;



    void Update()
    {
        // place the initial drones
      /*  if (initialPlacementIsDone == false)
        {
            initialPlacement.Placing();
            initialPlacementIsDone = true;
       }*/ 

        //intial drones are place and its time for the first real turn. time to roll dice
        if (initialPlacementIsDone == true && isDoneRolling == false )
        {
            diceRoller.Number(); // TODO: check roll number funktion button , change script to roll instead of rolling number directly
            isDoneRolling = true;
        }

        if (initialPlacementIsDone == true && isDoneRolling == true && isDoneMoving == false)
        {
            // moveunit - move a drone the omount rolled. 
            // if no legal moves continue; ? 
        }
        


        if (true)
        {
            NextTurn();
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



    public void NextTurn ()
    {


        // advances to next player 
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfplayers;
    }
	



	
}
