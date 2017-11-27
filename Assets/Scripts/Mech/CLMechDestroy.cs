using UnityEngine;
using System.Collections;

public class CLMechDestroy : CLAction<CLMech>
{
    public override void Enter()
    {
        Actor.PathMove.StopMove();
        CLGameObject.Instantiate("ExplosionMech", Actor.Equip.Body.transform);
        GameObject wreck = CLGameObject.Instantiate("mechwreck", transform);
        wreck.transform.Translate(new Vector3(0, 1.3f, 0));
        gameObject.AddComponent<CLAutoDestroy>().LifeTime = 2f;
        Actor.Equip.Leg.gameObject.SetActive(false);
        CLGameUI.Instance.PanelBattle.DelMechBloodBar(Actor);
        CLGameUI.Instance.PanelBattle.DelRadarPoint(Actor);
        base.Enter();
    }
}