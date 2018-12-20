using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroup : MonoBehaviour
{
    public GameObject tempDrone;
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

        StartCoroutine(MoveDrone(tempDrone, GameHandler.Instance.GetRandomNode().transform.position));

        //rng = Random.Range(0, 2);
        //if (rng == 1)
        //{
        //    Debug.Log("Found and entered a portal");
        //    GameHandler.Instance.ReducePortalCount();
        //}
        //else
        //{
        //    Debug.Log("Found no portal, availablePortals stays the same");
        //}
        //GameHandler.Instance.CheckPortalsAfterMove();
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

    private IEnumerator MoveDrone(GameObject SelectedThing, Vector3 moveToPos)
    {
        bool notDoneMoving = true;
        float stepCounter = 0;
        Vector3 originalPos;
        originalPos = SelectedThing.transform.position;

        while (notDoneMoving)
        {
            if(SelectedThing.transform.position == moveToPos)
            {
                notDoneMoving = false;
            }
            SelectedThing.transform.position = Vector3.Lerp(originalPos, moveToPos, stepCounter);
            stepCounter += 0.05f;
            yield return null;
        }
        Debug.Log("Movement complete");
        GameHandler.Instance.CheckPortalsAfterMove();
    }

}