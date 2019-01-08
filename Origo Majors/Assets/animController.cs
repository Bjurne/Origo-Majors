using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class animController : MonoBehaviour {

    public StateManager stateManager;
    public Player currentPlayer;

    private Animator lastActiveAnimator;
    public Animator blueAnimator;
    public Animator redAnimator;
    public Animator greenAnimator;
    public Animator yellowAnimator;

    public GameObject blueAvatarImage;
    public GameObject redAvatarImage;
    public GameObject greenAvatarImage;
    public GameObject yellowAvatarImage;

    void Start () {
        redAnimator.Play("notredturn");
        lastActiveAnimator = redAnimator;
        StartCoroutine(FadeBackgroundAlpha());
        greenAnimator.Play("notgreenturn");
        lastActiveAnimator = greenAnimator;
        StartCoroutine(FadeBackgroundAlpha());
        yellowAnimator.Play("notyellowturn");
        lastActiveAnimator = yellowAnimator;
        StartCoroutine(FadeBackgroundAlpha());
        blueAnimator.Play("notblueturn");
        lastActiveAnimator = blueAnimator;
        StartCoroutine(FadeBackgroundAlpha());

        //blueAvatarImage.GetComponent<Image>().sprite = FindObjectOfType<StartupSettings>().blueSelectedAvatar;
        //redAvatarImage.GetComponent<Image>().sprite = FindObjectOfType<StartupSettings>().redSelectedAvatar;
        //greenAvatarImage.GetComponent<Image>().sprite = FindObjectOfType<StartupSettings>().greenSelectedAvatar;
        //yellowAvatarImage.GetComponent<Image>().sprite = FindObjectOfType<StartupSettings>().yellowSelectedAvatar;

        SetUISize();
	}

	public void SetUISize () {

        Debug.Log("Kör UI-animationer");
        
        if (lastActiveAnimator == blueAnimator) blueAnimator.Play("notblueturn");
        else if (lastActiveAnimator == redAnimator) redAnimator.Play("notredturn");
        else if (lastActiveAnimator == greenAnimator) greenAnimator.Play("notgreenturn");
        else if (lastActiveAnimator == yellowAnimator) yellowAnimator.Play("notyellowturn");

        StartCoroutine(FadeBackgroundAlpha());
        

        if (stateManager.currentPlayer == Player.Blue)
        {
            blueAnimator.Play("blueTurn");
            lastActiveAnimator = blueAnimator;
            StartCoroutine(ResetBackgroundAlpha());
        }
        else if (stateManager.currentPlayer == Player.Red)
        {
            redAnimator.Play("redTurn");
            lastActiveAnimator = redAnimator;
            StartCoroutine(ResetBackgroundAlpha());
        }
        else if (stateManager.currentPlayer == Player.Green)
        {
            greenAnimator.Play("greenTurn");
            lastActiveAnimator = greenAnimator;
            StartCoroutine(ResetBackgroundAlpha());
        }
        else if (stateManager.currentPlayer == Player.Yellow)
        {
            yellowAnimator.Play("yellowTurn");
            lastActiveAnimator = yellowAnimator;
            StartCoroutine(ResetBackgroundAlpha());
        }


        //if (stateManager.currentPlayer == Player.Blue)
        //{

        //    yellowAnimator.Play("notyellowturn");
        //    blueAnimator.Play("blueTurn");
        //}
       
        // if (stateManager.currentPlayer == Player.Red)
        //{
        //    blueAnimator.Play("notblueturn");
        //    redAnimator.Play("redTurn");
        //}
        // if (stateManager.currentPlayer == Player.Green)
        //{
        //    redAnimator.Play("notredturn");
        //    greenAnimator.Play("greenTurn");
        //}
        // if (stateManager.currentPlayer == Player.Yellow)
        //{
        //    greenAnimator.Play("notgreenturn");
        //    yellowAnimator.Play("yellowTurn");
        //}
    }

    public IEnumerator FadeBackgroundAlpha()
    {
        lastActiveAnimator.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0.5f, false);
        lastActiveAnimator.GetComponentInChildren<Image>().CrossFadeAlpha(0, 0.5f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator ResetBackgroundAlpha()
    {
        if (stateManager.currentPlayer == Player.Blue)
        {
            blueAnimator.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, false);
            blueAnimator.GetComponentInChildren<Image>().CrossFadeAlpha(1, 0.5f, false);
        }
        else if (stateManager.currentPlayer == Player.Red)
        {
            redAnimator.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, false);
            redAnimator.GetComponentInChildren<Image>().CrossFadeAlpha(1, 0.5f, false);
        }
        else if (stateManager.currentPlayer == Player.Green)
        {
            greenAnimator.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, false);
            greenAnimator.GetComponentInChildren<Image>().CrossFadeAlpha(1, 0.5f, false);
        }
        else if (stateManager.currentPlayer == Player.Yellow)
        {
            yellowAnimator.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, false);
            yellowAnimator.GetComponentInChildren<Image>().CrossFadeAlpha(1, 0.5f, false);
        }
        yield return new WaitForSeconds(1);
    }
}
