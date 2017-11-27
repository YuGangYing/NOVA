using UnityEngine;
using System.Collections;

public class CLMove : MonoBehaviour
{
    //public float MoveSpeed = 5;
    //public float RotateSpeed = 10;

    //public void MoveToTarget(Vector3 targetPosition)
    //{
    //    Vector3 path = targetPosition - transform.position;
    //    MovePath(path);
    //}

    //public void MoveToTargetIgnoreY(Vector3 targetPosition)
    //{
    //    Vector3 path = targetPosition - transform.position;
    //    path.y = 0;
    //    MovePath(path);
    //}

    //public void MovePath(Vector3 path)
    //{
    //    Vector3 moveDirection = Vector3.Normalize(path);
    //    float moveDistance = MoveSpeed * Time.deltaTime;
    //    Vector3 motion = (moveDistance < path.magnitude) ? moveDirection * moveDistance : path;
    //    Move(motion);
    //    if (motion == Vector3.zero)
    //    {
    //        SendMessage("OnMoveArriveTarget", SendMessageOptions.DontRequireReceiver);
    //    }
    //}

    //public void Move(Vector3 motion)
    //{
    //    transform.Translate(motion, Space.World);
    //}

    //public void RotateToTarget(GameObject target)
    //{
    //    RotateToTarget(target.transform.position);
    //}

    //public void RotateToTarget(Vector3 targetPosition)
    //{
    //    Vector3 targetDirection = targetPosition - transform.position;
    //    RotateTo(targetDirection);
    //}

    //public void RotateToTargetYUp(Vector3 targetPosition)
    //{
    //    Vector3 targetDirection = targetPosition - transform.position;
    //    targetDirection.y = 0; // Y up
    //    RotateTo(targetDirection);
    //}

    //public void RotateTo(Vector3 direction)
    //{
    //    if (direction != Vector3.zero)
    //    {
    //        Quaternion rotateTarget = Quaternion.LookRotation(direction);
    //        float rotateAngle = Time.deltaTime * RotateSpeed;
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget, rotateAngle);
    //    }
    //}
}