using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeContents : MonoBehaviour {

    public bool occupied = false;
    public bool holdingTeleporter = false;
    public bool holdingBoosterPickUp = false;
    public StateManager stateManager;
    private int placeInList;
    //public List<Vector4> scoredDrones;
    //public GameObject scoredDroneStorage;

    private void Awake()
    {
        //scoredDrones = new Vector4[12];
    }

    public void OnDroneEnter () {

        if (holdingBoosterPickUp && occupied)
        {
            FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster-=2;

            Debug.Log("A booster has been picked up!");
            stateManager = FindObjectOfType<StateManager>();

            try
            {
                GameObject myBooster = GetComponentInChildren<WarpBoosterScript>().gameObject;
                Destroy(myBooster);
                holdingBoosterPickUp = false;

                StartCoroutine(FindObjectOfType<CameraShake>().Shake(.15f, .4f));
                
                stateManager.currentPlayer--;
            }
            catch (System.Exception)
            {
            }

            try
            {
                GameObject myBooster = GetComponentInChildren<ThrottleBoosterScript>().gameObject;
                Destroy(myBooster);
                holdingBoosterPickUp = false;

                StartCoroutine(FindObjectOfType<CameraShake>().Shake(.15f, .4f));

                Debug.Log("A Throttle has been picked up!");

                ThrottleBar[] allThrottleBars = FindObjectOfType<Dice>().GetComponentsInChildren<ThrottleBar>(true);

                foreach (var throttleBar in allThrottleBars)
                {
                    if (throttleBar.gameObject.tag == FindObjectOfType<StateManager>().currentPlayer.ToString())
                    {
                        throttleBar.GainThrottle();
                    }
                    else
                    {
                    }
                }
                

            }
            catch (System.Exception)
            {
            }

            //try
            //{
            //    GameObject myBooster = GetComponentInChildren<DupeBoosterScript>().gameObject;
            //    Destroy(myBooster);
            //    holdingBoosterPickUp = false;

            //    stateManager.currentPlayer--;


            //    var everyDrones = FindObjectsOfType<DroneLocation>();

            //    foreach (var drone in everyDrones)
            //    {
            //        drone.gameObject.layer = 11; // refenses to nonselctable layer
            //    }

            //    var allPortals = FindObjectsOfType<TeleporterScript>();

            //    foreach (var portal in allPortals)
            //    {
            //        portal.gameObject.layer = 10;
            //    }
            //}
            //catch (System.Exception)
            //{
            //    throw;
            //}

            //Det vi behöver göra här är att kolla vilken typ av booster som blivit upplockad.
            //tex genom att kolla (myBooster.name), eller döpa om "BoosterScript" till tre olika namn och ge de olika
            //Booster prefabsen varsitt, och sedan GetComponentInChildren<ETT UTAV BOOSTER-NAMNEN>()
            // sedan if myBooster is Booster a - do this
            // if myBooster is Booster b - do that
            // if myBooster is Booster c - do them other things


            var allDrones = FindObjectsOfType<DroneLocation>();

            foreach (var drone in allDrones)
            {
                if (drone.tag == stateManager.currentPlayer.ToString())
                {
                    drone.gameObject.layer = 10; // refenses to, selctable layer
                }
                else
                {
                    drone.gameObject.layer = 11; // refenses to nonselctable layer
                }
            }
        }

        if (holdingTeleporter && occupied)
        {
            FindObjectOfType<BoosterPickUpGenerator>().chanceToSpawnBooster-=2;
            
            Debug.Log("A teleporter has been entered!");
            stateManager = FindObjectOfType<StateManager>();
            int ownerOfDrone = (int)stateManager.currentPlayer;
            Vector3 spawnCoordinates = gameObject.GetComponent<GridNode>().Coordinates;

            Vector4 droneScored = new Vector4(ownerOfDrone, spawnCoordinates.x, spawnCoordinates.y, spawnCoordinates.z);

            ScoredDroneStorage scoredDroneStorage = FindObjectOfType<ScoredDroneStorage>();

            scoredDroneStorage.scoredDrones.Add(droneScored);

            if (stateManager.currentPlayer == Player.Blue)
            {
                stateManager.blueScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.blueScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Red)
            {
                stateManager.redScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.redScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Green)
            {
                stateManager.greenScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.greenScore + " points!");
            }
            else if (stateManager.currentPlayer == Player.Yellow)
            {
                stateManager.yellowScore++;
                Debug.Log(stateManager.currentPlayer + " now has " + stateManager.yellowScore + " points!");
            }
            GameObject myTeleporter = GetComponentInChildren<TeleporterScript>().gameObject;
            Destroy(myTeleporter);

            StartCoroutine(FindObjectOfType<CameraShake>().Shake(.2f, .5f));

            holdingTeleporter = false;
            occupied = false;

            GameObject myDrone = GetComponentInChildren<DroneLocation>().gameObject;
            myDrone.GetComponent<DroneLocation>().currentlyOccupiedWaypoint = null;
            myDrone.GetComponent<DroneLocation>().previouslyOccupiedWaypoint = null;
            Destroy(myDrone);

            //var allTeleporters = FindObjectsOfType<TeleporterScript>();

            //if (allTeleporters.Length <= 0) FindObjectOfType<VictoryScreenScript>().winnerName = stateManager.currentPlayer.ToString();
        }
	}
}
