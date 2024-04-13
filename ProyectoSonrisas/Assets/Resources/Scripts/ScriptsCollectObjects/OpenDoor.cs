using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject aliade;

    public GameObject wagon;

    private bool entered = false;

    private bool opened;

    Vector3 original_position;
    

    void OnTriggerEnter()
    {
        entered = true;
        opened = false;
    }

    void Start()
    {
        original_position = new Vector3(aliade.transform.position.x, aliade.transform.position.y, aliade.transform.position.z);
    }

    void Update()
    {
        if (entered){
            if (Vector3.Distance(aliade.transform.position, door.transform.position) > 5)
            {
                aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, door.transform.position, 10f*Time.deltaTime);
            }
            else
            {
                opened = true;
                door.SetActive(false);  
            }
        }

        if (opened && Vector3.Distance(aliade.transform.position, wagon.transform.position) < 1)
        {
            Debug.Log("OPENED");
            aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, wagon.transform.position, 15f*Time.deltaTime);
        }

        
    }

}

