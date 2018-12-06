using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // flytta in text (textrutan) in i Text Number (script) för att referera till text rutan 
    // som tillhör UI:n
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private int diceValue;
    [HideInInspector]
    public int moveRange;
    //  public Text textNumber;
    public Sprite[] NumberSprite;

    public void Number()
    {
        diceValue = Random.Range(1, 7);

        if (diceValue == 1)
        {
            moveRange = 1;
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[0];
        }

        if (diceValue == 2 || diceValue == 5)
        {
            moveRange = 2;
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[1];
        }

        if (diceValue == 3 || diceValue == 6 || diceValue == 7)
        {
            moveRange = 3;
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[2];
        }

        if (diceValue == 4)
        {
            moveRange = 4;
            this.transform.GetChild(1).GetComponent<Image>().sprite = NumberSprite[3];
        }
    
        stateManager.isDoneRolling = true;
    }


}