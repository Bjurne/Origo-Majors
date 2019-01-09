using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour {


    public Dropdown playernumber;
    public int numberOfSelectedplayers;

    public GameObject player1;
    public GameObject player3;
    public GameObject player4;

  



    // Use this for initialization
    void Start () {

       
    }
    
	
	// Update is called once per frame
	void Update () {
        // check number od selected players 
        // show players 3-4 if their number is selected and if player one is setactive
        numberOfSelectedplayers = playernumber.value + 2;

        if (numberOfSelectedplayers  > 2  && player1.activeInHierarchy == true)
        {
            player3.SetActive(true);
        }
        else
        {
            player3.SetActive(false);
        }
        if (numberOfSelectedplayers == 4 && player1.activeInHierarchy == true)
        {
            player4.SetActive(true);
        }
        else
        {
            player4.SetActive(false);
        }

    }


    public Button player1dronebutton00;
    public Button player1dronebutton01;
    public Button player1dronebutton02;
    public Button player1dronebutton03;


    public int player1DroneSelected = 0;
    public int player2DroneSelected = 1;
    public int player3DroneSelected = 2;
    public int player4DroneSelected = 3;


    public void player1buttonpressed()
    {
        // todo 

        if (player1DroneSelected == 0)
        {
            player1dronebutton00.Select();
        }
        else if (player1DroneSelected == 1)
        {
            player1dronebutton01.Select();
        }
        else if (player1DroneSelected == 2)
        {
            player1dronebutton02.Select();
        }
        else if (player1DroneSelected == 3)
        {
            player1dronebutton03.Select();
        }


        // this works sorta now, need to be remvoed from update and be run at button click.
    }
}


//numberOfSelectedplayers = playernumber.value + 1;


