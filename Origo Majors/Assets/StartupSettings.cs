using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupSettings : MonoBehaviour
{
    public Dropdown playernumber;
    public Dropdown dronenumber;
    [HideInInspector]
    public int numberOfSelectedplayers;
    [HideInInspector]
    public int numberOfSelectedDrones;

    public int player1DroneSelected = 0;
    public int player2DroneSelected = 1;
    public int player3DroneSelected = 2;
    public int player4DroneSelected = 3;

    // keep this between scenes
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

    }
    public void Update()
    {
        // todo 
        // when number if player selcted equals int
        // reveal custumise for number of selcted players
    }

    // Update is called once per frame
    public void SetGameSettings()
    {
        //Debug.Log("Number of players" + numberOfSelectedplayers);
        numberOfSelectedplayers = playernumber.value + 1;

        //Debug.Log("Number of drones" + numberOfSelectedDrones);

        numberOfSelectedDrones = dronenumber.value + 2;

    }


    // Player 1 drone selection
    public void Player1Drone00  ()
    {
        player1DroneSelected = 0;
    }
    public void Player1Drone01()
    {
        player1DroneSelected = 1;
    }
    public void Player1Drone02()
    {
        player1DroneSelected = 2;
    }
    public void Player1Drone03()
    {
        player1DroneSelected = 3;
    }

    // Player 2 drone selection
    public void Player2Drone00()
    {
        player1DroneSelected = 0;
    }
    public void Player2Drone01()
    {
        player2DroneSelected = 1;
    }
    public void Player2Drone02()
    {
        player2DroneSelected = 2;
    }
    public void Player2Drone03()
    {
        player2DroneSelected = 3;
    }
    
    // Player 3 drone selection
    public void Player3Drone00()
    {
        player3DroneSelected = 0;
    }
    public void Player3Drone01()
    {
        player3DroneSelected = 1;
    }
    public void Player3Drone02()
    {
        player3DroneSelected = 2;
    }
    public void Player3Drone03()
    {
        player3DroneSelected = 3;
    }


    // Player 4 drone selection
    public void Player4Drone00()
    {
        player4DroneSelected = 0;
    }
    public void Player4Drone01()
    {
        player4DroneSelected = 1;
    }
    public void Player4Drone02()
    {
        player4DroneSelected = 2;
    }
    public void Player4Drone03()
    {
        player4DroneSelected = 3;
    }


}


