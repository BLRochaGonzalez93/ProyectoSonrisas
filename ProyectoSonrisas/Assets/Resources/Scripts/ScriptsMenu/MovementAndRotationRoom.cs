using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MovementAndRotationRoom : MonoBehaviour
{
    [System.Serializable]
    public struct SelectorTransformPair
    {
        public GameObject selector;
        public Transform targetTransform;
    }

    public SelectorTransformPair[] selectorTransformPairs;
    public float rotationSpeed ;
    public float selectionTime ;

    public float movementSpeed; // Velocidad de movimiento

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
                    Debug.Log("Objeto detectado: " + hitObject.name);

                    rotationTimer += Time.deltaTime;

                    // time limit to rotate
                    if (rotationTimer >= selectionTime)
                    {
                        // RotateToTarget(selectorTransformMap[hitObject]);
                        RotateToTarget(selectorTransformMap[hitObject]);
                        Debug.Log("Objeto rotando hacia: " + selectorTransformMap[hitObject]);
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



    IEnumerator RotateThenMove(Transform target)
    {
        Quaternion startRotation = origin.rotation;
        Vector3 directionToTarget = (target.position - origin.position).normalized;
        Quaternion endRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        float rotationTime = 0f;

        while (rotationTime < 1.6f)
        {
            rotationTime += Time.deltaTime / rotationSpeed;
            origin.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime);
            isRotating = false;
            yield return null;
        }

        StartCoroutine(AdjustOrientation(target, rotationSpeed));

        yield return new WaitUntil(() => !isRotating);

        StartCoroutine(MoveTowardsTarget(target.position));
    }

    void RotateToTarget(Transform target)
    {
        isRotating = true;
        StartCoroutine(RotateThenMove(target));
    }

    IEnumerator MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = origin.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (true)
        {
            float distanceCovered = (Time.time - startTime);// * movementSpeed;
            float journeyFraction = distanceCovered / journeyLength;
            origin.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            if (journeyFraction >= 1f)
            {
                isMoving = false;
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

        IEnumerator AdjustOrientation(Transform target, float duration)
        {
            XROrigin xrOrigin = GetComponent<XROrigin>();

            // Guardar el vector forward del objetivo
            Vector3 targetForward = target.forward;

            // Obtener la rotación actual del origen
            Quaternion startRotation = xrOrigin.transform.rotation;

            // Calcular la rotación objetivo basada en el vector forward del objetivo
            Quaternion targetRotation = Quaternion.LookRotation(targetForward, target.up);

            float elapsedTime = 0f;

            // Realizar la interpolación durante la duración especificada
            while (elapsedTime < duration)
            {
                // Calcular la fracción del tiempo transcurrido
                float t = elapsedTime / duration;

                // Interpolar entre las rotaciones actual y objetivo
                Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, t);

                // Aplicar la nueva rotación al origen
                xrOrigin.transform.rotation = newRotation;

                // Incrementar el tiempo transcurrido
                elapsedTime += Time.deltaTime;
            isRotating = false;
            yield return null;
            }

            // Asegurarse de que la rotación sea exactamente la rotación objetivo al final
            xrOrigin.transform.rotation = targetRotation;
        }
    }




