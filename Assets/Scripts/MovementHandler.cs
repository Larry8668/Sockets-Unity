using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class MovementHandler : MonoBehaviour
{
    public float smoothing = 7f;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public void SetMoveCoords(Vector3 position, Quaternion rotation)
    {
        targetPosition = position;
        targetRotation = rotation;
        Debug.Log("got Vector -->" + targetPosition);
        Debug.Log("got Quaternion -->" + targetRotation);
    }

    void Update()
    {
        MoveTowardsTargetPosition();
        RotateTowardsTargetRotation();
    }

    void MoveTowardsTargetPosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }

    void RotateTowardsTargetRotation()
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing * Time.deltaTime);
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }
}
