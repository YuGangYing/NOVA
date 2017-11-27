using UnityEngine;
using System.Collections;

public class CLMoveRotateTo : MonoBehaviour
{
    public CLMoveTo Move;
    public CLRotateTo Rotate;

    void Awake()
    {
        Move = CLGameObject.AddComponent<CLMoveTo>(gameObject);
        Rotate = CLGameObject.AddComponent<CLRotateTo>(gameObject);
    }

    public void MoveRotateToTarget(Vector3 targetPosition)
    {
        Move.MoveToTarget(targetPosition);
        Rotate.RotateToTarget(targetPosition);
    }

    public void MoveRotateToTargetHorizontality(Vector3 targetPosition)
    {
        Move.MoveToTargetHorizontality(targetPosition);
        Rotate.RotateToTargetHorizontality(targetPosition);
    }

    public bool ReachTarget()
    {
        return Move.ReachTarget();
    }
}