using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour {

    public int boardSize = 5;
    public float nodeOuterRadius = 2f;
    public int publicInt = 0;
    float nodeInnerRadius; //float nodeInnerRadius = nodeOuterRadius * 0.866025405f;
    int boardMaxDistance;

    public GridNode gridNodePrefab;
    public Text nodeTextPrefab;

    public GridNode[] nodes;

    Canvas gridCanvas;

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
        dic = new Dictionary<Vector3, GridNode>();
        gridCanvas = GetComponentInChildren<Canvas>();
        nodeInnerRadius = nodeOuterRadius * 0.866025405f; // temp location

        UpdateBoardSizeVariables(boardSize);
        nodes = new GridNode[boardMaxDistance * boardMaxDistance];
    }

    private void Start ()
    {
        GenerateGameBoard(boardMaxDistance);
        //PaintCenter();
        PaintBoard();

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

    //IN PROGRESS: Målar hela brädet 
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

                PaintNode(paintCoordinate);

                paintCoordinate = origo;
            }
        }

        //noll steg, rita mitten + 4 vänster + 4 höger 
        //ett steg, rita mitten + 4 vänster + 3 höger
        //två steg, rita mitten + 3 vänster + 3 höger
        //tre steg, rita mitten + 3 vänster + 2 höger
        //fyra steg, ritta mitten + 2 vänster + 2 höger
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


    int ResetNumber (int dir)
    {
        return dir % 6;
    }

    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            //HandleInput();
            //Debug.Log("Mouse button being pressed.");
        }
        if (Input.GetButtonDown("Jump")) {

            GridNode testNode;

            if (dic.TryGetValue(FindCenter(), out testNode))
            {
                testNode.transform.localScale = new Vector3(2, 2, 2);
                testNode.Coordinates += GridMetrics.Dir[Random.Range(0, 6)];
            }
            else
            {
                Debug.Log("almost");
            }
        }
    }

    //Kallas vid musknappsklick, används enbart för test
    void HandleInput ()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            m_Material = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
            m_Material.color = Color.red;
            Debug.Log("Doing things.");
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
        GridNode node = nodes[i] = Instantiate<GridNode>(gridNodePrefab);
        node.transform.SetParent(transform, false);
        node.transform.localPosition = position;
        node.Coordinates = cubeCoordinates;
        dic.Add(cubeCoordinates, node);

        //Lägger till text ovanpå alla noder.
       Text text = Instantiate<Text>(nodeTextPrefab);
        text.rectTransform.SetParent(gridCanvas.transform, false);
        text.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        text.text = cubeCoordinates.x.ToString() + "\n" +
                    cubeCoordinates.y.ToString() + "\n" +
                    cubeCoordinates.z.ToString();
    }

}
