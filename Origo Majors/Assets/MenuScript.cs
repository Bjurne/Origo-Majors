using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    private void Start()
    {
        //var textComponents = Component.FindObjectsOfType<Text>();
        //foreach (var component in textComponents)
        //    component.font = < choosefont >;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Svedlund Test Scene");
    }
}
