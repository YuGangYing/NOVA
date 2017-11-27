using UnityEngine;
using System.Collections;

public class CLMechWeaponIdle : CLAction<CLMechWeapon>
{
    public override void Enter()
    {
        //if (actor.User)
        //{
        //    print(actor.User.name + " EnterWeaponidle");
        //}
        Actor.HideFireEffect();
        base.Enter();
    }
}