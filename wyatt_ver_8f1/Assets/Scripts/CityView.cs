using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityView : MonoBehaviour
{
    public Collider landscapeCollider;
    public StructureControl structureControl;
    public ItemApplication itemApplication;
    private bool mouseDown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (itemApplication != null) itemApplication.cleanup();
            itemApplication = new SidewalkItemApplication(structureControl);
        }

        if (Input.GetKey(KeyCode.B))
        {
            if (itemApplication != null) itemApplication.cleanup();
            itemApplication = new BuildingItemApplication(structureControl);
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (itemApplication != null) itemApplication.cleanup();
            itemApplication = new BombItemApplication(structureControl);
        }

        if (itemApplication != null)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (landscapeCollider.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                int i = (int)hit.point.x;
                int j = (int)hit.point.z;

                itemApplication.draw(i, j);

                if (mouseDown)
                {
                    if (itemApplication.isValidPosition(i, j))
                    {
                        itemApplication.useItem(i, j);
                    }
                }
            }
        }
    }
}


public abstract class ItemApplication
{
    protected StructureControl structureControl;
    public abstract void draw(int i, int j);
    public abstract bool isValidPosition(int i, int j);
    public abstract void useItem(int i, int j);
    public abstract void cleanup();
    protected ItemApplication(StructureControl _structureControl)
    {
        structureControl = _structureControl;
    }
}

public class BombItemApplication : ItemApplication
{
    public BombItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }
    public override void draw(int i, int j)
    {
        structureControl.createDeadSpot(i, j);
    }
    public override bool isValidPosition(int i, int j)
    {
        return structureControl.isStructure(i, j);
    }
    public override void useItem(int i, int j)
    {
        cleanup();
        structureControl.removeStructure(i, j);
    }
    public override void cleanup()
    {
        structureControl.restoreSpot();
    }
}

public abstract class StructureItemApplication : ItemApplication
{
    protected GameObject structure;
    public StructureItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }

    public override bool isValidPosition(int i, int j)
    {
        return structureControl.isEmpty(i, j);
    }

    public override void cleanup()
    {
        if (structure != null)
        {
            structureControl.destroyObject(structure);
        }
    }
}



public class SidewalkItemApplication : StructureItemApplication
{
    public SidewalkItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }

    public override void draw(int i, int j)
    {
        if (structure == null)
        {
            structure = (GameObject)structureControl.createFakeSidewalk(i, j, false);
        }
        else
        {
            structure.transform.position = new Vector3(0.5f + i, 0.05f, 0.5f + j);
        }
    }

    public override void useItem(int i, int j)
    {
        cleanup();
        structureControl.addSidewalk(i, j);
    }
}

public class BuildingItemApplication : StructureItemApplication
{
    public BuildingItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }

    public override void draw(int i, int j)
    {
        if (structure == null)
        {
            structure = (GameObject)structureControl.createFakeBuilding(i, j, false);
        }
        else
        {
            structure.transform.position = new Vector3(0.5f + i, 0.5f, 0.5f + j);
        }
    }

    public override void useItem(int i, int j)
    {
        cleanup();
        structureControl.addBuilding(i, j);
    }
}
