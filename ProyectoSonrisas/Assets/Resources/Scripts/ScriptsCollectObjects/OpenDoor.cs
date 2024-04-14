using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject aliade;

    public GameObject aliade_ref;

    private bool entered = false;

    private bool opened;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Door"))
        {
            entered = true;
            opened = false;
        }
        
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
        Debug.Log("Opened: " + opened);
        if (opened && Vector3.Distance(aliade.transform.position, aliade_ref.transform.position) >= 0.01)
        {
            Debug.Log("OPENED");
            aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, aliade_ref.transform.position, 15f*Time.deltaTime);
        }

        
    }

}

