using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour {

    public enum MovementType {
        Normalized,
        Units
    }

    [SerializeField] private SplineAdvanced spline;
    public float speed = 1f;
    [SerializeField] private MovementType movementType;

    private float moveAmount;
    private float maxMoveAmount;

    private void Start() {
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
