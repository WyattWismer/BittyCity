using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float speed = 3;

    private Action action;
    public StructureControl structureControl;

    public void setup(StructureControl _structureControl)
    {
        structureControl = _structureControl;
        action = new Action(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
        action.Update();
    }
}

public class Action
{
    private Citizen parent;
    private Path path;
    private int path_index;
    public Action(Citizen _parent)
    {
        parent = _parent;
        path = parent.structureControl.shortest_path(new Node(10,11), new Node(12,9));
        //Debug.Log("Path Size: " + path.size().ToString());
        path_index = 0;
    }

    public bool Update()
    {
        // see if we have completed the path
        if (path_index == path.size())
        {
            return true;
        }

        // find where to move to
        Vector3 destination = path.position(path_index);
        destination.y = parent.transform.position.y;

        if ((parent.transform.position - destination).magnitude > parent.speed * Time.deltaTime)
        {
            //Debug.Log("My position: " + parent.transform.position.ToString());
            //Debug.Log("Destination: " + destination.ToString());
            Vector3 delta = destination - parent.transform.position;
            delta /= delta.magnitude;
            delta *= parent.speed;
            parent.transform.Translate(delta * Time.deltaTime);
        } else
        {
            //Debug.Log("Hit corner");
            path_index++;
        }
        return false;
    }
}
