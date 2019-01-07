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

   
    public void Start()
    {
        booster1source.clip = booster1;

        booster2source.clip = booster2;

        booster3source.clip = booster3;

        portalEntersource.clip = portalEnter;

        moveLerpSource.clip = moveLerp;

        rollDiceSource.clip = rollDice;

        newDimensionSource.clip = newDimension;

        newBoosterSource.clip = newBooster;

        selectionSource.clip = selection;

        winScreenSource.clip = winScreen;
    }   
}

//add below before start
//public AudioManager audiomanager;
//add below to start
//audiomanager = FindObjectOfType<AudioManager>();
//link below at location
// audiomanager.soundSource.Play();
