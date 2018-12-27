using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum Player
{
    Blue,
    Red,
    Green,
    Yellow
}

public class StateManager : MonoBehaviour {


    public Player currentPlayer;
    public Player previousPlayer;
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
    public animController animationController;
    public GameObject userInterfaceCanvas;

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

    //The current dimension
    public int currentDimension = 0;

    //Used to actively count number of players remaning
    public int numberOfActivePlayers = 0;

    private int comboCounter = 0;

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

        //numberOfActivePlayers = FindObjectOfType<StartupSettings>().numberOfSelectedplayers;
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                try
                {
                    Destroy(FindObjectOfType<StartupSettings>().gameObject);
                }
                catch (Exception e)
                {
                }
                SceneManager.LoadScene("patriktest");
            }
        } 

        if (initialPlacementIsDone == true && isDoneRolling == false ) // time to roll the dice
        {
            // add a reset for dice roll image
            diceRoller.transform.GetChild(1).GetComponent<Image>().sprite = rollSprite;
            diceRoller.setRollButtonColor();
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

            ThrottleBar[] allThrottleBars = userInterfaceCanvas.GetComponentsInChildren<ThrottleBar>(true);

            foreach (var throttleBar in allThrottleBars)
            {
                if (throttleBar.gameObject.tag == currentPlayer.ToString())
                {
                    throttleBar.gameObject.SetActive(true);
                }
                else
                {
                    throttleBar.gameObject.SetActive(false);
                }
            }

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

            ThrottleBar[] allThrottleBars = userInterfaceCanvas.GetComponentsInChildren<ThrottleBar>(true);
            foreach (var throttleBar in allThrottleBars)
            {
                throttleBar.gameObject.SetActive(false);
            }

            clickListener.ClearCurrentlySelected();
            clickListener.ClearLegalWarpDestinations();
            Debug.Log( "next turn" );
            PassTurnToNextPlayer();
            rollButton.interactable = false; // disables button. is here for now

            
            FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster++;
            FindObjectOfType<BoosterPickUpGenerator>().CheckChanceToSpawnBooster();

            Debug.Log("chanceToSpawnBooster is now: " + FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster);

            isDoneRolling = false;
            isDoneMoving = false;

        }
    }



    public void PassTurnToNextPlayer()
    {
        CountTeleports();
        //if (initialPlacementIsDone == true) CountActivePlayers();
        Debug.Log(" passar turen ");
        
        if (currentPlayer == (Player)FindObjectOfType<StartupSettings>().numberOfSelectedplayers)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }

        //CountTotalDrones(); Gjordes för mvpn. Kan behövas för ett annat game mode?
        if (!isGameOver && initialPlacementIsDone)
        {
            CountPlayerDrones();
        }

        if (currentPlayer == previousPlayer)
        {
            comboCounter++;
            Debug.Log("Det är en combo!" + comboCounter);
        }
        else
        {
            comboCounter = 0;
        }

        if (currentPlayer != previousPlayer) animationController.SetUISize();
        previousPlayer = currentPlayer;
    }

    private void CountPlayerDrones()
    {
        //TODO här letar vi efter vilka gameobjects som helst med rätt tag, vill egentligen bara hitta drönare med rätt tag
        GameObject[] myDrones = GameObject.FindGameObjectsWithTag(currentPlayer.ToString()) as GameObject[];
        Debug.Log(currentPlayer.ToString() + " has " + myDrones.Length + " drones left");
        if (myDrones.Length <= 0)
        {
            Debug.Log("currentPlayer has no drones, next player!");
            PassTurnToNextPlayer();
        }
    }

    private void CountActivePlayers()
    {
        int activePlayers = 0;
        int blueDronesRemaining = 0;
        int redDronesRemaining = 0;
        int greenDronesRemaining = 0;
        int yellowDronesRemaining = 0;


        var allDrones = FindObjectsOfType<DroneLocation>();

        foreach (var drone in allDrones)
        {
            if (drone.tag == Player.Blue.ToString())
            {
                blueDronesRemaining++;
            }
            if (drone.tag == Player.Red.ToString())
            {
                redDronesRemaining++;
            }
            if (drone.tag == Player.Green.ToString())
            {
                greenDronesRemaining++;
            }
            if (drone.tag == Player.Yellow.ToString())
            {
                yellowDronesRemaining++;
            }
        }

        if (blueDronesRemaining >= 1) activePlayers++;
        if (redDronesRemaining >= 1) activePlayers++;
        if (greenDronesRemaining >= 1) activePlayers++;
        if (yellowDronesRemaining >= 1) activePlayers++;

        numberOfActivePlayers = activePlayers;
    }

    private void CountTotalDrones()
    {
        var allDrones = FindObjectsOfType<DroneLocation>();
        //Debug.Log(allDrones.Length + " drones are still in play");
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
        //Debug.Log(allTeleports.Length + " teleports are still in play");
        if (allTeleports.Length <= 0 && initialPlacementIsDone)
        {
            if (currentDimension == 2)
            {
                //Victory Screen
                Debug.Log("The game is over");
                isGameOver = true;

                victoryScreen.GetComponent<VictoryScreenScript>().winnerName = currentPlayer.ToString();

                victoryScreen.SetActive(true);
                victoryScreen.GetComponent<VictoryScreenScript>().DisplayVictoryScreen();
            }
            else
            {
                currentDimension++;
                LoadNewDimension();
            }
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
        //Debug.Log(isDoneMoving + " " + isDoneRolling);

        isDoneRolling = true;
        isDoneMoving = true;
        CalculateLegalWarpDestination calculateMove = FindObjectOfType<CalculateLegalWarpDestination>();
        if (calculateMove.thisIsAQuantumLeap) calculateMove.thisIsAQuantumLeap = false;
        //Debug.Log(isDoneMoving + " " + isDoneRolling);
    }

    public void LoadNewDimension()
    {
        ClearRemainingBoosterPickUps();
        ClearRemainingDrones();
        
        initialPlacementIsDone = true;
        isDoneRolling = true;
        isDoneMoving = true;
        isGameOver = false;

        currentPlayer--;
        
        FindObjectOfType<ScoredDroneStorage>().SpawnScoredDrones();
        FindObjectOfType<TeleportGenerator>().GenerateTeleports();
        FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
    }


    private void ClearRemainingBoosterPickUps()
    {
        WarpBoosterScript[] remainingWarpBoosterPickUps = FindObjectsOfType<WarpBoosterScript>();
        ThrottleBoosterScript[] remainingMapperBoosterPickUps = FindObjectsOfType<ThrottleBoosterScript>();
        QuantumLeapBoosterScript[] remainingDupeBoosterPickUps = FindObjectsOfType<QuantumLeapBoosterScript>();

        foreach (WarpBoosterScript boosterScript in remainingWarpBoosterPickUps)
        {
            boosterScript.GetComponentInParent<NodeContents>().holdingBoosterPickUp = false;
            Destroy(boosterScript.gameObject);
        }
        foreach (ThrottleBoosterScript boosterScript in remainingMapperBoosterPickUps)
        {
            boosterScript.GetComponentInParent<NodeContents>().holdingBoosterPickUp = false;
            Destroy(boosterScript.gameObject);
        }
        foreach (QuantumLeapBoosterScript boosterScript in remainingDupeBoosterPickUps)
        {
            boosterScript.GetComponentInParent<NodeContents>().holdingBoosterPickUp = false;
            Destroy(boosterScript.gameObject);
        }
    }

    private void ClearRemainingDrones()
    {
        var allDrones = FindObjectsOfType<DroneLocation>();
        foreach (var drone in allDrones)
        {
            drone.GetComponentInParent<NodeContents>().occupied = false;
            Destroy(drone.gameObject);
        }
    }
}

