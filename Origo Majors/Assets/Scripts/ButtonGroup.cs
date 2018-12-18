using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroup : MonoBehaviour
{

    int rng;

    public void A_PlaceMyDrone()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Place Starting Drones");
        GameHandler.Instance.ChangeState(GameState.WaitForRoll);
    }

    public void A_Roll()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Roll");
        GameHandler.Instance.ChangeState(GameState.DecideActionBeforeMove);
    }

    public void A_BeforeMove()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Make a Decision Before Move");
        GameHandler.Instance.ChangeState(GameState.Move);
    }

    public void A_Move()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Make Move");

        rng = Random.Range(0, 2);
        if (rng == 1)
        {
            Debug.Log("Found and entered a portal");
            GameHandler.Instance.ReducePortalCount();
        }
        else
        {
            Debug.Log("Found no portal, availablePortals stays the same");
        }
        GameHandler.Instance.CheckPortalsAfterMove();
    }

    public void A_AfterMove()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Make a Decision (Passed the Turn)");
        GameHandler.Instance.ChangeState(GameState.EndOfTurn);
    }

    public void A_SkipTurn()
    {
        Debug.Log("---------------------------------");
        Debug.Log("Clicked Skip Turn, go to EndOfTurn");
        GameHandler.Instance.ChangeState(GameState.EndOfTurn);
    }

}