using System;
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

    private void Awake()
    {
        //SÅHÄR KKALLAR MAN PÅ GRID GENERATOR NU
        gridGenerator.CallGridGenerator(5, 1.8f);
    }

    void Start()
    {
        // draw nodes
        currentPlayer = Player.Blue;     // blue player starts
        rollButton.interactable = false; // rollbutton disabled
        skipTurnButton.interactable = false;

    }

    public Player currentPlayer;
    public GameObject dronePlaceholderPrefab;

    // reference other scripts
    public GridGenerator gridGenerator;
    public DronePlacement initialPlacement; 
    public Dice diceRoller;
    public Button rollButton;
    public Button skipTurnButton;
    public ClickListener clickListener;
    public GameObject victoryScreen;

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
          //  rollButton.interactable = false;
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
            //rollButton.GetComponent<Image>().color = ;
            rollButton.interactable = true;
            skipTurnButton.interactable = true;
            
        }
        if (Input.GetKeyDown("enter"))
            {
                SkipTurn();
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

        if (initialPlacementIsDone == true && isDoneRolling == true && isDoneMoving == true && isGameOver == false)
        {
            var allDrones = FindObjectsOfType<DroneLocation>();

            foreach (var drone in allDrones)
            {
                if (drone.tag == currentPlayer.ToString())
                {
                    drone.gameObject.layer = 11; // refenses to, nonselctable layer
                }
            }
            clickListener.ClearCurrentlySelected();
            clickListener.ClearLegalWarpDestinations();
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
        CountTeleports();
        if (!isGameOver)
        {
            CountPlayerDrones();
        }
    }
    
    private void CountTotalDrones()
    {
        var allDrones = FindObjectsOfType<DroneLocation>();
        Debug.Log(allDrones.Length + " drones are still in play");
        if (allDrones.Length <= 0 && initialPlacementIsDone)
        {
            //Victory Screen
            Debug.Log("The game is over");
            isGameOver = true;
            victoryScreen.SetActive(true);
            victoryScreen.GetComponent<VictoryScreenScript>().DisplayVictoryScreen();
        }
    }

    private void CountTeleports()
    {
        var allTeleports = FindObjectsOfType<TeleporterScript>();
        Debug.Log(allTeleports.Length + " teleports are still in play");
        if (allTeleports.Length <= 0 && initialPlacementIsDone)
        {
            //Victory Screen
            Debug.Log("The game is over");
            isGameOver = true;
            victoryScreen.SetActive(true);
            victoryScreen.GetComponent<VictoryScreenScript>().DisplayVictoryScreen();
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

    public void SkipTurn()
    {
        Debug.Log("skipturn clicked mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        isDoneRolling = true;
        isDoneMoving = true;
    }




}

/*
public enum BigState
{
    //Menu-stuff,
    StartUpGameBoard,       //Initialize gridgenerator
    GenerateObjects,        //Portals and boosters
    PlaceStartingLocations, //Self-explanatory
    PlayGameLoop,           //Loop through this until game is done
    SetNewGameBoard,        //Save entered portal positions, clear gameboard objects,
                            //place drones at previous portal positions, generate new portals & boosters,
                            //go run through PlayGameLoop
    VictoryState            //Announce winner, Player can go back to menu
}

public enum GameLoop
{
    WaitForRoll, //Player can click roll button
    DecideActionBeforeMove, //Show legal moves. Player can Use Booster, Move or Skip to end
    Move, //Player can click on any drone and move a drone
    DecideActionAfterMove, //Player can use Booster or Skip to end
    EndOfTurn //End of turn, check amount of portals available, if 0, go to next stage
}

    void Start ()
    {
        BigState(StartUpGameBoard);

        BigState(GenerateObjects);
    }

    void Update ()
    {
        if(BigState == 2)
        {
            PlaceStartingLocations();
        }

        if(BigState == 3)
        {
            currentPlayer = 0;

            if (GameLoop == 0)
            {
                //Do stuff with currentPlayer
            }
            if (GameLoop == 1)
            {
                //Do stuff with currentPlayer
            }
            if (GameLoop == 2)
            {
                //Do stuff with currentPlayer
            }
            if (GameLoop == 3)
            {
                //Do stuff with currentPlayer
            }
            if (GameLoop == 4)
            {
                if (available portals = 0)
                {
                    if (available dimensions = 0)
                    {
                        BigState(VictoryState); // WIN GAME
                    }
                    else
                    {
                    BigState(SetNewGameBoard); //SetNewGameBoard (DO A BUNCH OF SHIT)
                    }
                }
                else
                {
                    currentPlayer++;
                    GameLoop = 0; //Go again with a new player
                }
            }
        }
    }
    */
