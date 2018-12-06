using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour {

    Material m_Material;

    //pls dont manipulate
    public Vector3 Coordinates = Vector3.zero;

    internal bool isActive = false;
    
    public void Paint ()
    {
        m_Material = GetComponent<MeshRenderer>().material;
        m_Material.color = Color.red;
    }
}
