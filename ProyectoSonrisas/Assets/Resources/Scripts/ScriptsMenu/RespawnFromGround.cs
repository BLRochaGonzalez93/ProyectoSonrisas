using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFromGround : MonoBehaviour
{
    private Vector3 _respawn;

    private void Start()
    {
        _respawn= transform.position;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            Debug.Log("Ha tocao suelo");
            transform.position = _respawn;
        }
    }
}
