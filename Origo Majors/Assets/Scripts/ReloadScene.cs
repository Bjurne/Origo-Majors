using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    public GameObject victoryScreenCanvas;

    public void reloadScene()
    {
        victoryScreenCanvas.SetActive(false);
        SceneManager.LoadScene("MainGame");
    }
}
