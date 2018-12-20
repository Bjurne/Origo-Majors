using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Inactive,
    PlaceStartingDrones,
    WaitForRoll,
    DecideActionBeforeMove,
    Move,
    DecideActionAfterMove,
    EndOfTurn
}

public class GameHandler : MonoBehaviour
{

    protected static GameHandler instance;
    public static GameHandler Instance { get { return instance; } }

    GameState currentGameState;

    PlayerInfo[] playerArray = { new PlayerInfo ("Blue", 0),
                                 new PlayerInfo ("Red", 1),
                                 new PlayerInfo ("Green", 2),
                                 new PlayerInfo ("Yellow", 3) };

    PlayerInfo currentPlayer;

    int availablePortals;
    int availableDimensions;
    int turnId;

    //REFERENCES
    public GridGenerator myGridGenerator;
    public CameraHolder myCameraHolder;
    public GameBoardScript myGameBoardScript;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        //myGridGenerator.CallGridGenerator(5, 1.8f);

        myGameBoardScript.MoveBoardToCenter();

        myCameraHolder.MoveCameraToCenter();

        Debug.Log("On Start(), initializing gameboard");

        SetPortalCount(5);
        SetDimensionCount(3);
        turnId = playerArray.Length;
        currentPlayer = playerArray[0]; //Blue

        GameHandler.Instance.ChangeState(GameState.Inactive);

    }

    //TODO: Make only the button relevant to the current state clickable.
    //TODO: currentPlayer stuff


    public void ChangeState(GameState newState)
    {
        currentGameState = newState;

        if (currentGameState == GameState.Inactive)
        {
            Debug.Log("GameState currently set to Inactive");
        }

        if (currentGameState == GameState.PlaceStartingDrones)
        {
            try
            {
                FindObjectOfType<TeleportGenerator>().GenerateTeleports();
                FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
            }
            catch
            {
                Debug.Log("No instance of TeleportGenerator or BoosterPickUpGenerator found.");
            }


            Debug.Log("Waiting for all drones to have been placed");
        }

        if (currentGameState == GameState.WaitForRoll)
        {
            //if currentPlayer.numberOfNumberofDrones == 0, NextPlayer();
            Debug.Log("Current player is: " + currentPlayer.name);
            Debug.Log("Waiting for die to have been rolled");
        }

        if (currentGameState == GameState.DecideActionBeforeMove)
        {
            //if currentPlayer.numberOfBoosters1 > 0, ChangeState(Move);
            Debug.Log("Waiting for decision before move to be made");
        }

        if (currentGameState == GameState.Move)
        {
            Debug.Log("Waiting for move to have been made");
        }

        if (currentGameState == GameState.DecideActionAfterMove)
        {
            //if currentPlayer.numberOfBoosters2 && 3 == 0, ChangeState(EndOfTurn);
            Debug.Log("Waiting for decision after move to be made");
        }

        if (currentGameState == GameState.EndOfTurn)
        {
            Debug.Log("Reached EndOfTurn, id: " + turnId + ", variables are: \n" +
                      "Portals: " + availablePortals + ", " +
                      "Dimensions: " + availableDimensions);

            if (availablePortals > 0)
            {
                NextPlayer();
            }

            else if (availableDimensions > 0)
            {
                NextDimension();
            }

            if (availableDimensions == 0)
            {
                Victory();
            }

        }

    }

    public void NextPlayer()
    {
        Debug.Log("NextPlayer()");

        turnId++;
        currentPlayer = playerArray[PlayerModulo(turnId)];

        Debug.Log("Going to GameLoop.WaitForRoll");
        GameHandler.Instance.ChangeState(GameState.WaitForRoll);
    }

    public void NextDimension()
    {
        Debug.Log("NextDimension()");
        ReduceDimensionCount();
        availablePortals = 5;
        Debug.Log("New Dimension, variables are: \n" +
          "Portals: " + availablePortals + ", " +
          "Dimensions: " + availableDimensions);

        NextPlayer();
    }

    public void Victory()
    {
        Debug.Log("Victory()");
        Debug.Log("Game Ended! The winner is X! Do you want to play again?");
    }

    public void SetPortalCount(int p)
    {
        availablePortals = p;
        Debug.Log("Set Portal count to: " + p);
    }

    public void SetDimensionCount(int d)
    {
        availableDimensions = d;
        Debug.Log("Set Dimension count to: " + d);
    }

    public void ReducePortalCount()
    {
        availablePortals -= 1;
        Debug.Log("Reduced Portal count by 1");
    }

    public void ReduceDimensionCount()
    {
        availableDimensions -= 1;
        Debug.Log("Reduced Dimension count by 1");
    }

    public void CheckPortalsAfterMove()
    {
        if (availablePortals == 0)
        {
            Debug.Log("Detected shortage of portals in this Dimension, force end of turn");
            GameHandler.Instance.ChangeState(GameState.EndOfTurn);
        }
        else
        {
            Debug.Log("Portals still available, moving on");
            GameHandler.Instance.ChangeState(GameState.DecideActionAfterMove);

        }
    }

    public GameState GetCurrentState()
    {
        return currentGameState;
    }

    public void SetCurrentPlayer(int number)
    {
        currentPlayer = playerArray[number];
    }

    public PlayerInfo GetCurrentPlayer()
    {
        return currentPlayer;

    }

    private int PlayerModulo(int turnId)
    {
        int newId = turnId % playerArray.Length;
        return newId;
    }

    public GameObject GetRandomNode()
    {
        int hello = Random.Range(0, myGridGenerator.activeNodes.Length);

        return myGridGenerator.activeNodes[hello].gameObject;
    }

}