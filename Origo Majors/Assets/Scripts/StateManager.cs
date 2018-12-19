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
    public CameraHolder myCameraHolder;
    public GameBoardScript myGameBoardScript;

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

    private void Awake()
    {
        gridGenerator.CallGridGenerator(5, 1.8f);

        myGameBoardScript.MoveBoardToCenter();

        myCameraHolder.MoveCameraToCenter();
    }

    void Start()
    {
        // draw nodes
        currentPlayer = Player.Blue;     // blue player starts
        rollButton.interactable = false; // rollbutton disabled
        skipTurnButton.interactable = false;

    }

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
            setRollButtonColor();
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
            //Debug.Log(" Time to selct ");
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

    private void setRollButtonColor()
    {
        Vector4 rollButtonColor = Color.white;
        if (currentPlayer == Player.Blue)
        {
            rollButtonColor = Color.blue;
        }
        else if (currentPlayer == Player.Red)
        {
            rollButtonColor = Color.red;
        }
        else if (currentPlayer == Player.Green)
        {
            rollButtonColor = Color.green;
        }
        else if (currentPlayer == Player.Yellow)
        {
            rollButtonColor = Color.yellow;
        }
        rollButton.GetComponent<Image>().color = rollButtonColor;
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
            //Debug.Log("The game is over");
            //isGameOver = true;
            //victoryScreen.SetActive(true);
            //victoryScreen.GetComponent<VictoryScreenScript>().DisplayVictoryScreen();

            LoadNewDimension();
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
        //Debug.Log("skipturn clicked mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        //isDoneRolling = true;
        //isDoneMoving = true;
        Debug.Log(isDoneMoving + " " + isDoneRolling);

        isDoneRolling = true;
        isDoneMoving = true;
        Debug.Log(isDoneMoving + " " + isDoneRolling);
    }

    public void LoadNewDimension()
    {
        WarpBoosterScript[] remainingWarpBoosterPickUps = FindObjectsOfType<WarpBoosterScript>();

        foreach (WarpBoosterScript boosterScript in remainingWarpBoosterPickUps)
        {
            Destroy(boosterScript.gameObject);
        }


        initialPlacementIsDone = true;
        isDoneRolling = false;
        isDoneMoving = false;
        isGameOver = false;

        currentPlayer = Player.Blue;

        FindObjectOfType<ScoredDroneStorage>().SpawnScoredDrones();

        FindObjectOfType<TeleportGenerator>().GenerateTeleports();
        FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
    }


}

/*
public enum BigState
{
    //Menu-stuff,
    //StartUpGameBoard      //Initialize gridgenerator
    //GenerateObjects       //Portals and boosters
    PlaceStartingDrones,    //Self-explanatory
    PlayGameLoop,           //Loop through this until game is done
    //SetNewGameBoard       //Save entered portal positions, clear gameboard objects,
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
        StartUpGameBoard();
        GenerateObjects();

    }

    void Update ()
    {
        if(currentState == BigState.PlaceStartingDrones)
        {
            //Do stuff
        }

        if(currentState == BigState.PlayGameLoop)
        {

            if (currentGameLoop == GameLoop.WaitForRoll)
            {
                //Do stuff with currentPlayer
            }
            if (currentGameLoop == GameLoop.DecideActionBeforeMove)
            {
                //Do stuff with currentPlayer
            }
            if (currentGameLoop == GameLoop.Move)
            {
                //Do stuff with currentPlayer
            }
            if (currentGameLoop == GameLoop.DecideActionAfterMove)
            {
                //Do stuff with currentPlayer
            }
            if (currentGameLoop == GameLoop.EndOfTurn)
            {
                if (availablePortals = 0)
                {
                    if (availableDimensions = 0)
                    {
                        VictoryState(); // Exits loop
                        
                    }
                    else
                    {
                        SetNewGameBoard(); // (DO A BUNCH OF SHIT)
                        currentGameLoop = GameLoop.WaitForRoll; //New dimension, back to gameLoop
                    }
                }
                else
                {
                    currentPlayer++;
                    currentGameLoop = GameLoop.WaitForRoll; //Go again with a new player
                }
            }
        }
    }
    */
