using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour {

    public StateManager stateManager;
    public Player currentPlayer;

    public Animator lastActiveAnimator;
    public Animator blueAnimator;
    public Animator redAnimator;
    public Animator greenAnimator;
    public Animator yellowAnimator;

    void Start () {
        blueAnimator.Play("notblueturn");
        redAnimator.Play("notredturn");
        greenAnimator.Play("notgreenturn");
        yellowAnimator.Play("notyellowturn");

        lastActiveAnimator = blueAnimator;
	}

	public void SetUISize () {

        Debug.Log("Kör UI-animationer");
        
        if (lastActiveAnimator == blueAnimator) blueAnimator.Play("notblueturn");
        else if (lastActiveAnimator == redAnimator) redAnimator.Play("notredturn");
        else if (lastActiveAnimator == greenAnimator) greenAnimator.Play("notgreenturn");
        else if (lastActiveAnimator == yellowAnimator) yellowAnimator.Play("notyellowturn");

        if (stateManager.currentPlayer == Player.Blue)
        {
            blueAnimator.Play("blueTurn");
            lastActiveAnimator = blueAnimator;
        }
        else if (stateManager.currentPlayer == Player.Red)
        {
            redAnimator.Play("redTurn");
            lastActiveAnimator = redAnimator;
        }
        else if (stateManager.currentPlayer == Player.Green)
        {
            greenAnimator.Play("greenTurn");
            lastActiveAnimator = greenAnimator;
        }
        else if (stateManager.currentPlayer == Player.Yellow)
        {
            yellowAnimator.Play("yellowTurn");
            lastActiveAnimator = yellowAnimator;
        }


        //if (stateManager.currentPlayer == Player.Blue)
        //{

        //    yellowAnimator.Play("notyellowturn");
        //    blueAnimator.Play("blueTurn");
        //}
       
        // if (stateManager.currentPlayer == Player.Red)
        //{
        //    blueAnimator.Play("notblueturn");
        //    redAnimator.Play("redTurn");
        //}
        // if (stateManager.currentPlayer == Player.Green)
        //{
        //    redAnimator.Play("notredturn");
        //    greenAnimator.Play("greenTurn");
        //}
        // if (stateManager.currentPlayer == Player.Yellow)
        //{
        //    greenAnimator.Play("notgreenturn");
        //    yellowAnimator.Play("yellowTurn");
        //}
    }
}
