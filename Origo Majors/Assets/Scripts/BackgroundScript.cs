using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public SpriteRenderer nebula0;
    public SpriteRenderer nebula1;
    public SpriteRenderer nebula2;
    public SpriteRenderer nebula3;
    public SpriteRenderer layer0;
    public SpriteRenderer layer1;
    public SpriteRenderer layer2;
    public SpriteRenderer layer3;
    Vector3 turnSpeed = new Vector3 (0.0f, 0.0f, 1.0f);
    SpriteRenderer currentNebula;
    SpriteRenderer[] nebulas;


    private void Awake()
    {
        SpriteRenderer[] nebulas = { nebula0, nebula1, nebula2, nebula3 };
        ChangeNebula(nebula0);
    }

    private void Update()
    {

        IdleRotation(currentNebula, 0.5f);

        IdleRotation(layer0, 0.6f);
        IdleRotation(layer1, 0.6f);
        IdleRotation(layer2, 0.7f);
        IdleRotation(layer3, 0.7f);

        //StarTransparency(layer1, 0);
        //StarTransparency(layer2, 360);
    }

    public void ChangeNebula(SpriteRenderer nebula)
    {
        for (int i = 0; i < nebulas.Length; i++)
        {
            nebulas[i].gameObject.SetActive(false);
        }
        nebula.gameObject.SetActive(true);
        currentNebula = nebula;
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
