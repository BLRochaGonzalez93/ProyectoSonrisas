using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Transform cameraTransform; 
    public float pointerDistance = 5f; 

    void Update()
    {
        PointerCamera();
    }

    private void PointerCamera()
    {

        
        Vector3 pointerPosition = cameraTransform.position + cameraTransform.forward * pointerDistance;


        // Asignar la posicion calculada al puntero
        transform.position = pointerPosition;

        // Opcional: Ajustar la rotacion del puntero para que mire hacia adelante
        transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
    }
}
