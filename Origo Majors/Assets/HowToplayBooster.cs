using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToplayBooster : MonoBehaviour {
    public Animator animator;

    int animationInt = 0;
    public GameObject nextButton;
    public GameObject previousButton;

    public void Reset()
    {
        animationInt = 0;
        animator.SetInteger("Booster", animationInt);

    }

    public void Next ()
    {
        animationInt += 1;
        animator.SetInteger("Booster", animationInt) ;
        Debug.Log(animator.GetInteger("Booster"));
        Buttoninteracability();
    }

    public void Previous()
    {
        animationInt -= 1;
        animator.SetInteger("Booster", animationInt);
        Debug.Log(animator.GetInteger("Booster"));
        Buttoninteracability();
    }

    void Buttoninteracability ()
    {
        if (animationInt > 2)
        {
            nextButton.SetActive(false);

            //  nextButton.interactable = !nextButton.interactable;
        }

        if (animationInt < 1)
        {
            previousButton.SetActive(false);
            //previousButton.interactable = !previousButton.interactable;
        }
        
    }
}
