using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followDistance = 3f;
    public float upOffset = 1.7f;
    public float rightOffset = 0;
    public float xRotation = -5f;
    public float yRotation = 0;
    public float dampeningFactor = 0.01f;
    public float dampeningFactorRotation = 0.5f;
    public bool isTurnAroundModeOn;
    public float turnAroundModeFollowDistance = 30f;
    public float turnAroundModeUpOffset = 21f;
    public float turnAroundModeRightOffset = 0;


    private Vector3 offset;

    private void Start()
    {
        Utilities.utilities.LockMouse();
    }

    private void LateUpdate()
    {
        // kameram oyuncunun pozisyonunun followDistance kaar arkasina olacak
        // kameramin poziyonu + (oyuncumun arka yonu * followDistance)
        if (!isTurnAroundModeOn)
        {
            SmoothMovement(target.position + (-target.forward * followDistance) +
                (target.up * upOffset) + (target.right * rightOffset));

            SmoothRotation(target.rotation * Quaternion.Euler(xRotation, yRotation, 0));
        }
        else
        {
            LookAtTarget();
            SmoothMovement(target.position + (-transform.forward * turnAroundModeFollowDistance) +
                (transform.right * turnAroundModeRightOffset));

        }

    }

    public void SetLookUpDown(float aLookUpDownValue)
    {
        xRotation += aLookUpDownValue;
    }

    private void SmoothMovement(Vector3 aTargetPosition)
    {
        offset += aTargetPosition;
        Vector3 oldPos = transform.position;
        Vector3 newPos = Vector3.LerpUnclamped(oldPos, aTargetPosition, dampeningFactor);
        transform.position = newPos;
        offset -= newPos - oldPos;
    }
    private void SmoothRotation(Quaternion aRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, aRotation, dampeningFactorRotation);
    }
    private void LookAtTarget()
    {
        transform.LookAt(target);
    }

}
