using UnityEngine;
using System.Collections;

public abstract class CLAction<T> : MonoBehaviour
{
    public bool IsInAction;
    protected float mTime;
    public T Actor;

    public virtual void Enter()
    {
        IsInAction = true;
        mTime = 0f;
    }

    public virtual void Execute()
    {
        mTime += Time.deltaTime;
    }

    public virtual void Exit()
    {
        IsInAction = false;
    }

    public virtual void OnAddAction()
    {
    }
}