using UnityEngine;
using System.Collections;

public class CLGameUpdate : CLAction<CLGame>
{
    public float UpdateProgress;

    public override void Enter()
    {
        CLGameUI.Instance.PanelUpdate.Open();
        UpdateProgress = 0f;
        base.Enter();
    }

    public override void Execute()
    {
        UpdateProgress = mTime;
        if (UpdateProgress >= 1)
        {
            Actor.OnGameUpdateOver();
        }
        base.Execute();
    }

    public override void Exit()
    {
        CLGameUI.Instance.PanelUpdate.Close();
        base.Exit();
    }
}
