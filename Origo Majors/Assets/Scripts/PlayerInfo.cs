using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInfo
{

    public string name;
    public int id;
    public int numberOfBooster1;
    public int numberOfBooster2;
    public int numberOfBooster3;
    public int numberOfDrones;

    public PlayerInfo(string name, int id)
    {
        this.name = name;
        this.id = id;
        numberOfBooster1 = 0;
        numberOfBooster2 = 0;
        numberOfBooster3 = 0;
        numberOfDrones = 0;
    }

}