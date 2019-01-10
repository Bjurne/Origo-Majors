using System;
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

    public AudioClip victoryScreenWobble;
    public AudioSource victoryScreenWobbleSource;

    public AudioClip origoMajorsMainThemeLooping;
    public AudioSource origoMajorsMainThemeLoopingSource;

    public AudioClip victoryScreenMusicLooping;
    public AudioSource victoryScreenMusicLoopingSource;

    public float musicVolume = 0;


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

        victoryScreenWobbleSource.clip = victoryScreenWobble;

        origoMajorsMainThemeLoopingSource.clip = origoMajorsMainThemeLooping;

        victoryScreenMusicLoopingSource.clip = victoryScreenMusicLooping;


        StartCoroutine(FadeinMusic());
    }

    private IEnumerator FadeinMusic()
    {
        //musicVolume = origoMajorsMainThemeLoopingSource.volume;
        origoMajorsMainThemeLoopingSource.volume = musicVolume;
        origoMajorsMainThemeLoopingSource.Play();
        while (musicVolume <0.27)
        {
            musicVolume += 0.02f;
            origoMajorsMainThemeLoopingSource.volume = musicVolume;
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
}

//add below before start
//public AudioManager audiomanager;
//add below to start
//audiomanager = FindObjectOfType<AudioManager>();
//link below at location
// audiomanager.soundSource.Play();
