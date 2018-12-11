﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VictoryScreenScript : MonoBehaviour {

    public StateManager stateManager;
    public Text firstPlaceText;
    public Text secondPlaceText;
    public Text thirdPlaceText;
    public Text fourthPlaceText;
    public string winnerName;

    public void DisplayVictoryScreen ()
    {
        int blueScore = stateManager.blueScore;
        int redScore = stateManager.redScore;
        int greenScore = stateManager.greenScore;
        int yellowScore = stateManager.yellowScore;

        int[] scores = { blueScore, redScore, greenScore, yellowScore};

        int highestScore = scores.Max();

        if (highestScore == blueScore)
        {
            winnerName = ("Blue Player");
        }
        else if (highestScore == redScore)
        {
            winnerName = ("Red Player");
        }
        else if (highestScore == greenScore)
        {
            winnerName = ("Green Player");
        }
        else if (highestScore == yellowScore)
        {
            winnerName = ("Yellow Player");
        }

        firstPlaceText.text = (winnerName + " wins with " + highestScore.ToString() + " points!");
    }
	
}
