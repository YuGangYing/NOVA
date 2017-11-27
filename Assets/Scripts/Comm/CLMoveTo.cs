using UnityEngine;
using System.Collections;

public class CLMoveTo : MonoBehaviour
{
    public float Gravity = 0;
    public float Speed = 5;
    private Vector3 mPath;
    bool mReachTarget;

    public void MoveToTarget(Vector3 targetPosition)
    {
        mPath = targetPosition - transform.position;
        MovePath(mPath);
    }

    public void MoveToTargetHorizontality(Vector3 targetPosition)
    {
        mPath = targetPosition - transform.position;
        mPath.y = 0;
        MovePath(mPath);
    }

    private void MovePath(Vector3 path)
    {
        Vector3 moveDirection = Vector3.Normalize(path);
        float moveDistance = Speed * Time.deltaTime;
        Vector3 motion;
        if(moveDistance < path.magnitude) 
        {
            motion = moveDirection * moveDistance;
            mReachTarget = false;
        }else
        {
            motion = path;
            mReachTarget = true;
        }
        motion.y -= Gravity * Time.deltaTime;// apply gravity
        transform.Translate(motion, Space.World);
    }

    public bool ReachTarget()
    {
        return mReachTarget;
    }
}