using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour {

	public void LoadMenu()
    {
        Destroy(FindObjectOfType<StartupSettings>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
