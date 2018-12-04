using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    //public class Drawboard; 
	// Use this for initialization
	void Start () {

        //TODO:  add to start in each script that reference statemanager     :  
       // theStateManager = GameObject.FindObjectOfType<StateManager>();

    }

    // TODO : add to each script:     StateManager theStateManager;


    public int NumberOfplayers = 4;
    public int CurrentPlayerId = 0;

    // todo : add boolians in each phase and add them here

    bool isDonePlacing = false;

    void Update()
    {
        if (isDonePlacing == false)
        {

        }

        // Todo: add all boolians that check the turn status for each turn element

        if (true)
        {
            NextTurn();
        }
    }

    public void PlacementTurn ()    
    {
        if (isDonePlacing == true)
        {
            return;
        }
     //   DronePlacement(); 
        isDonePlacing = true;
    }



    public void NextTurn ()
    {


        // advances to next player 
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfplayers;
    }
	



	
}
