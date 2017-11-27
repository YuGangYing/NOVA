using UnityEngine;
using System.Collections;

public class CLMechAttack : CLAction<CLMech>
{
    public override void Enter()
    {
        //print(actor.name + " enter attack");
        Actor.OpenFire();
        CLAnimation.CrossFade(Actor.Equip.Leg.GetComponent<Animation>(), Actor.Equip.Leg.IdleAnimClip, 0.2f);
    }

    public override void Execute()
    {
        if (Actor.AttackTargetIsAlive())
        {
            if (Actor.AttackTargetInAttackRange())
            {
                Actor.AimAttackTarget();
            }
            else
            {
                Actor.OnAttackTargetOutOfAttackRange();
            }
        }
        else
        {
            Actor.OnAttackTargetDie();            
        }
    }

    public override void Exit()
    {
       // print(actor.name + " exit attack");
        Actor.StopFire();
    }
}