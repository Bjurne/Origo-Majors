using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour {

    public int boardSize = 5;
    public float nodeOuterRadius = 2f;
    float nodeInnerRadius; //float nodeInnerRadius = nodeOuterRadius * 0.866025405f;
    int boardMaxDistance;

    public GridNode gridNodePrefab;
    public Text nodeTextPrefab;

    public GridNode[] nodes;

    Canvas gridCanvas;

    public Dictionary<Vector3, GridNode> dic; //Roberts variabelnamn, confirmed teacher standard

    public void Awake ()
    {
        dic = new Dictionary<Vector3, GridNode>();
        gridCanvas = GetComponentInChildren<Canvas>();
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location

        UpdateBoardSizeVariables(boardSize);
        nodes = new GridNode[boardMaxDistance * boardMaxDistance];
    }

    private void Start ()
    {
        GenerateGameBoard(boardMaxDistance);
        //FindCenter(boardSize);
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

    public void FindCenter (int boardSize)
    {
        //fixa formel för att hitta mitten???
        int centerX = 2;
        int centerY = -(boardSize + 1);
        int centerZ = (boardSize / 2) + 1;
        Vector3 center = new Vector3(0, 0, 0);
    }

    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
        if (Input.GetButtonDown("Jump")) {
            GridNode testNode;
            var pos = new Vector3(2, -5, 3);
            if (dic.TryGetValue(pos, out testNode))
                testNode.transform.localScale = new Vector3(2, 2, 2);
            else
                Debug.Log("almost");
        }
    }

    void HandleInput ()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell (Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        Debug.Log("touched at " + position);
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

        Vector3 cubeCoordinates;
        cubeCoordinates.x = x - z / 2;
        cubeCoordinates.y = -(x - z / 2) -z;
        cubeCoordinates.z = z;
        Debug.Log(-x -z);

        GridNode node = nodes[i] = Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;
        node.Coordinates = cubeCoordinates;

        dic.Add(cubeCoordinates, node);

       Text text = Instantiate<Text>(nodeTextPrefab);
        text.rectTransform.SetParent(gridCanvas.transform, false);
        text.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        text.text = cubeCoordinates.x.ToString() + "\n" +
                    cubeCoordinates.y.ToString() + "\n" +
                    cubeCoordinates.z.ToString();
    }

}
