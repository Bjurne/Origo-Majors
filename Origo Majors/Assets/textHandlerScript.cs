using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class textHandlerScript : MonoBehaviour {

    public GameObject rollingTextPrefab;
    public StateManager stateManager;
    private String myText;
    public Vector3 textPosition;

	public void Print(string textToPrint)
    {
        SetText(textToPrint);
        FadeAllActiveRollingTexts();
        SetTextPosition();

        GameObject myRollingText = Instantiate(rollingTextPrefab, textPosition, Quaternion.identity);

        myRollingText.transform.SetParent(FindObjectOfType<textHandlerScript>().gameObject.transform);
        myRollingText.GetComponentInChildren<Text>().color = setTextColor();
        myRollingText.GetComponentInChildren<Text>().text = myText;
    }


    private void SetText(string textToPrint)
    {
        if (textToPrint == "WarpBooster")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + " has picked up a Warp Booster!";
            }
            else if (randomText == 2)
            {
                myText = "It's " + stateManager.currentPlayer + "'s turn to roll again!";
            }
            else
            {
                myText = stateManager.currentPlayer + " takes another turn!";
            }
        }

        if (textToPrint == "ThrottleBooster")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + " has picked up a Throttle Booster!";
            }
            else if (randomText == 2)
            {
                myText = stateManager.currentPlayer + " is gearing up!";
            }
            else
            {
                myText = "This will come in handy!";
            }
        }

        if (textToPrint == "QLBooster")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + " has picked up a Quantum Leap Booster!";
            }
            else if (randomText == 2)
            {
                myText = stateManager.currentPlayer + " can perform a Quantum Leap!";
            }
            else
            {
                myText = "Check this awesome move out!";
            }
        }

        if (textToPrint == "Portal")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + " has entered a Portal!";
            }
            else if (randomText == 2)
            {
                myText = stateManager.currentPlayer + " is going strong!";
            }
            else
            {
                myText = "Great job, " + stateManager.currentPlayer;
            }
        }

        if (textToPrint == "Turn")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + "'s turn!";
            }
            else if (randomText == 2)
            {
                myText = "It's " + stateManager.currentPlayer + "'s turn!";
            }
            else
            {
                myText = "Go " + stateManager.currentPlayer + "!";
            }
        }

        if (textToPrint == "PlacementTurn")
        {
            int randomText = Random.Range(1, 4);

            if (randomText == 1)
            {
                myText = stateManager.currentPlayer + "'s turn to place a drone!";
            }
            else if (randomText == 2)
            {
                myText = "It's " + stateManager.currentPlayer + "'s turn!";
            }
            else
            {
                myText = "Place a drone, " + stateManager.currentPlayer + "!";
            }
        }
    }

    private void FadeAllActiveRollingTexts()
    {
        var allRollingTexts = FindObjectsOfType<RollingTextScript>();

        foreach (var text in allRollingTexts)
        {
            text.GetComponentInChildren<Text>().CrossFadeAlpha(0, 0.5f, false);
        }
    }

    private void SetTextPosition()
    {
        textPosition.Set(Screen.width / 2, 0, Screen.height / 4);

        var allRollingTexts = FindObjectsOfType<RollingTextScript>();

        foreach (var text in allRollingTexts)
        {
            textPosition.y += 10;
        }
    }

    private Color setTextColor()
    {
        var currentPlayer = stateManager.currentPlayer;
        Vector4 textColor = Color.white;
        if (currentPlayer == Player.Blue)
        {
            textColor = Color.blue;
        }
        else if (currentPlayer == Player.Red)
        {
            textColor = Color.red;
        }
        else if (currentPlayer == Player.Green)
        {
            textColor = Color.green;
        }
        else if (currentPlayer == Player.Yellow)
        {
            textColor = Color.yellow;
        }
        return textColor;
    }
}
