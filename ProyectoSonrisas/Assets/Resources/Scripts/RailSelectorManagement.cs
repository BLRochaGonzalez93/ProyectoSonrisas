using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RailSelectorManagement : MonoBehaviour
{
    public List<Rail> railPrefabs;
    public List<Rail> typeSRail, typeARail, typeBRail, typeCRail;


    // Start is called before the first frame update
    void Start()
    {
        railPrefabs.Sort(Rail.SortByID);
        for (int i = 0; i < railPrefabs.Count; i++)
        {
            if (railPrefabs[i].type == RailType.TYPE_S) typeSRail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_A) typeARail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_B) typeBRail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_C) typeCRail.Add(railPrefabs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
