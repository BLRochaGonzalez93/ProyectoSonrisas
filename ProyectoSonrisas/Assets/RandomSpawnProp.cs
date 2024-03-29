using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnProp : MonoBehaviour
{
    public List<GameObject> spwnProps;
    public int amount;
    public GameObject parentPlane;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount - 1; i++)
        {
            GameObject prop = spwnProps[Random.Range(0, spwnProps.Count)];
            Vector3 pos = new Vector3(Random.Range(parentPlane.transform.position.x - 100, parentPlane.transform.position.x + 100), parentPlane.transform.position.y, Random.Range(parentPlane.transform.position.z - 50, parentPlane.transform.position.z + 50));
            Instantiate(prop, pos, Quaternion.Euler(-90f, 0, 0), parentPlane.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
