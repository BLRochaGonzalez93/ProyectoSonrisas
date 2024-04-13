using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class LevelSelector : MonoBehaviour
{
    public InputActionReference trigger;
    public bool isTouchingBook= false;
    private void Update()
    {
        if (isTouchingBook==true)
        {
            GoToLevelOne();
        }
        
    }

   

    public void GoToLevelOne()
    {
        float triggerValue = trigger.action.ReadValue<float>();
        if (triggerValue>0 )
        {
            SceneManager.LoadScene("Pathfinder");
            Debug.Log("Trigger pulsado");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingBook = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingBook = false;
        }
    }

    public void GoLevelOne()
    {
        Debug.Log("Prueba:Se ha pulsado");
    }
}
