using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridMetrics {

    //public const float nodeOuterRadius = 0f;

    //public const float nodeInnerRadius = nodeOuterRadius * 0.866025404f;

    //Enhetscirkeln, börjar från vänster, jämnt fördelat på 6 vinklar
    public static Vector3[] Dir
    {
        get
        {
            return new Vector3[] { new Vector3(1, -1, 0),
                                   new Vector3(0, -1, 1),
                                   new Vector3(-1, 0, 1),
                                   new Vector3(-1, 1, 0),
                                   new Vector3(0, 1, -1),
                                   new Vector3(1, 0, -1)
            };
        }
    }

}