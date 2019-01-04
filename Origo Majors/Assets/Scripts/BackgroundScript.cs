using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public GameObject background;
    public SpriteRenderer backgroundOverlay0;
    Vector3 turnSpeed = new Vector3 (0.0f, 0.0f, 1.0f);
    float thing;

    private void Update()
    {
        StarTransparency();
        IdleRotation();

    }

    private void StarTransparency()
    {
        thing = (Mathf.Sin(Time.frameCount * 0.01f) + 1f) / 2;
        backgroundOverlay0.color = new Color(255, 255, 255, thing);
    }

    private void IdleRotation()
    {
        transform.Rotate(turnSpeed * Time.deltaTime);
    }

}
