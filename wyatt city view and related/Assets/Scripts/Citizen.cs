using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float speed = 1;
    public StructureControl structureControl;
    public CitizenControl citizenControl;
    public Node location;
    public int actionsLeftUntilMatingAllowed;

    private Action action;
    

    public void setup(CitizenControl _citizenControl, StructureControl _structureControl, Node _location)
    {
        citizenControl = _citizenControl;
        structureControl = _structureControl;
        location = _location;
        action = ActionFactory.getAction(this);
        actionsLeftUntilMatingAllowed = 0;
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
            if (actionsLeftUntilMatingAllowed > 0)
            {
                actionsLeftUntilMatingAllowed--;
            }
        }
    }
}

public static class ActionFactory
{
    private static readonly System.Random random = new System.Random();
    public static Action getAction(Citizen _parent)
    {
        while (true)
        {
            int action_choice = random.Next(13);
            switch (action_choice)
            {
                case 0:
                    Node rand_sidewalk_dst = _parent.structureControl.getRandomSidewalk();
                    Path rand_sidewalk_path = _parent.structureControl.shortest_path(_parent.location, rand_sidewalk_dst);
                    if (rand_sidewalk_path == null) continue;
                    return new NavigationAction(_parent, rand_sidewalk_path);
                //return new walkToSidewalkAction(_parent);
                case 1:
                case 2:
                    Node rand_building_dst = _parent.structureControl.getRandomBuilding();
                    Path rand_building_path = _parent.structureControl.shortest_path(_parent.location, rand_building_dst);
                    if (rand_building_path == null) continue;
                    return new NavigationAction(_parent, rand_building_path);
                //return new walkToBuildingAction(_parent);
                case 3:
                case 4:
                case 5:
                    Node sidewalk_dst = tryToGetDesination(_parent);
                    if (sidewalk_dst == null) continue;
                    return new CreateSidewalkAction(_parent, sidewalk_dst);
                case 6:
                    Node building_dst = tryToGetDesination(_parent);
                    if (building_dst == null) continue;
                    return new CreateBuildingAction(_parent, building_dst);
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    //Debug.Log("Trying to mate");
                    if (_parent.actionsLeftUntilMatingAllowed > 0) continue; // must do more work before mating
                    if (!citizenInBuilding(_parent)) continue;
                    bool mate_started = _parent.citizenControl.tryToMate(_parent.location.i, _parent.location.j);
                    if(mate_started)
                    {
                        _parent.actionsLeftUntilMatingAllowed = 5;
                        return new MateAction(_parent);
                    }
                    else
                    {
                        continue;
                    }
                default:
                    throw new System.ArgumentException("Not a valid action");
            }
        }
    }

    public static Node tryToGetDesination(Citizen _parent)
    {
        if (citizenInBuilding(_parent)) return null; // citizen is inside building
        Node destination = _parent.structureControl.getStructureLocation(_parent.location);
        return destination;
    }

    public static bool citizenInBuilding(Citizen _parent)
    {
        return _parent.structureControl.inBuilding(_parent.location);
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

public class MateAction : Action
{
    private float patienceRemaining;
    private float boredomSpeed;

    public MateAction(Citizen _parent) : base(_parent)
    {
        boredomSpeed = 25;
        patienceRemaining = 100;
    }

    public override bool Update()
    {

        var i = parent.location.i;
        var j = parent.location.j;
        if (parent.citizenControl.hasMated(i, j))
        { 
            // new citizen gets created in citizenControl.hasMated
            return true;
        }

        patienceRemaining -= boredomSpeed * Time.deltaTime;
        if (patienceRemaining <= 0)
        {
            // give up
            parent.actionsLeftUntilMatingAllowed = 0;
            parent.citizenControl.giveUpOnMating(i, j);
            return true;
        }

        return false;
    }
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

public class NavigationAction : Action
{
    private Path path;
    private int path_index;

    /*
    protected NavigationAction(Citizen _parent, Node _destination) : base(_parent)
    {
        path = parent.structureControl.shortest_path(_parent.location, _destination);
        path_index = 0;
    }
    */
    public NavigationAction(Citizen _parent, Path _path) : base(_parent)
    {
        path = _path;
        path_index = 0;
    }

    public override bool Update()
    {
        // see if we have completed the path
        if (path == null || path_index == path.size())
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

/*
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
*/

