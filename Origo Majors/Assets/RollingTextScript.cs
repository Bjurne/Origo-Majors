using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingTextScript : MonoBehaviour {

    public Text myText;
    public GameObject rollingTextPrefab;

	void Start ()
    {
        StartCoroutine(RollText("Hello World!"));
    }

    private IEnumerator RollText(string textToRoll)
    {
        myText.text = textToRoll;

        float randomEuler = Random.Range(-0.2f, 0.2f);
        myText.fontSize = 72;
        myText.CrossFadeAlpha(0, 1.5f, false);
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.02f);
            myText.fontSize = (int)(myText.fontSize * 1.02);
            myText.transform.Rotate(randomEuler, randomEuler, randomEuler);
        }
        yield return new WaitForSeconds(2);
        Destroy(GetComponentInParent<RollingTextScript>().gameObject);
    }

    public void SpawnText()
    {
        Instantiate(rollingTextPrefab);
    }
}
