using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupSettings : MonoBehaviour
{
    public Dropdown playernumber;
    public Dropdown dronenumber;

    // keep this between scenes
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playernumber.value);
    }

    // debug log shows current selected in drop down first being 0
    // TODO: add a function that sets NUMBEROFPLAYERS to the dropdown value
    // or if function to se if
}



/*
public Dropdown myDropdown;

void Start()
{
    myDropdown.onValueChanged.AddListener(delegate {
        myDropdownValueChangedHandler(myDropdown);
    });
}
void Destroy()
{
    myDropdown.onValueChanged.RemoveAllListeners();
}

private void myDropdownValueChangedHandler(Dropdown target)
{
    Debug.Log("selected: " + target.value);
}

public void SetDropdownIndex(int index)
{
    myDropdown.value = index;
}
*/