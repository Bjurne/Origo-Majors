using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{



    public Dropdown playernumber;
    public int numberOfSelectedplayers;

    public GameObject player1;
    public GameObject player3;
    public GameObject player4;

    // buttins for dronebutton reference too keep them highlighted
    public Button player1dronebutton00;
    public Button player1dronebutton01;
    public Button player1dronebutton02;
    public Button player1dronebutton03;

    public Button player2dronebutton00;
    public Button player2dronebutton01;
    public Button player2dronebutton02;
    public Button player2dronebutton03;

    public Button player3dronebutton00;
    public Button player3dronebutton01;
    public Button player3dronebutton02;
    public Button player3dronebutton03;

    public Button player4dronebutton00;
    public Button player4dronebutton01;
    public Button player4dronebutton02;
    public Button player4dronebutton03;

    // button referedcnces for avatars to keep highlighted when activated
    public Button player1avatarbutton00;
    public Button player1avatarbutton01;
    public Button player1avatarbutton02;
    public Button player1avatarbutton03;

    public Button player2avatarbutton00;
    public Button player2avatarbutton01;
    public Button player2avatarbutton02;
    public Button player2avatarbutton03;

    public Button player3avatarbutton00;
    public Button player3avatarbutton01;
    public Button player3avatarbutton02;
    public Button player3avatarbutton03;

    public Button player4avatarbutton00;
    public Button player4avatarbutton01;
    public Button player4avatarbutton02;
    public Button player4avatarbutton03;

    // shows which drone is selected in an int in this script
    public int player1DroneSelection;
    public int player2DroneSelection;
    public int player3DroneSelection;
    public int player4DroneSelection;

    // same as above but for avatars
    public int player1AvatarSelection;
    public int player2AvatarSelection;
    public int player3AvatarSelection;
    public int player4AvatarSelection;



    public StartupSettings startupsettings;

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

        // check number od selected players 
        // show players 3-4 if their number is selected and if player one is setactive
        numberOfSelectedplayers = playernumber.value + 2;

        if (numberOfSelectedplayers > 2 && player1.activeInHierarchy == true)
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


    // keep buttons selection always selected, even setactive false/true
    public void Player1Dronepressed()
    {
        Debug.Log("what");
        player1DroneSelection = startupsettings.player1DroneSelected;


        if (player1DroneSelection == 0)
        {
            player1dronebutton00.Select();
        }
        else if (player1DroneSelection == 1)
        {
            player1dronebutton01.Select();
        }
        else if (player1DroneSelection == 2)
        {
            player1dronebutton02.Select();
        }
        else if (player1DroneSelection == 3)
        {
            player1dronebutton03.Select();
        }
    }


        public void Player2Dronepressed()
    {
        player2DroneSelection = startupsettings.player2DroneSelected;

        if (player2DroneSelection == 0)
        {
            player2dronebutton00.Select();
        }
        else if (player2DroneSelection == 1)
        {
            player2dronebutton01.Select();
        }
        else if (player2DroneSelection == 2)
        {
            player2dronebutton02.Select();
        }
        else if (player2DroneSelection == 3)
        {
            player2dronebutton03.Select();
        }
    }


    public void Player3Dronepressed()
    {
        player3DroneSelection = startupsettings.player3DroneSelected;

        if (player3DroneSelection == 0)
        {
            player3dronebutton00.Select();
        }
        else if (player3DroneSelection == 1)
        {
            player3dronebutton01.Select();
        }
        else if (player3DroneSelection == 2)
        {
            player3dronebutton02.Select();
        }
        else if (player3DroneSelection == 3)
        {
            player3dronebutton03.Select();
        }
    }

    public void Player4Dronepressed()
    {
        player4DroneSelection = startupsettings.player4DroneSelected;

        if (player4DroneSelection == 0)
        {
            player4dronebutton00.Select();
        }
        else if (player4DroneSelection == 1)
        {
            player4dronebutton01.Select();
        }
        else if (player4DroneSelection == 2)
        {
            player4dronebutton02.Select();
        }
        else if (player4DroneSelection == 3)
        {
            player4dronebutton03.Select();
        }
    }




    ////same as above but avatars
    //public void Player1Avatarpressed ()
    //{

    //    player1AvatarSelection = startupsettings.player1AvatarSelected;

    //    if (player1AvatarSelection == 0)
    //    {
    //        player1avatarbutton00.Select();
    //    }
    //    else if (player1AvatarSelection == 1)
    //    {
    //        player1avatarbutton01.Select();
    //    }
    //    else if (player1AvatarSelection == 2)
    //    {
    //        player1avatarbutton02.Select();
    //    }
    //    else if (player1AvatarSelection == 3)
    //    {
    //        player1avatarbutton03.Select();
    //    }
    //}

    //public void Player2Avatarpressed()
    //{

    //    player1AvatarSelection = startupsettings.player1AvatarSelected;

    //    if (player1AvatarSelection == 0)
    //    {
    //        player1avatarbutton00.Select();
    //    }
    //    else if (player1AvatarSelection == 1)
    //    {
    //        player1avatarbutton01.Select();
    //    }
    //    else if (player1AvatarSelection == 2)
    //    {
    //        player1avatarbutton02.Select();
    //    }
    //    else if (player1AvatarSelection == 3)
    //    {
    //        player2avatarbutton03.Select();
    //    }
    //}

}



