using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleBar : MonoBehaviour {

    public Transform firstThrottlePlace;
    public Transform secondThrottlePlace;
    public Transform thirdThrottlePlace;

    public GameObject ThrottleOnePrefab;
    public GameObject ThrottleTwoPrefab;
    public GameObject ThrottleThreePrefab;

    private Transform thePlaceToSpawnThrottle;
    private GameObject throttleToSpawn;

    public void Start()
    {
        GainThrottle();
        GainThrottle();
        GainThrottle();
    }

    public void GainThrottle()
    {
        //TODO fixa en funktion som flyttar upp efterkommande throttle-ikoner i throttlebaren när en throttle används
        FindPlace();
        ChooseRandomThrottle();
        //GameObject myThrottle = Instantiate(ThrottleOnePrefab, thirdThrottlePlace);
        if (thePlaceToSpawnThrottle != null)
        {
            GameObject myThrottle = Instantiate(throttleToSpawn, thePlaceToSpawnThrottle);
            myThrottle.transform.SetParent(thePlaceToSpawnThrottle);
        }
        Debug.Log(FindObjectOfType<StateManager>().currentPlayer.ToString() + " has picked up a Throttle!");
    }

    private void FindPlace()
    {
        if (firstThrottlePlace.childCount == 0)
        {
            thePlaceToSpawnThrottle = firstThrottlePlace;
        }
        else if (secondThrottlePlace.childCount == 0)
        {
            thePlaceToSpawnThrottle = secondThrottlePlace;
        }
        else if (thirdThrottlePlace.childCount == 0)
        {
            thePlaceToSpawnThrottle = thirdThrottlePlace;
        }
        else thePlaceToSpawnThrottle = null;
    }

    private void ChooseRandomThrottle()
    {
        int randomThrottle = UnityEngine.Random.Range(1,4);

        if (randomThrottle == 3)
        {
            throttleToSpawn = ThrottleThreePrefab;
        }
        else if (randomThrottle == 2)
        {
            throttleToSpawn = ThrottleTwoPrefab;
        }
        else
        {
            throttleToSpawn = ThrottleOnePrefab;
        }
    }
}
