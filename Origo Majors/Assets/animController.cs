using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour {

    public StateManager stateManager;
    public Animator anim;
    public Player currentPlayer;


    void Start () {
        anim = GetComponent<Animator>();
	}

	void Update () {
        if (currentPlayer == Player.Blue)
        {
            anim.Play("blueTurn");
        }
        else if (currentPlayer == Player.Red)
        {
            anim.Play("redTurn");
        }
        else if (currentPlayer == Player.Green)
        {
            anim.Play("greenTurn");
        }
        else if (currentPlayer == Player.Yellow)
        {
            anim.Play("yellowTurn");
        }
    }
}
