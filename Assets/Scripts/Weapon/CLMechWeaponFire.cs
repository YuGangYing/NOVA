using UnityEngine;
using System.Collections;

public class CLMechWeaponFire : CLAction<CLMechWeapon>
{
    public override void Enter()
    {
        // print(actor.User.name + " EnterWeaponFire");
        Actor.ShowFireEffect();
        CLAudio.Play(Actor.GetComponent<AudioSource>(), Actor.FireClip);
        Actor.ShotAmmo();
        base.Enter();
    }

    public override void Execute()
    {
        if (mTime > Actor.FireTime)
        {
            Actor.Action<CLMechWeaponCool>();
        }
        base.Execute();
    }

    public override void Exit()
    {
        Actor.HideFireEffect();
        CLAudio.Stop(Actor.GetComponent<AudioSource>());
        base.Exit();
    }
}