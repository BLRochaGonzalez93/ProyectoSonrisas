using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour {

    public enum MovementType {
        Normalized,
        Units
    }

    [SerializeField] private SplineAdvanced spline;
    public float speed;
    [SerializeField] private MovementType movementType;

    public float moveAmount;
    public float maxMoveAmount;
    public int tramo=0;

    private void Start() {
        spline = GetComponent<RailPositionerManager>().splines[tramo];
        speed = GetComponentInChildren<RailPositionerManager>().speed;
        switch (movementType) {
            default:
            case MovementType.Normalized:
                maxMoveAmount = 1f;
                break;
            case MovementType.Units:
                maxMoveAmount = spline.GetSplineLength();
                break;
        }
    }

    private void Update() {
        speed = GetComponentInChildren<RailPositionerManager>().speed;
        if ((moveAmount + (Time.deltaTime * speed)) / maxMoveAmount >= 1)
        {
            tramo++;
            spline = GetComponent<RailPositionerManager>().splines[tramo];
        }
        moveAmount = (moveAmount + (Time.deltaTime * speed)) % maxMoveAmount;

        switch (movementType) {
            default:
            case MovementType.Normalized:
                transform.position = spline.GetPositionAt(moveAmount);
                transform.forward = spline.GetForwardAt(moveAmount);
                maxMoveAmount = 1f;
                break;
            case MovementType.Units:
                transform.position = spline.GetPositionAtUnits(moveAmount);
                transform.forward = spline.GetForwardAtUnits(moveAmount);
                maxMoveAmount = spline.GetSplineLength();
                break;
        }
    }

}
