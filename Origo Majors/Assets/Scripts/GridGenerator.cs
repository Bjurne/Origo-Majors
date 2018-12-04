using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour {

    //För ett liggande hex-grid
    public int boardSize = 5;
    public float nodeOuterRadius = 2f;
    float nodeInnerRadius; //float nodeInnerRadius = nodeOuterRadius * 0.866025405f;
    int boardMaxDistance;

    public GridNode gridNodePrefab;
    public Text nodeTextPrefab;

    GridNode[] nodes;

    Canvas gridCanvas;

    public void Awake ()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location

        UpdateBoardSizeVariables(boardSize);
        nodes = new GridNode[boardMaxDistance * boardMaxDistance];
    }

    private void Start ()
    {
        GenerateGameBoard(boardMaxDistance);
        PaintHexagon();
    }
	
    public void UpdateBoardSizeVariables (int boardSize)
    {
        boardMaxDistance = (boardSize * 2) - 1;
    }

    public void GenerateGameBoard (int boardMaxDist)
    {
        for (int z = 0, i = 0; z < boardMaxDist; z++)
        {
            for (int x = 0; x < boardMaxDist; x++)
            {
                CreateNode(x, z, i++);
            }
        }
    }

    public void PaintHexagon ()
    {
        //Calculate needed / unneeded nodes on the grid and inactivate unneeded nodes
    }

    /*  FOR OLD HEX SHAPE GRID, USE TO CALCULATE ACTIVE GAMEBOARD
    private int NodeCounter ()
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

    public void GenerateGameBoard ()
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
    */

    void CreateNode(int x, int z, int i)
    {

        // offset = (((float)n - (float)boardSize) / 2); //Hex shape old version of grid
        // position.x = (x - offset) * (nodeInnerRadius * 2f);

        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (nodeInnerRadius * 2f);
        position.y = 0f;
        position.z = z * (nodeOuterRadius * 1.5f);

        GridNode node = nodes[i] = Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;
        node.coordinates = GridCoordinates.FromOffsetCoordinates(x, z);

        Text text = Instantiate<Text>(nodeTextPrefab);
        text.rectTransform.SetParent(gridCanvas.transform, false);
        text.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        text.text = x.ToString() + "\n" + z.ToString();
        text.text = node.coordinates.ToStringOnSeparateLines();
        
    }

}
