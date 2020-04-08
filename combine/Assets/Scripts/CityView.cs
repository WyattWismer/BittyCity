using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityView : MonoBehaviour
{
    public CityControl cityControl;
    public Collider landscapeCollider;
    public StructureControl structureControl;
    public ItemApplication itemApplication;
    
    private bool mouseDown = false;
    //private CityState saveForTesting;

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

        /*
        if (Input.GetKey(KeyCode.K)) // test saving game
        {
            saveForTesting = cityControl.SaveCity();
        }

        if (Input.GetKey(KeyCode.L)) // test loading game
        {
            cityControl.LoadCity(saveForTesting);
        }
        */

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
    public StructureItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }

    public override bool isValidPosition(int i, int j)
    {
        return structureControl.isEmpty(i, j);
    }

    public override void cleanup()
    {
        structureControl.restoreSpot();
    }
}

public class SidewalkItemApplication : StructureItemApplication
{
    public SidewalkItemApplication(StructureControl _structureControl) : base(_structureControl)
    {
    }

    public override void draw(int i, int j)
    {
        structureControl.createFakeSidewalk(i, j, false);
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
        structureControl.createFakeBuilding(i, j, false);
    }

    public override void useItem(int i, int j)
    {
        cleanup();
        structureControl.addBuilding(i, j);
    }
}
