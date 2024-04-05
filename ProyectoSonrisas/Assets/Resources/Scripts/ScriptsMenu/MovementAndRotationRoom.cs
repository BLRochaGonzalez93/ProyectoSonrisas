using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndRotationRoom : MonoBehaviour
{
    
    
        [System.Serializable]
        public struct SelectorTransformPair
        {
            public GameObject selector;
            public Transform targetTransform;
        }

        [System.Serializable]
        public struct TransformRotationPair
        {
            public Transform targetTransform;
            public Quaternion finalRotation;
        }

        public SelectorTransformPair[] selectorTransformPairs;
        public TransformRotationPair[] transformRotationPairs;

        public float rotationSpeed = 5f;
        public float movementSpeed = 2f;
        public float selectionTime = 3f;

        private Dictionary<GameObject, Transform> selectorTransformMap;
        private Dictionary<Transform, Quaternion> targetRotationMap;
        private bool isRotating = false;
        private bool isMoving = false;
        private float rotationTimer = 0f;

        public Transform origin;
        public Transform head;

        void Start()
        {
            selectorTransformMap = new Dictionary<GameObject, Transform>();
            targetRotationMap = new Dictionary<Transform, Quaternion>();

            foreach (var pair in selectorTransformPairs)
            {
                selectorTransformMap.Add(pair.selector, pair.targetTransform);
            }
            foreach (var pair in transformRotationPairs)
            {
                targetRotationMap.Add(pair.targetTransform, pair.finalRotation);
            }
        }

        void Update()
        {
            if (!isRotating && !isMoving)
            {
                RaycastHit hit;
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;

                    if (selectorTransformMap.ContainsKey(hitObject))
                    {
                        Debug.Log("Objeto detectado: " + hitObject.name);
                        rotationTimer += Time.deltaTime;

                        if (rotationTimer >= selectionTime)
                        {
                            RotateAndMoveTowardsTarget(selectorTransformMap[hitObject]);
                            rotationTimer = 0f;
                        }
                    }
                    else
                    {
                        rotationTimer = 0f;
                    }
                }
            }
        }

        void RotateAndMoveTowardsTarget(Transform target)
        {
            StartCoroutine(RotateAndMove(target));
        }

        IEnumerator RotateAndMove(Transform target)
        {
            isRotating = true;
            Quaternion startRotation = origin.rotation;
            Quaternion endRotation = Quaternion.LookRotation(target.position - origin.position, Vector3.up);

            float rotationElapsedTime = 0f;

            while (rotationElapsedTime < 1f)
            {
                rotationElapsedTime += Time.deltaTime * rotationSpeed;
                origin.rotation = Quaternion.Slerp(startRotation, endRotation, rotationElapsedTime);
                yield return null;
            }

            // Una vez que se ha completado la rotación, movemos hacia el objetivo
            isMoving = true;
            Vector3 startPosition = origin.position;
            Vector3 targetPosition = target.position;
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;

            while (isMoving)
            {
                float distanceCovered = (Time.time - startTime) * movementSpeed;
                float journeyFraction = distanceCovered / journeyLength;
                origin.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

                if (journeyFraction >= 1f)
                {
                    isMoving = false;
                // Si se alcanza el destino, aplicamos la rotación final
                if (targetRotationMap.ContainsKey(target))
                {
                    // Obtenemos el Quaternion de la rotación final
                    Quaternion finalRotationQuaternion = targetRotationMap[target];
                    // Asignamos el Quaternion al objeto origin
                    origin.rotation = Quaternion.Slerp(startRotation,finalRotationQuaternion, rotationElapsedTime);
                    origin.rotation = finalRotationQuaternion;
                }

            }

            yield return null;
            }

            isRotating = false;
        }
    }

