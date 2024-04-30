using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using TMPro;
using System;




public class RotatingRoom : MonoBehaviour
{
    [System.Serializable]
    public struct SelectorTransformPair
    {
        public GameObject selector;
        public Transform targetTransform;
    }

    public SelectorTransformPair[] selectorTransformPairs;
    public float rotationSpeed = 5f;
    public float rotationDuration = 5f;
    public float selectionTime = 3f;

    public float movementSpeed = 3f; // Velocidad de movimiento

    private Dictionary<GameObject, Transform> selectorTransformMap;
    private bool isRotating = false;
    private bool isMoving = false; // Bandera de movimiento
    private Transform currentRotationTarget;
    private float rotationTimer = 0f;

    public Transform origin;
    public Transform head;

    void Start()
    {

        selectorTransformMap = new Dictionary<GameObject, Transform>();
        foreach (var pair in selectorTransformPairs)
        {
            selectorTransformMap.Add(pair.selector, pair.targetTransform);
        }
    }

    void Update()
    {
        if (!isRotating)
        {
            // Ray from camera to detect selectors
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;


                if (selectorTransformMap.ContainsKey(hitObject))
                {
                    

                    rotationTimer += Time.deltaTime;

                    // time limit to rotate
                    if (rotationTimer >= selectionTime)
                    {
                        // RotateToTarget(selectorTransformMap[hitObject]);
                        RotateToTarget(selectorTransformMap[hitObject]);
                        
                        rotationTimer = 0f;
                    }
                }
                else
                {
                    // reset time if player
                    rotationTimer = 0f;
                }
            }
        }
    }

    //void RotateToTarget(Transform target)
    //{
    //    // Inicia la rotaci�n hacia el objetivo
    //    isRotating = true;
    //    StartCoroutine(RotateSmoothly(target));
    //}

    //IEnumerator RotateSmoothly(Transform target)
    //{
    //    //Quaternion startRotation = transform.rotation;
    //    Quaternion startRotation = origin.rotation;
    //    Quaternion endRotation = Quaternion.LookRotation(target.forward, Vector3.up);

    //    float elapsedTime = 0f;

    //    while (elapsedTime < 1f)
    //    {
    //        elapsedTime += Time.deltaTime * rotationSpeed;
    //        origin.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
    //        Vector3 cameraForward = head.forward;
    //        cameraForward.y = 0f;
    //         Vector3 targetForward= target.forward;
    //        targetForward.y= 0f;

    //        float angle= Vector3.SignedAngle(cameraForward,targetForward,Vector3.up);
    //        //head.transform.rotation= Quaternion.Slerp(startRotation, endRotation, elapsedTime);
    //        //origin.RotateAround(cameraForward, targetForward, angle);
    //        //head.localRotation = Quaternion.identity;
    //        isRotating = false;
    //        yield return null;
    //    }


    //   // transform.rotation = endRotation;
    //   //origin.rotation=endRotation;
    //    isRotating = false;
    //}

    //functions to move and rotate

    void RotateToTarget(Transform target)
    {
        isRotating = true;
        StartCoroutine(RotateAndMove(target));
    }
    //esta es la buena

    IEnumerator RotateAndMove(Transform target)
    {
        Quaternion startRotation = origin.rotation;
        Vector3 directionToTarget = (target.position - origin.position).normalized;
        Quaternion endRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        float elapsedTime = 0f;

        while (elapsedTime < 1.6f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            origin.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);

            if (!isMoving && elapsedTime >= 0.7f) // Comienza a moverse despu�s de la mitad de la rotaci�n
            {
                isMoving = true;
                //StartCoroutine(MoveTowardsTarget(target.position));
                StartCoroutine(MoveTowardsTarget(target.position, () =>
                {
                    // Recenter(target);
                    StartCoroutine(AdjustOrientation(target, rotationDuration));
                }));


            }

            yield return null;
        }


        isRotating = false;
    }






    IEnumerator MoveTowardsTarget(Vector3 targetPosition, Action onMovementComplete)
    {
        Vector3 startPosition = origin.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;



        while (true)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float journeyFraction = distanceCovered / journeyLength;
            origin.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            if (journeyFraction >= 1f)
            {
                isMoving = false;

                if (onMovementComplete != null)
                {
                    onMovementComplete(); // Llama a la funci�n de ajuste de posici�n y orientaci�n
                }



                break;
            }

            yield return null;
        }
    }

    //private void Recenter(Transform target)
    //{
    //    XROrigin xrOrigin = GetComponent<XROrigin>();
    //    //xrOrigin.MoveCameraToWorldLocation(target.position);

    //    xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);

    //}

    private IEnumerator AdjustOrientation(Transform target, float duration)
    {




        // Obtener la rotaci�n actual del origen
        Quaternion startRotation = origin.rotation;
        // Guardar el vector forward del objetivo
        Vector3 targetForward = target.forward;

        // Calcular la rotaci�n objetivo basada en el vector forward del objetivo
        Quaternion targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);

        float elapsedTime = 0f;

        // Realizar la interpolaci�n durante la duraci�n especificada
        while (elapsedTime < duration)
        {
            // Calcular la fracci�n del tiempo transcurrido
            float t = elapsedTime / duration;

            // Interpolar entre las rotaciones actual y objetivo
            Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            // Aplicar la nueva rotaci�n al origen
            origin.rotation = newRotation;

            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Asegurarse de que la rotaci�n sea exactamente la rotaci�n objetivo al final
        origin.rotation = targetRotation;
    }



}


