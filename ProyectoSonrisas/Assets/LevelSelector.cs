using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class LevelSelector : MonoBehaviour
{
    private FollowPathOutside followPath;
    [SerializeField] private GameObject pointer;
    public InputActionReference trigger;
    public bool isTouchingBook= false;
    private ChestManager panel;
    private bool isTriggerPressed = false;
    private void Start()
    {
        followPath= FindObjectOfType<FollowPathOutside>();
         panel=FindObjectOfType<ChestManager>();
    }
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
        if (triggerValue>0 && !isTriggerPressed)
        {
            isTriggerPressed = true;
            //desactivar el pointer de la camara
            pointer.SetActive(false);
            panel.hideTooltip();
            
            //deberia salir panel de confirmación para saber que es el libro que quiere
            //y una vez confirmado el libro de ancla a la mano
            StartCoroutine(followPath.MoveToTargets());
            //SceneManager.LoadScene("Pathfinder");
            
            
        }
    }

    public void AtacthBookToHand()
    {
        //TODO 
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
