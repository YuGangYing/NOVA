using UnityEngine;
using System.Collections;

public class CLRotateTo : MonoBehaviour
{
    public float Speed = 10;
    Quaternion mTargetRotation;

    public void RotateToTarget(GameObject target)
    {
        RotateToTarget(target.transform.position);
    }

    public void RotateToTarget(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        RotateTo(targetDirection);
    }

    public void RotateToTargetHorizontality(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection.y = 0; // Y up
        RotateTo(targetDirection);
    }

    public void RotateTo(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            mTargetRotation = Quaternion.LookRotation(direction);
            float rotateAngle = Time.deltaTime * Speed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mTargetRotation, rotateAngle);
        }
    }

    public bool TowardTarget()
    {
        return mTargetRotation == transform.rotation;
    }
}
