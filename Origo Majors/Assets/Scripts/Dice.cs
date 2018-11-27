using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {
    
    
    // flytta in text (textrutan) in i Text Number (script) för att referera till text rutan 
    // som tillhör UI:n

    int rolledNumber, moveRange;
    public Text textNumber;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // space är placeholder button för aktivering tillvidare
        if (Input.GetKeyDown("space"))
        {
            Number();
            textNumber.text = moveRange.ToString();
        }


    }






    void Number()
    {
        rolledNumber = Random.Range(1, 7);

        if (rolledNumber == 1)
        {
            moveRange = 1;
        }

        if (rolledNumber == 2 || rolledNumber == 5)
        {
            moveRange = 2;
        }

        if (rolledNumber == 3 || rolledNumber == 6 || rolledNumber == 7)
        {
            moveRange = 3;
        }

        if (rolledNumber == 4)
        {
            moveRange = 4;
        }
    }

}
