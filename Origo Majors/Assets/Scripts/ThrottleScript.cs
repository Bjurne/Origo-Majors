using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleScript : MonoBehaviour {

    public int ThrottleIdentifier;
    //public GameObject throttleIcon;

    public void Start()
    {
        StartCoroutine(AnimateSize());
    }

    private IEnumerator AnimateSize()
    {
        Debug.Log("inne i coroutine");
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = 1;
        while (x < 2)
        {
            x += 0.1f;
            y += 0.1f;
            transform.localScale = new Vector3(x, y, z);
            Debug.Log(x);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void FindDiceAndRunThrottle()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (FindObjectOfType<Dice>().hasBeenThrottled == false && FindObjectOfType<CalculateLegalWarpDestination>().thisIsAQuantumLeap == false)
        {
            FindObjectOfType<Dice>().hasBeenThrottled = true;
            audioManager.throttleActivatedSource.Play();
            FindObjectOfType<Dice>().ThrottleChanges(ThrottleIdentifier);
            Destroy(this.gameObject);
        }
        else
        {
            audioManager.failedSource.Play();
        }
    }

    //public void DestroyOnActivation()
    //{
    //    if (FindObjectOfType<Dice>().hasBeenThrottled == false)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
