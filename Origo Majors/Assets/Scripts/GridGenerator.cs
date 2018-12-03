using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

    //För ett liggande hex-grid
    public int boardSize = 5;

    public float nodeOuterRadius = 2f;
    float nodeInnerRadius;
    //float nodeInnerRadius = nodeOuterRadius * 0.866025405f;

    int boardMaxDistance, widthMin, heightMax;
    float offset;

    public GridNode gridNodePrefab;

    GridNode[] nodes;

    public void Awake ()
    {
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location
        UpdateBoardSizeVariables(boardSize);
        nodes = new GridNode[NodeCounter()];
        Debug.Log(NodeCounter());
    }

    private void Start ()
    {
        GenerateGameBoard();
    }
	
    public void UpdateBoardSizeVariables(int boardSize)
    {
        boardMaxDistance = (boardSize * 2) - 1;
        widthMin = boardSize;
    }

    private int NodeCounter()
    {
        int counter = 0;
        for (int z = 0, i = 0, n = widthMin; z < boardMaxDistance; z++)
        {
            for (int x = 0; x < n; x++, i++)
            {
                counter++;
            }

            if (z < boardSize - 1)
            {
                n++;
            }
            else if (z >= boardSize - 1)
            {
                n--;
            }
        }
        return counter;
    }

    public void GenerateGameBoard()
    {
        for (int z = 0, i = 0, n = widthMin; z < boardMaxDistance; z++)
        {
            for (int x = 0; x < n; x++, i++)
            {
                CreateNode(x, z, n, i);
            }

            if (z < boardSize - 1)
            {
                n++;
            }
            else if (z >= boardSize - 1)
            {
                n--;
            }
        }
    }

    void CreateNode(int x, int z, int n, int i)
    {      
        Vector3 position;
        offset = (((float)n - (float)boardSize) / 2);
        position.x = (x - offset) * (nodeInnerRadius * 2f);
        position.y = 0f;
        position.z = z * (nodeOuterRadius * 1.5f);

        GridNode node = nodes[i] = Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;
    }

}
