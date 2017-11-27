using UnityEngine;
using System.Collections;

public class CLMechWeaponCool : CLAction<CLMechWeapon>
{
    public override void Enter()
    {
        //print(actor.User.name + " EnterWeaponcool");
        base.Enter();
    }

    public override void Execute()
    {
        if (mTime > Actor.CoolTime)
        {
            Actor.Action<CLMechWeaponFire>();
        }
        base.Execute();
    }
}