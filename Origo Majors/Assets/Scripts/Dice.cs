using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{

    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private int diceValue;
    [HideInInspector]
    public int moveRange;
    public Text currentPlayerTurn;
    public Sprite[] NumberSprite;
   




    private void Update()
    {
        if (stateManager.initialPlacementIsDone == false)
        {
            currentPlayerTurn.text = stateManager.currentPlayer.ToString() + "'s Turn to place Drone";

        }
        else
        {
            if (stateManager.isDoneRolling == false)
            {
                currentPlayerTurn.text = stateManager.currentPlayer.ToString() + "'s Turn to roll";
            }
            else
            {
                currentPlayerTurn.text = stateManager.currentPlayer.ToString() + "'s Turn to move";
            }

        }

    }

    public void ThrottleChanges(int changes)
    {
        if (changes == 3) moveRange += 1;
        else moveRange -= changes;
        if (moveRange < 1) moveRange = 1;
        if (moveRange > 4) moveRange = 4;
        SetDiceDisplaySprite();
    }

    public void Number()
    {
        diceValue = Random.Range(1, 8);
        if (diceValue > 7) Debug.Log("Tärningsfel");

        SetMoveRange();
        SetDiceDisplaySprite();
        
        stateManager.isDoneRolling = true;
    }

    private void SetMoveRange()
    {
        if (diceValue == 1)
        {
            moveRange = 1;
        }

        if (diceValue == 2 || diceValue == 5)
        {
            moveRange = 2;
        }

        if (diceValue == 3 || diceValue == 6 || diceValue == 7)
        {
            moveRange = 3;
        }

        if (diceValue == 4)
        {
            moveRange = 4;
        }
    }

    public void SetDiceDisplaySprite()
    {
        if (moveRange == 1)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[0];
        }

        if (moveRange == 2)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[1];
        }

        if (moveRange == 3)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[2];
        }

        if (moveRange == 4)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[3];
        }

        if (FindObjectOfType<CalculateLegalWarpDestination>().thisIsAQuantumLeap)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[4];
        }
    }

    public void setRollButtonColor()
    {
        var currentPlayer = stateManager.currentPlayer;
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
        stateManager.rollButton.GetComponent<Image>().color = rollButtonColor;
    }



    //public void NumberDisplay()
    //{
    //    if (moveRange == 1)
    //    {
    //        this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[0];
    //    }
    //    if (moveRange==2)
    //    {
    //        this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[1];
    //    }
    //    if (moveRange== 3)
    //    {
    //        this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[2];

    //    }
    //    if (moveRange==4)
    //    {
    //        this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[3];

    //    }
    //}
}