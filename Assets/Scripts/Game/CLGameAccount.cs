using UnityEngine;
using System.Collections;

public class CLGameAccount : CLAction<CLGame>
{
    public override void Enter()
    {
        CLGameUI.Instance.PanelAccount.Open();
        AllMechStand();
        if (!CLGame.Instance.SingleMode)
        {
            Actor.GetComponent<CLGameBattle>().SenAccount();
        }
        base.Enter();
    }

    public override void Exit()
    {
        CLGameUI.Instance.PanelAccount.Close();
        base.Exit();
    }

    public void AllMechStand()
    {
        foreach (GameObject mech in GameObject.FindGameObjectsWithTag("Mech"))
        {
            mech.GetComponent<CLMech>().StopFire();
            mech.GetComponent<CLMech>().Action<CLMechStand>();
        }
    }
}