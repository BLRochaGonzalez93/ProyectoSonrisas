using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;




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
    public float selectionTime = 3f; 

    private Dictionary<GameObject, Transform> selectorTransformMap;
    private bool isRotating = false;
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

    void RotateToTarget(Transform target)
    {
        // Inicia la rotación hacia el objetivo
        isRotating = true;
        StartCoroutine(RotateSmoothly(target));
    }

    IEnumerator RotateSmoothly(Transform target)
    {
        //Quaternion startRotation = transform.rotation;
        Quaternion startRotation = origin.rotation;
        Quaternion endRotation = Quaternion.LookRotation(target.forward, Vector3.up);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            origin.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
            Vector3 cameraForward = head.forward;
            cameraForward.y = 0f;
             Vector3 targetForward= target.forward;
            targetForward.y= 0f;
            
            float angle= Vector3.SignedAngle(cameraForward,targetForward,Vector3.up);
            //head.transform.rotation= Quaternion.Slerp(startRotation, endRotation, elapsedTime);
            //origin.RotateAround(cameraForward, targetForward, angle);
            //head.localRotation = Quaternion.identity;
            isRotating = false;
            yield return null;
        }

        
       // transform.rotation = endRotation;
       //origin.rotation=endRotation;
        isRotating = false;
    }
    

}
