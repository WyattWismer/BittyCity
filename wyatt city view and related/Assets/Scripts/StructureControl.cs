using System.Linq;
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

    private SortedSet<Node> sidewalks = new SortedSet<Node>();
    private SortedSet<Node> buildings = new SortedSet<Node>();
    private static readonly System.Random random = new System.Random();

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
        sidewalks.Add(new Node(i, j));
        sidewalkInfo.addStructure(i, j);
        grid[i, j] = 1;
        numSidewalks++;
        displayNumSidewalks();
    }

    public void removeSidewalk(int i, int j)
    {
        sidewalks.Remove(new Node(i, j));
        sidewalkInfo.removeStructure(i, j);
        grid[i, j] = 0;
        numSidewalks--;
        displayNumSidewalks();
    }

    public void addBuilding(int i, int j)
    {
        buildings.Add(new Node(i, j));
        buildingInfo.addStructure(i, j);
        grid[i, j] = 2;
        numBuildings++;
        displayNumBuildings();
    }

    public void removeBuilding(int i, int j)
    {
        buildings.Remove(new Node(i, j));
        buildingInfo.removeStructure(i, j);
        grid[i, j] = 0;
        numBuildings--;
        displayNumBuildings();
    }

    public Node getStructureLocation(Node node)
    {
        List<Node> options = new List<Node>();
        foreach (Node nxt in node.getNeighbors(gridWidth))
        {
            if (grid[nxt.i, nxt.j] == 0)
            {
                options.Add(nxt);
            }
        }
        if (options.Count == 0)
        {
            return null;
        }
        return options.ElementAt(random.Next(options.Count));
    }

    public Node getRandomSidewalk()
    {
        return sidewalks.ElementAt(random.Next(sidewalks.Count));
    }

    public Node getRandomBuilding()
    {
        return buildings.ElementAt(random.Next(buildings.Count));
    }

    public Object createFakeSidewalk(int i, int j)
    {
        grid[i, j] = -1;
        return sidewalkInfo.createFakeStructure(i, j);
    }

    public Object createFakeBuilding(int i, int j)
    {
        grid[i, j] = -1;
        return buildingInfo.createFakeStructure(i, j);
    }

    public bool inBuilding(Node node)
    {
        if(buildings.Contains(node))
        {
            return true;
        }
        return false;
    }

    void displayNumSidewalks()
    {
        numSidewalkText.text = "# Sidewalks: " + numSidewalks.ToString();
    }

    void displayNumBuildings()
    {
        numBuildingText.text = "# Buildings: " + numBuildings.ToString();
    }

    public void destroyObject(Object obj)
    {
        Destroy(obj);
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
                if (grid[nxt.i, nxt.j] > 0 && parent[nxt.i, nxt.j] == null)
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


public class Node : System.IComparable
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
    public int CompareTo(object other)
    {
        if (other == null) return 1;

        Node otherNode = other as Node;
        if (otherNode != null)
        {
            int ci = i.CompareTo(otherNode.i);
            int cj = j.CompareTo(otherNode.j);
            if (ci == 0)
            {
                return cj;
            }
            return ci;
        }
        else
        {
            throw new System.ArgumentException("Object is not a Node");
        }
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
    public Node getNode(int i)
    {
        return nodes[i];
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