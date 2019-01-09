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

    public AudioManager audiomanager;
    public Player currentPlayer;
    public Player previousPlayer;
    public Player lastPlayerToEnterNewDimension;
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
    public bool pausExcecution = false;
    public bool autoRollerIsEnabled = true; // <----- TODO reference StartupSettings
    public bool startOfNewDimension = false;

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
        audiomanager = FindObjectOfType<AudioManager>();
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
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (!pausExcecution)
        {
            if (FindObjectOfType<CalculateLegalWarpDestination>().thisIsAQuantumLeap && isDoneRolling == false) diceRoller.Number();
            //if (isDoneRolling == false) diceRoller.Number();


            if (initialPlacementIsDone == true && isDoneRolling == false) // time to roll the dice
            {
                CountActivePlayers();
                diceRoller.transform.GetChild(1).GetComponent<Image>().sprite = rollSprite;
                diceRoller.SetRollButtonColor();
                rollButton.interactable = true;
                skipTurnButton.interactable = true;
                if (autoRollerIsEnabled && !isGameOver) diceRoller.Number();
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

                foreach (var drone in allDrones)
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
                rollButton.interactable = false; // disables button. is here for now


                FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster++;
                FindObjectOfType<BoosterPickUpGenerator>().CheckChanceToSpawnBooster();

                Debug.Log("chanceToSpawnBooster is now: " + FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster);

                isDoneRolling = false;
                isDoneMoving = false;

                Debug.Log("next turn");
                PassTurnToNextPlayer();
            }
        }
    }



    public void PassTurnToNextPlayer()
    {
        CountTeleports();
        if (initialPlacementIsDone == true) CountActivePlayers();

        Debug.Log(" passar turen ");

        if (currentPlayer == (Player)FindObjectOfType<StartupSettings>().numberOfSelectedplayers)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }

        if (startOfNewDimension)
        {
            currentPlayer = lastPlayerToEnterNewDimension;
            //startOfNewDimension = false;
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

        if (!isGameOver)
        {
            if (initialPlacementIsDone)
            {
                if (!startOfNewDimension) FindObjectOfType<textHandlerScript>().Print("Turn");
            }
            else
            {
                FindObjectOfType<textHandlerScript>().Print("PlacementTurn");
            }
        }

        if (initialPlacementIsDone)
        {
            Paus();
        }
    }

    private void CountPlayerDrones()
    {
        //TODO här letar vi efter vilka gameobjects som helst med rätt tag, vill egentligen bara hitta drönare med rätt tag
        //GameObject[] myDrones = GameObject.FindGameObjectsWithTag(currentPlayer.ToString()) as GameObject[];
        //Debug.Log(currentPlayer.ToString() + " has " + myDrones.Length + " drones left");

        int currentPlayersActiveDrones = 0;
        var myDrones = FindObjectsOfType<DroneLocation>();

        foreach (var drone in myDrones)
        {
            if (drone.tag == currentPlayer.ToString())
            {
                currentPlayersActiveDrones++;
            }
        }
        if (currentPlayersActiveDrones <= 0)
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

        var allScoredDrones = FindObjectOfType<ScoredDroneStorage>().scoredDrones;

        foreach (var scoredDrone in allScoredDrones)
        {
            if (scoredDrone.x == 0) blueDronesRemaining++;
            if (scoredDrone.x == 1) redDronesRemaining++;
            if (scoredDrone.x == 2) greenDronesRemaining++;
            if (scoredDrone.x == 3) yellowDronesRemaining++;
        }

        if (blueDronesRemaining >= 1) activePlayers++;
        if (redDronesRemaining >= 1) activePlayers++;
        if (greenDronesRemaining >= 1) activePlayers++;
        if (yellowDronesRemaining >= 1) activePlayers++;

        numberOfActivePlayers = activePlayers;

        Debug.Log("Blue has " + blueDronesRemaining + " drones left");
        Debug.Log("Red has " + redDronesRemaining + " drones left");
        Debug.Log("Green has " + greenDronesRemaining + " drones left");
        Debug.Log("Yellow has " + yellowDronesRemaining + " drones left");
        Debug.Log("Number of active players = " + numberOfActivePlayers);

        if (activePlayers == 1)
        {
            //Victory Screen
            Debug.Log("The game is over");
            isGameOver = true;
            //victoryScreen.GetComponent<VictoryScreenScript>().winnerName = currentPlayer.ToString();

            if (blueDronesRemaining > 0) victoryScreen.GetComponent<VictoryScreenScript>().winnerName = "Blue";
            if (redDronesRemaining > 0) victoryScreen.GetComponent<VictoryScreenScript>().winnerName = "Red";
            if (greenDronesRemaining > 0) victoryScreen.GetComponent<VictoryScreenScript>().winnerName = "Green";
            if (yellowDronesRemaining > 0) victoryScreen.GetComponent<VictoryScreenScript>().winnerName = "Yellow";

            victoryScreen.SetActive(true);
            victoryScreen.GetComponent<VictoryScreenScript>().DisplayVictoryScreen();
        }
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

        clickListener.ClearAllSelectionMarkers();
        CalculateLegalWarpDestination calculateMove = FindObjectOfType<CalculateLegalWarpDestination>();
        if (calculateMove.thisIsAQuantumLeap) calculateMove.thisIsAQuantumLeap = false;
        if (startOfNewDimension) startOfNewDimension = false;
        //Debug.Log(isDoneMoving + " " + isDoneRolling);
    }

    public void LoadNewDimension()
    {
        lastPlayerToEnterNewDimension = currentPlayer;
        Debug.Log(lastPlayerToEnterNewDimension.ToString() + " was the last to enter, he is going to start now");

        audiomanager.newDimensionSource.Play();

        ClearRemainingBoosterPickUps();
        ClearRemainingDrones();
        
        initialPlacementIsDone = true;
        isDoneRolling = true;
        isDoneMoving = true;
        isGameOver = false;

        //currentPlayer--;

        //pausExcecution = true;

        StartCoroutine(generateContent());
        

        //CountActivePlayers();
        //currentPlayer = lastPlayerToEnterNewDimension;
        //currentPlayer--;

        startOfNewDimension = true;

        //PassTurnToNextPlayer();
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

    private IEnumerator generateContent()
    {
        pausExcecution = true;
        StartCoroutine(FindObjectOfType<ScoredDroneStorage>().SpawnScoredDrones());
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<TeleportGenerator>().GenerateTeleports();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
        yield return new WaitForSeconds(0.5f);
        pausExcecution = false;
        FindObjectOfType<textHandlerScript>().Print("Turn");
    }

    public IEnumerator Paus()
    {
        Debug.Log("Starting paus");
        pausExcecution = true;
        yield return new WaitForSeconds(1);
        Debug.Log("Paus is done");
        pausExcecution = false;
    }
}

