using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject panelChest;
    [SerializeField] InputActionReference inputTrigger;

    private void Update()
    {
       
    }
    public void ShowTooltip()
    {
        panelChest.SetActive(!panelChest);
    }
       
    

}
