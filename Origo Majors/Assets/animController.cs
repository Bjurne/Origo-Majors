using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour {

    public StateManager stateManager;
    public Animator anim;
    public Player currentPlayer;
    public DronePlacement dronePlacement;

    void Start () {
        anim = GetComponent<Animator>();
	}

	void Update () {
        if (stateManager.currentPlayer == Player.Blue)
        {
            if (dronePlacement.numberOfDronesSpawned > 0)
            {
                anim.Play("notyellowturn");
            }
            anim.Play("blueTurn");
           
        }
       
         if (stateManager.currentPlayer == Player.Red)
        {
            anim.Play("notblueturn");
            anim.Play("redTurn");
        }
         if (stateManager.currentPlayer == Player.Green)
        {
            anim.Play("notredturn");
            anim.Play("greenTurn");
        }
         if (stateManager.currentPlayer == Player.Yellow)
        {
            anim.Play("notgreenturn");
            anim.Play("yellowTurn");
        }
    }
}
