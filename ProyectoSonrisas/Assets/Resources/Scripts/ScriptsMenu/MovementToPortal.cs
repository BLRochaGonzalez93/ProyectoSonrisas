using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementToPortal : MonoBehaviour
{
    private bool _playerInCarro= false;
    [SerializeField] GameObject portal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_playerInCarro)
        {
            

            //mover carro
            transform.position = Vector3.MoveTowards(transform.position, portal.transform.position, 2f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInCarro = true;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInCarro = false;
        }
    }
}
