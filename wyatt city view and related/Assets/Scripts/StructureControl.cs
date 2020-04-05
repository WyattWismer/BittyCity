using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureControl : MonoBehaviour
{
    public StructuresInfo sidewalkInfo;
    public StructuresInfo buildingInfo;
    public Text numSidewalkText;
    public Text numBuildingText;
    static private int gridWidth = 25;


    private int numSidewalks = 0;
    private int numBuildings = 0;


    // 0 is empty, 1 is sidewalk, 2 is building
    private int[,] grid = new int[gridWidth, gridWidth];

    // Start is called before the first frame update
    void Start()
    {

        displayNumSidewalks();
        displayNumBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSidewalk(int i, int j)
    {
        sidewalkInfo.addStructure(i, j);
        grid[i, j] = 1;
        numSidewalks++;
        displayNumSidewalks();
    }

    public void removeSidewalk(int i, int j)
    {
        sidewalkInfo.removeStructure(i, j);
        grid[i, j] = 0;
        numSidewalks--;
        displayNumSidewalks();
    }

    public void addBuilding(int i, int j)
    {
        buildingInfo.addStructure(i, j);
        grid[i, j] = 2;
        numBuildings++;
        displayNumBuildings();
    }

    public void removeBuilding(int i, int j)
    {
        buildingInfo.removeStructure(i, j);
        grid[i, j] = 0;
        numBuildings--;
        displayNumBuildings();
    }

    void displayNumSidewalks()
    {
        numSidewalkText.text = "# Sidewalks: " + numSidewalks.ToString();
    }

    void displayNumBuildings()
    {
        numBuildingText.text = "# Buildings: " + numBuildings.ToString();
    }

    public Path shortest_path(Node strt, Node end)
    {
        Node[,] parent = new Node[gridWidth, gridWidth];
        Queue < Node > q = new Queue<Node>();
        parent[strt.i, strt.j] = new Node(strt.i, strt.j);
        q.Enqueue(strt);

        // Breadth first search
        while (q.Count > 0)
        {
            Node x = q.Dequeue();
            // Optimization, early exit early if we find end
            if (x == end)
            {
                break;
            }

            foreach (Node nxt in x.getNeighbors(gridWidth))
            {
                if (grid[nxt.i, nxt.j] != 0 && parent[nxt.i, nxt.j] == null)
                {
                    parent[nxt.i, nxt.j] = x;
                    q.Enqueue(nxt);
                }
            }
        }

        // Check if accessible
        if (parent[end.i, end.j] == null)
        {
            // Not accessible
            return null;
        }

        Path result = new Path();
        for(Node c = end;  parent[c.i, c.j] != c; c = parent[c.i, c.j])
        {
            result.addNode(c);
        }
        result.addNode(strt);
        result.reverse();
        return result;
    }
}


public class Node
{
    public int i;
    public int j;
    public Node(int _i, int _j)
    {
        this.i = _i;
        this.j = _j;
    }
    public IEnumerable<Node> getNeighbors(int gridWidth)
    {
        for (int di = -1; di <= 1; di++)
        {
            for(int dj = -1; dj <= 1; dj++)
            {
                if (di + dj != 0 && (di == 0 || dj == 0))
                {
                    Node nxt = new Node(this.i + di, this.j + dj);
                    if (nxt.i >= 0 && nxt.i < gridWidth && nxt.j >= 0 && nxt.j < gridWidth)
                    {
                        yield return new Node(nxt.i, nxt.j);
                    }
                }
            }
        }
    }
    public float x
    {
        get { return 0.5f + i; }
    }
    public float z
    {
        get { return 0.5f + j; }
    }
    public override bool Equals(object obj)
    {
        return this.Equals(obj as Node);
    }
    public override int GetHashCode()
    {
        return i.GetHashCode() ^ j.GetHashCode();
    }
    public bool Equals(Node oth)
    {
        // If parameter is null, return false.
        if (Object.ReferenceEquals(oth, null))
        {
            return false;
        }
        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, oth))
        {
            return true;
        }
        // Return true if the fields match.
        return this.i == oth.i && this.j == oth.j;
    }
    public static Node operator +(Node a, Node b)
        => new Node(a.i + b.i, a.j + b.j);
    public static Node operator -(Node a, Node b)
        => new Node(a.i - b.i, a.j - b.j);
    public static bool operator ==(Node a, Node b)
    {
        if (Object.ReferenceEquals(a, null))
        {
            return Object.ReferenceEquals(b, null);
        }
        return a.Equals(b);
    }
    public static bool operator !=(Node a, Node b)
    {
        return !(a == b);
    }

}

public class Path
{
    private List<Node> nodes = new List<Node>();
    public void addNode(int i, int j)
    {
        addNode(new Node(i, j));
    }
    public void addNode(Node x)
    {
        if (nodes.Count >= 2)
        {
            Node delta_1 = nodes[nodes.Count - 1] - nodes[nodes.Count - 2];
            Node delta_2 = x - nodes[nodes.Count - 1];
            if (delta_1 == delta_2)
            {
                //Debug.Log("Optimized!");
                nodes.RemoveAt(nodes.Count - 1);
            }
        }
        nodes.Add(x);
    }
    public int size()
    {
        return nodes.Count;
    }
    public void reverse()
    {
        nodes.Reverse();
    }
    public Vector3 position(int index)
    {
        Node node = nodes[index];
        return new Vector3(node.x,0f,node.z);
    }
}
