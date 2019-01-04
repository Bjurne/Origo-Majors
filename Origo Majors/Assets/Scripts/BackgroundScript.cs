using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public SpriteRenderer layer0;
    public SpriteRenderer layer1;
    public SpriteRenderer layer2;
    Vector3 turnSpeed = new Vector3 (0.0f, 0.0f, 1.0f);

    private void Update()
    {
        IdleRotation(layer0, 1.0f);
        IdleRotation(layer1, 1.1f);
        IdleRotation(layer2, 1.2f);

        StarTransparency(layer1, 0);
        StarTransparency(layer2, 360);
    }

    private void IdleRotation(SpriteRenderer layer, float rotationOffset)
    {
        layer.transform.Rotate((turnSpeed * rotationOffset) * Time.deltaTime);
    }

    private void StarTransparency(SpriteRenderer layer, int offset)
    {
        float thing = (Mathf.Sin((Time.frameCount + offset) * 0.01f) + 1f) / 2;
        layer.color = new Color(255, 255, 255, thing);
    }

}
