using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float speed = 1;
    public StructureControl structureControl;
    public Node location;

    private Action action;

    public void setup(StructureControl _structureControl, Node _location)
    {
        structureControl = _structureControl;
        location = _location;
        action = ActionFactory.getAction(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
        if(action.Update())
        {
            // action completed, get new action
            action = ActionFactory.getAction(this);
        }
    }
}
/*
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
}*/
public static class ActionFactory
{
    private static readonly System.Random random = new System.Random();
    public static Action getAction(Citizen _parent)
    {
        while (true)
        {
            int action_choice = random.Next(5);
            switch (action_choice)
            {
                case 0:
                    return new walkToSidewalkAction(_parent);
                case 1:
                    return new walkToBuildingAction(_parent);
                case 2:
                case 3:
                    Node sidewalk_dst = tryToGetDesination(_parent);
                    if (sidewalk_dst == null) continue;
                    return new CreateSidewalkAction(_parent, sidewalk_dst);
                case 4:
                    Node building_dst = tryToGetDesination(_parent);
                    if (building_dst == null) continue;
                    return new CreateBuildingAction(_parent, building_dst);
                default:
                    throw new System.ArgumentException("Not a valid action");
            }
        }
    }

    public static Node tryToGetDesination(Citizen _parent)
    {
        if (_parent.structureControl.inBuilding(_parent.location)) return null; // citizen is inside building
        Node destination = _parent.structureControl.getStructureLocation(_parent.location);
        return destination;
    }
}

public abstract class Action
{
    protected Citizen parent;
    public Action(Citizen _parent)
    {
        parent = _parent;
    }
    public abstract bool Update();
}


public class CreateSidewalkAction : Action
{
    private Node destination;
    private float workRemaining;
    private float workSpeed;
    private Object fakeSidewalk;

    public CreateSidewalkAction(Citizen _parent, Node _destination) : base(_parent)
    {
        destination = _destination;
        workSpeed = 75;
        workRemaining = 100;
        fakeSidewalk = _parent.structureControl.createFakeSidewalk(destination.i, destination.j);
    }

    public override bool Update()
    {
        workRemaining -= workSpeed * Time.deltaTime;
        if(workRemaining <= 0)
        {
            parent.structureControl.addSidewalk(destination.i, destination.j);
            parent.structureControl.destroyObject(fakeSidewalk);
            return true;
        }
        return false;
    }
}

public class CreateBuildingAction : Action
{
    private Node destination;
    private float workRemaining;
    private float workSpeed;
    private Object fakeBuilding;

    public CreateBuildingAction(Citizen _parent, Node _destination) : base(_parent)
    {
        destination = _destination;
        workSpeed = 25;
        workRemaining = 100;
        fakeBuilding = _parent.structureControl.createFakeBuilding(destination.i, destination.j);
    }

    public override bool Update()
    {
        workRemaining -= workSpeed * Time.deltaTime;
        if (workRemaining <= 0)
        {
            parent.structureControl.addBuilding(destination.i, destination.j);
            parent.structureControl.destroyObject(fakeBuilding);
            return true;
        }
        return false;
    }
}

public abstract class NavigationAction : Action
{
    private Path path;
    private int path_index;

    protected NavigationAction(Citizen _parent, Node _destination) : base(_parent)
    {
        path = parent.structureControl.shortest_path(_parent.location, _destination);
        path_index = 0;
    }

    public override bool Update()
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
        }
        else
        {
            parent.location = path.getNode(path_index);
            path_index++;
        }
        return false;
    }
}

public class walkToSidewalkAction : NavigationAction
{
    public walkToSidewalkAction(Citizen _parent) : base(_parent, _parent.structureControl.getRandomSidewalk())
    {
    }
}

public class walkToBuildingAction : NavigationAction
{
    public walkToBuildingAction(Citizen _parent) : base(_parent, _parent.structureControl.getRandomBuilding())
    {
    }
}

