using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Font Origotext;
    public BackgroundScript backgroundScript;

    private void Start()
    {

        var textComponents = Component.FindObjectsOfType<Text>();
        foreach (var component in textComponents)
            component.font = Origotext ;

        var textcolorComponents = Component.FindObjectsOfType<Text>();
        foreach (var component in textcolorComponents)
            component.color = Color.white;

        backgroundScript.ChangeNebula(backgroundScript.nebulas[Random.Range(0, 4)]);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }
}
