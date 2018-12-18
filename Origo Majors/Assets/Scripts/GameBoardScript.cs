using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardScript : MonoBehaviour {

    public GridGenerator gridGenerator;

    public void MoveBoardToCenter()
    {
        GridNode testNode;
        if (gridGenerator.dic.TryGetValue(gridGenerator.FindCenter(), out testNode))
        {
            transform.position = testNode.gameObject.transform.position;
        }
        else
        {
            Debug.Log("BoardScript can't find center node!");
        }
    }
}
