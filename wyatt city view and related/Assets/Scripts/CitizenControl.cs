using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenControl : MonoBehaviour
{
    public StructureControl structureControl;
    public GameObject citizenPrefab;
    public float heightOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCitizen(int i, int j)
    {
        GameObject new_citizen = Instantiate(citizenPrefab, new Vector3(0.5f + i, heightOffset, 0.5f + j), Quaternion.identity);
        new_citizen.AddComponent<Citizen>();
        new_citizen.GetComponent<Citizen>().setup(structureControl, new Node(i, j));
    }
}


