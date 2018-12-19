using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupSettings : MonoBehaviour
{
    public Dropdown playernumber;
    public Dropdown dronenumber;
    public int numberOfSelectedplayers;
    public int numberOfSelectedDrones;
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
        //Debug.Log("Number of players" + numberOfSelectedplayers);
        numberOfSelectedplayers = playernumber.value + 2;

        Debug.Log("Number of drones" + numberOfSelectedDrones);

        numberOfSelectedDrones = dronenumber.value + 4;

    }

    public void PlayerCount ()
    {
          
    }

    
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