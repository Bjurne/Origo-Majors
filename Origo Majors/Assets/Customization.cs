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

    bool player3check;
    bool player4check;
    bool player1check;

    // Use this for initialization
    void Start () {
        player3check = false;
        player4check = false;
        //player3.SetActive(player3check);
        //player4.SetActive(player4check );
        //player1.SetActive(player1check);
       
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
        //else
        //{
        //    player3.SetActive(false);
        //}

        //if (numberOfSelectedplayers == 4 && player1.activeInHierarchy)
        //{
        //    player4.SetActive(true);
        //}
        //else
        //{
        //    player4.SetActive(false);
        //}

    }
}


//numberOfSelectedplayers = playernumber.value + 1;


