using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour {

    internal int boardSize;
    internal float nodeOuterRadius;
    float nodeInnerRadius; //float nodeInnerRadius = nodeOuterRadius * 0.866025405f;
    int boardMaxDistance;


    public GridNode gridNodePrefab;
    public Text nodeTextPrefab;

    Canvas gridCanvas;

    public Dictionary<Vector3, GridNode> dic; //Roberts variabelnamn, confirmed teacher standard
    public GridNode[] activeNodes;

    //EXEMPEL PÅ HUR MAN ANVÄNDER DICTIONARYN
    //Detta hittar noden på koordinaten "centerNode", refererar till
    //gameObject "testNode" och ändrar storleken på den
    //  GridNode testNode;
    //  if (dic.TryGetValue(centerNode, out testNode))
    //  {
    //     testNode.transform.localScale = new Vector3(2, 2, 2);
    //  }


    private void Start ()
    {
        //Herr Svedlunds coola kod
        try
        {
            FindObjectOfType<TeleportGenerator>().GenerateTeleports();
            FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
        }
        catch
        {
            Debug.Log("No instance of TeleportGenerator or BoosterPickUpGenerator found.");
        }

    }
	
    public void CallGridGenerator (int bSize, float bSpacing)
    {
        InitializeGridGenVariables(bSize, bSpacing);

        GenerateGameBoard(boardMaxDistance);
        PaintActiveBoard();
        DisableInactiveNodes();
        NodeListToArray();
    }

    private void InitializeGridGenVariables(int bSize, float bSpacing)
    {
        boardSize = bSize;
        nodeOuterRadius = bSpacing;

        activeNodes = new GridNode[0];
        dic = new Dictionary<Vector3, GridNode>();
        gridCanvas = GetComponentInChildren<Canvas>();
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location ???
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

    //Hittar spelbrädets mittpunkt, returnerar koordinaten i en Vector3
    public Vector3 FindCenter ()
    {
        Vector3 centerNode = Vector3.zero;
        for (int i = 0; i < boardSize -1; i++)
        {
            centerNode += GridMetrics.Dir[0];
        }

        for (int i = 0; i < boardSize -1; i++)
        {

            if (i % 2 == 1)
                centerNode += GridMetrics.Dir[2];
            else
            {
                centerNode += GridMetrics.Dir[1];
            }
        }
        return centerNode;
    }

    //Målar hela brädet 
    public void PaintActiveBoard()
    {
        Vector3 origo = FindCenter();
        Vector3 paintCoordinate = origo;

        //Varje steg utåt från mitten
        for (int i = 0; i < boardSize; i++)
        {
            //Varje riktning ([Dir[0] - Dir[5])
            for (int j = 0; j < 6; j++)
            {
                paintCoordinate += GridMetrics.Dir[j] * (i);
                ToggleNode(paintCoordinate);

                //Rita noder med koordinaterna "i - 1" steg i en "Dir + 2" vinkel
                //Ritar noder till höger om noden i relation till nodens riktning mot mitten
                for (int k = 0; k < i - 1; k++)
                {
                    paintCoordinate += GridMetrics.Dir[ResetNumber(j + 2)];
                    ToggleNode(paintCoordinate);
                }
                paintCoordinate = origo;
            }
        }
    }

    private void ToggleNode(Vector3 nodeCoordinate, bool state = true)
    {
        GridNode currentNode;
        if (dic.TryGetValue(nodeCoordinate, out currentNode))
        {
            currentNode.isActive = state;
            
        }
        else
        {
            Debug.Log("No coordinate found! " + nodeCoordinate);
        }
    }

    //Håller alltid talet mellan 0 - 5 (används för förändringar i Dir[])
    int ResetNumber (int dir)
    {
        return dir % 6;
    }

    //TODO: Undersöka hur "var" används här, hur "kvp =>" används och vad "Linq" är.
    void DisableInactiveNodes ()
    {
        foreach (var item in dic.Where(kvp => !kvp.Value.isActive).ToList())
        {
            Destroy(item.Value.gameObject);
            dic.Remove(item.Key);
        }
    }

    void NodeListToArray()
    {
        List<GridNode> theNodes = new List<GridNode>();
        foreach (KeyValuePair<Vector3, GridNode> node in dic)
        {
            if (node.Value.isActive)
            {
                theNodes.Add(node.Value);
            }
        }
        activeNodes = theNodes.ToArray();
    }

    void CreateNode(int x, int z, int i)
    {
        //Fysisk position i world space
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (nodeInnerRadius * 2f);
        position.y = 0f;
        position.z = z * (nodeOuterRadius * 1.5f);

        //Motsvarande koordinat i det tredimensionella rutnätet (aka cube grid).
        Vector3 cubeCoordinates;
        cubeCoordinates.x = x - z / 2;
        cubeCoordinates.y = -(x - z / 2) -z;
        cubeCoordinates.z = z;

        //Lägger till noder i en array och ger tilldelar dem en plats i dictionaryn.
        //Varje nod har sin koordinat sparad i GridNode.cs som read only.
        GridNode node =  Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;
        node.Coordinates = cubeCoordinates;
        dic.Add(cubeCoordinates, node);

    //Lägger till text ovanpå alla noder. 
    //TODO: Learn how to use #if-statements in VS to separate game / editor code
        Text text = Instantiate<Text>(nodeTextPrefab);
        text.rectTransform.SetParent(gridCanvas.transform, false);
        text.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        text.text = cubeCoordinates.x.ToString() + "\n" +
                    cubeCoordinates.y.ToString() + "\n" +
                    cubeCoordinates.z.ToString();

    }

}
