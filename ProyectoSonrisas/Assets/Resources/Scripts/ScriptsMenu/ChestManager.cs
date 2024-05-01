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
          
        
        
        panelChest.SetActive(true);

    }
    public void hideTooltip()
    {
        
        panelChest.SetActive(false);
    }


 

}
