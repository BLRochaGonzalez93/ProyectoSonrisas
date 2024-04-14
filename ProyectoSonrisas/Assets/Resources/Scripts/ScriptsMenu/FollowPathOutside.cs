using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class FollowPathOutside : MonoBehaviour
{
 
    public Transform[] targetPoints;
    public float rotationSpeed;
    
    

    public Transform origin;
    public Transform head;

    void Start()
    {
       
    }

    public IEnumerator MoveToTargets()
    {
        for (int i = 0; i < targetPoints.Length; i++)
        {   
            yield return StartCoroutine(RotateThenMove(targetPoints[i]));
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    IEnumerator RotateThenMove(Transform target)
    {
        Quaternion startRotation = origin.rotation;
        Vector3 directionToTarget = (target.position - origin.position).normalized;
        Quaternion endRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        
        float rotationTime = 0f;

        while (rotationTime < 1.4f)
        {
            rotationTime += Time.deltaTime / rotationSpeed;
            origin.rotation = Quaternion.Slerp(startRotation, endRotation, rotationTime);
            yield return null;
        }

        StartCoroutine(MoveToTarget(target.position));
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = origin.position;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (true)
        {
            float distanceCovered = Time.time - startTime;
            float journeyFraction = distanceCovered / journeyLength;
            origin.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            if (journeyFraction >= 1f)
                
                break;

            yield return null;
        }
    }
}