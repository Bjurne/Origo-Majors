using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioClip booster1;
    public AudioSource booster1source;

    public AudioClip booster2;
    public AudioSource booster2source;

    public AudioClip booster3;
    public AudioSource booster3source;

    public AudioClip throttleActivated;
    public AudioSource throttleActivatedSource;

    public AudioClip portalEnter;
    public AudioSource portalEntersource;

    public AudioClip moveLerp;
    public AudioSource moveLerpSource;

    public AudioClip rollDice;
    public AudioSource rollDiceSource;

    public AudioClip newDimension;
    public AudioSource newDimensionSource;

    public AudioClip newBooster;
    public AudioSource newBoosterSource;

    public AudioClip selection;
    public AudioSource selectionSource;

    public AudioClip winScreen;
    public AudioSource winScreenSource;

    public AudioClip respawnDrone;
    public AudioSource droneRespawnSource;

    public AudioClip failed;
    public AudioSource failedSource;

    public AudioClip throttleBoosterPickUp;
    public AudioSource throttleBoosterPickUpSource;

    public AudioClip quantumLeapBoosterPickUp;
    public AudioSource quantumLeapBoosterPickUpSource;


    public void Start()
    {
        booster1source.clip = booster1;

        booster2source.clip = booster2;

        booster3source.clip = booster3;

        throttleActivatedSource.clip = throttleActivated;

        portalEntersource.clip = portalEnter;

        moveLerpSource.clip = moveLerp;

        rollDiceSource.clip = rollDice;

        newDimensionSource.clip = newDimension;

        newBoosterSource.clip = newBooster;

        selectionSource.clip = selection;

        winScreenSource.clip = winScreen;

        droneRespawnSource.clip = respawnDrone;

        failedSource.clip = failed;

        throttleBoosterPickUpSource.clip = throttleBoosterPickUp;

        quantumLeapBoosterPickUpSource.clip = quantumLeapBoosterPickUp;

    }
}

//add below before start
//public AudioManager audiomanager;
//add below to start
//audiomanager = FindObjectOfType<AudioManager>();
//link below at location
// audiomanager.soundSource.Play();
