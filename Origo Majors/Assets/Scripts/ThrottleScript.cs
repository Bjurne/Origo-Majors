using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleScript : MonoBehaviour {

    public int ThrottleIdentifier;

	public void FindDiceAndRunThrottle()
    {
        FindObjectOfType<Dice>().ThrottleChanges(ThrottleIdentifier);
    }

    public void DestroyOnActivation()
    {
        Destroy(this.gameObject);
    }
}
