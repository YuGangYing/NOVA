using UnityEngine;
using System.Collections;

public class CLMechStand : CLAction<CLMech>
{
    public override void Enter()
    {
        CLAnimation.CrossFade(
            Actor.Equip.Leg.GetComponent<Animation>(),
            Actor.Equip.Leg.IdleAnimClip,
            0.2f);
        base.Enter();
    }

    public override void Execute()
    {
        Actor.Equip.Body.RotateTo.RotateToTargetHorizontality(transform.position + transform.forward);
    }
}