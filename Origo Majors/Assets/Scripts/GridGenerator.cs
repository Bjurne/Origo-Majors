using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

    //För ett liggande hex-grid
    public int boardSize = 5; //längden på en sida i antal noder
    int boardMaxDistance, widthMin, widthMax, heightMin, heightMax;

    public GridNode gridNodePrefab;

    GridNode[] nodes;

    private void Awake ()
    {
        UpdateBoardSizeVariables(boardSize);
        Debug.Log(widthMin);
        nodes = new GridNode[60]; //tillfällig tvingad storlek för ett size 5-grid
    }

    private void Start ()
    {
        GenerateGameBoard();
    }
	
    public void UpdateBoardSizeVariables(int boardSize)
    {
        boardMaxDistance = (boardSize * 2) - 1;
        widthMin = boardSize;
        widthMax = boardMaxDistance;
        heightMin = 1;
        heightMax = boardMaxDistance;
    }

    public void GenerateGameBoard ()
    {
        for (int z = 0; 0 < boardMaxDistance; z++)
        {
            for (int i = 0, x = 0; i < boardMaxDistance; i++)
            {
                if (i < boardSize)
                {
                    CreateNode(x, z, i);
                    x++;
                }
                else if (i >= boardSize)
                {
                    CreateNode(x, z, i);
                    x--;
                }
                Debug.Log("HELP");
            }
        }
    }

    void CreateNode (int x, int z, int i)
    {
        Vector3 position;
        position.x = x;
        position.y = 0f;
        position.z = z;

        GridNode node = nodes[i] = Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;

    }

}
