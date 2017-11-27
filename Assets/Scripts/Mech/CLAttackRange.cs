using UnityEngine;
using System.Collections;

[System.Serializable]
public class CLAttackRange
{
    public float Radius;
    public float Angle;
    public bool IsTargetIn(GameObject attacker, GameObject target, Vector3 direction, float targetRadius)
    {
        Vector3 distance = target.transform.position - attacker.transform.position;
        float angle = Vector3.Angle(direction, distance);
        return targetRadius + Radius > distance.magnitude
            && Angle > angle;
    }

    public bool IsTargetInAngle(GameObject attacker, GameObject target, Vector3 direction)
    {
        Vector3 distance = target.transform.position - attacker.transform.position;
        float angle = Vector3.Angle(direction, distance);
        return Angle > angle;
    }
}