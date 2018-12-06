using System;
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

    Canvas gridCanvas;

    public GridNode[] activeNodes;

    public Dictionary<Vector3, GridNode> dic; //Roberts variabelnamn, confirmed teacher standard

    Material m_Material;

    Vector3 pos;

    //EXEMPEL PÅ HUR MAN ANVÄNDER DICTIONARYN
    //  GridNode testNode;
    //  if (dic.TryGetValue(centerNode, out testNode))
    //  {
    //     testNode.transform.localScale = new Vector3(2, 2, 2);
    //  }

    public void Awake ()
    {
        activeNodes = new GridNode[boardMaxDistance * boardMaxDistance];
        dic = new Dictionary<Vector3, GridNode>();
        gridCanvas = GetComponentInChildren<Canvas>();
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location

        UpdateBoardSizeVariables(boardSize);
    }

    private void Start ()
    {
        GenerateGameBoard(boardMaxDistance);
        //PaintCenter();
        PaintBoard();
        DisableInactiveNodes();
        NodeListToArray();
 

        //Herr Svedlunds coola kod
        //FindObjectOfType<TeleportGenerator>().GenerateTeleports();
        //FindObjectOfType<BoosterPickUpGenerator>().GenerateBoosterPickUps();
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

    //Markerar mittNoden med en röd färg
    public void PaintCenter ()
    {
        GridNode testNode;
        if (dic.TryGetValue(FindCenter(), out testNode))
        {
            m_Material = testNode.GetComponent<MeshRenderer>().material;
            m_Material.color = Color.red;
        }
    }

    //Målar hela brädet 
    public void PaintBoard()
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

    private void PaintNode (Vector3 paintCoordinate)
    {
        GridNode paintNode;
        if (dic.TryGetValue(paintCoordinate, out paintNode))
        {
            m_Material = paintNode.GetComponent<MeshRenderer>().material;
            m_Material.color = Color.red;
        }
        else
        {
            Debug.Log("No coordinate found! " + paintCoordinate);
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

    int ResetNumber (int dir)
    {
        return dir % 6;
    }

    void DisableInactiveNodes ()
    {
        foreach (KeyValuePair<Vector3, GridNode> node in dic)
        {
            if (!node.Value.isActive)
            {
                Destroy(node.Value.gameObject);
                //dic.Remove(node.Key); //TODO: Fråga robert
            }
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

    //void Update () //Used for testing purposes only
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        HandleInput();
    //        Debug.Log("Mouse button being pressed.");
    //    }
    //}

    //Kallas vid musknappsklick, används enbart för test
    void HandleInput ()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            m_Material = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
            m_Material.color = Color.red;
        }
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
