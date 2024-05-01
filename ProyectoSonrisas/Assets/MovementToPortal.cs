using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementToPortal : MonoBehaviour
{
    private bool _playerInCarro = false;
    [SerializeField] GameObject portal;
    [SerializeField] Transform origin;
   

    private void FixedUpdate()
    {
        if (_playerInCarro)
        {
            
            StartCoroutine(MoveCarToPortalCoroutine());
            //mover carro
            // transform.position = Vector3.MoveTowards(transform.position, portal.transform.position, 3f * Time.fixedDeltaTime);
            //
            //origin.transform.position = Vector3.MoveTowards(origin.transform.position, portal.transform.position, 3f * Time.deltaTime);
        }
    }
    IEnumerator MoveCarToPortalCoroutine()
    {
        // Esperar 1 segundo
        yield return new WaitForSeconds(1.6f);
        transform.SetParent(origin.transform,true);
        // Mover el carro hacia el portal
          origin.transform.position = Vector3.MoveTowards(origin.transform.position, portal.transform.position, 3f * Time.fixedDeltaTime);
        yield return null;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.transform.SetParent(transform);
            _playerInCarro = true;
            
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        _playerInCarro = false;
    //    }
    //}
}
