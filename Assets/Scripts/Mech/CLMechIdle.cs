using UnityEngine;
using System.Collections;

public class CLMechIdle : CLAction<CLMech>
{
    public override void Enter()
    {
        //print("idle " + gameObject.name);
        CLAnimation.CrossFade(Actor.Equip.Leg.GetComponent<Animation>(), Actor.Equip.Leg.IdleAnimClip, 0.2f);
        base.Enter();
    }
   
    public override void Execute()
    {
        if (Actor.AttackTargetIsAlive())
        {
            Actor.AimAttackTarget();
            if (mTime > 1f)
            {
                Actor.OnAttackTargetOutOfAttackRange();
                mTime = 0f;
            }
        }
        else
        {
            Actor.OnAttackTargetDie();
        }
        base.Execute();
    }
}