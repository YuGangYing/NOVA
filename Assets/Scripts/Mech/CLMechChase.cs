using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLMechChase : CLAction<CLMech>
{
    List<CLMapGrid> mPath;

    public override void Enter()
    {
        mPath = Actor.BattleMap.FindPathIgnoreStartEnd(transform, Actor.AttackTarget.transform);
        if (mPath != null)
        {
            Actor.PathMove.StartMove(mPath);
            CLAnimation.CrossFade(Actor.Equip.Leg.GetComponent<Animation>(), Actor.Equip.Leg.RunAnimClip, 0.2f);
        }
        else
        {
            Actor.OnChaseNoPath();
        }
        base.Enter();
    }

    public override void Execute()
    {
        Actor.PathMove.Move();
        Actor.AimAttackTarget();
       // actor.Equip.Body.RotateTo.RotateToTargetHorizontality(actor.AttackTarget.transform.position);
        Actor.PathMove.MoveSpeed = Actor.MoveSpeed;
        Actor.PathMove.RotateSpeed = Actor.RotateSpeed;
    }

    public override void Exit()
    {
        Actor.PathMove.StopMove();
        base.Exit();
    }

    void OnReachCurGrid()
    {
        if (IsInAction)
        {
            if (Actor.AttackTargetIsAlive())
            {
                if (Actor.AttackTargetInAttackRange())
                {
                    Actor.PathMove.StopMove();
                    Actor.OnAttackTargetInAttackRange();
                }
                else
                {
                    if (Actor.AttackTargetIsBase())
                    {
                        Actor.SearchEnemyMech();
                        if (Actor.EnemyMech != null)
                        {
                            Actor.AttackTarget = Actor.EnemyMech;
                            Actor.PathMove.StopMove();
                            Actor.OnAttackTargetInAttackRange();
                        }
                    }
                }
            }
            else
            {
                Actor.OnAttackTargetDie();
            }
        }
    }

    void OnUpdateCurGrid(int curGridIndex)
    {
        if (mPath[curGridIndex].Walkable)
        {
            mPath[curGridIndex].Walkable = false;
            int oldGridIndex = curGridIndex - 1;
            mPath[oldGridIndex].Walkable = true;
        }
        else
        {
            OnPathDirty(curGridIndex);
        }
    }

    void OnPathDirty(int curGridIndex)
    {
        int oldGridIndex = curGridIndex - 1;
        CLMapGrid desGrid = Actor.BattleMap.Grid(Actor.AttackTarget.transform.position);
        CLMapGrid curGrid = mPath[oldGridIndex];
        mPath = Actor.BattleMap.FindPathIgnoreStartEnd(curGrid, desGrid);
        if (mPath != null)
        {
            Actor.PathMove.StartMove(mPath);
        }
        else
        {
            Actor.OnChaseNoPath();
        }
    }

    void OnHealthDie()
    {
        if (mPath != null)
        {
            mPath[Actor.PathMove.CurGridIndex()].Walkable = true;
        }
    }
}