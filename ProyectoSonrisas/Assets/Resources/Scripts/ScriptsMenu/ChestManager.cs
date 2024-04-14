using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject panelChest;
    
    

  
    public void ShowTooltip()
    {
          
        
        Debug.Log("Se ha pulsado");
        panelChest.SetActive(true);

    }
    public void hideTooltip()
    {
        Debug.Log("Se ha dejado de pulsar");
        panelChest.SetActive(false);
    }


 

}
