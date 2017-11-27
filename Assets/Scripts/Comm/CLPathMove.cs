using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLPathMove : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float RotateSpeed = 10;
    private List<CLMapGrid> mPath;
    private int mCurGridIndex = 0;
    private bool mCanMove = true;
    private CLMoveRotateTo mMoveRotateTo;

    void Awake()
    {
        mMoveRotateTo = CLGameObject.AddComponent<CLMoveRotateTo>(gameObject);
    }

    void Update()
    {
        mMoveRotateTo.Move.Speed = MoveSpeed;
        mMoveRotateTo.Rotate.Speed = RotateSpeed;
    }

    public void StopMove()
    {
        mCanMove = false;
    }

    public void StartMove(List<CLMapGrid> newPath)
    {
        mPath = newPath;
        mCurGridIndex = 0;
        mCanMove = true;
    }

    void MoveToCurGrid()
    {
        CLMapGrid curGrid = mPath[mCurGridIndex];
        mMoveRotateTo.MoveRotateToTargetHorizontality(curGrid.Center);
    }

    bool ReachCurGrid()
    {
        return mMoveRotateTo.ReachTarget();
    }

    bool ArriveDestination()
    {
        return mCurGridIndex == (mPath.Count - 1);
    }

    bool IsPathAvailable()
    {
        return (mPath != null) && (mPath.Count > 0);
    }

    void UpdateCurGrid()
    {
        mCurGridIndex++;
    }

    public void Move()
    {
        if (IsPathAvailable())
        {
            MoveToCurGrid();
            if (ReachCurGrid())
            {
                SendMessage("OnReachCurGrid", SendMessageOptions.DontRequireReceiver);
                if (mCanMove)
                {
                    if (ArriveDestination())
                    {
                        SendMessage("OnArriveDestination", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        UpdateCurGrid();
                        SendMessage("OnUpdateCurGrid", mCurGridIndex, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    public int CurGridIndex()
    {
        return mCurGridIndex;
    }
}