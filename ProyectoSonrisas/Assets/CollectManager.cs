using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    KeyHUD keyHUD;
    
    void Start()
    {
        keyHUD = FindObjectOfType<KeyHUD>();
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            keyHUD.Keys++;
            other.gameObject.SetActive(false);
        }

    }
}
