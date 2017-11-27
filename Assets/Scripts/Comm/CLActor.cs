using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("CL/CLActor")]
public class CLActor<ActorT> : MonoBehaviour
{
    protected ActorT mActor;
    private CLAction<ActorT> mCurAction;
    private CLAction<ActorT> mPreAction;

    protected void Update()
    {
        if (mCurAction != null)
        {
            mCurAction.Execute();
        }
    }

    protected ActionT AddAction<ActionT>() where ActionT : CLAction<ActorT>
    {
        ActionT newAction = CLGameObject.AddComponent<ActionT>(gameObject);
        newAction.Actor = mActor;
        newAction.OnAddAction();
        return newAction;
    }

    public void Action<ActionT>() where ActionT : CLAction<ActorT>
    {
        Action(GetComponent<ActionT>());
    }

    public void Action(CLAction<ActorT> newAction)
    {
        if (newAction != null)
        {
            if (mCurAction != null)
            {
                mCurAction.Exit();
            }
            mCurAction = newAction;
            mCurAction.Enter();
        }
    }

    public bool IsInAction<ActionT>() where ActionT : CLAction<ActorT>
    {
        return IsInAction(GetComponent<ActionT>());
    }

    public bool IsInAction(CLAction<ActorT> action)
    {
        return mCurAction == action;
    }

    public CLAction<ActorT> CurrentAction()
    {
        return mCurAction;
    }
}
