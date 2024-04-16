using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class Movement : MonoBehaviour
{
    
    public Transform target;
    public float speed;
    private bool insideCarro=false;

    void Start()
    {
        
    }

    void Update()
    {
        if (insideCarro)
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition=target.position;

            transform.position = Vector3.MoveTowards(startPosition, targetPosition, speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideCarro = true;
            other.transform.SetParent(transform);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideCarro =false;
            
        }
    }

}
