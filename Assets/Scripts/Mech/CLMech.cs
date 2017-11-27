using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLMech : CLActor<CLMech>
{
    public float MoveSpeed = 20;
    public float RotateSpeed = 720;
    public int PlayerID;
    public CLMechEquip Equip;
    public CLPathMove PathMove;
    public CLMap BattleMap;
    public GameObject AttackTarget;
    public GameObject ChaseTarget;
    public GameObject EnemyBase;
    public GameObject EnemyMech;
    public GameObject DestroyEffectPrefab;
    

    CLMech()
    {
        mActor = this;    
    }

    void Awake()
    {
        AddAction<CLMechIdle>();
        AddAction<CLMechChase>();
        AddAction<CLMechAttack>();
        AddAction<CLMechDestroy>();
        AddAction<CLMechStand>();
        PathMove = CLGameObject.AddComponent<CLPathMove>(gameObject);
        tag = "Mech";
    }

    void OnEnterEquip()
    {
        Action<CLMechStand>();
    }

    void OnEnterBattle()
    {
        BattleMap = GameObject.Find("Map").GetComponent<CLMap>();
        SearchEnemyMech();
        SearchEnemyBase();
        if (EnemyMech != null)
        {
            AttackTarget = EnemyMech;
            Action<CLMechAttack>();
        }
        else
        {
            AttackTarget = EnemyBase;
            ChaseTarget = EnemyBase;
            Action<CLMechChase>();
        }
        CLGameUI.Instance.PanelBattle.AddMechBloodBar(this);
        CLGameUI.Instance.PanelBattle.AddRadarPoint(this);
        
    }

    public void OnAttackTargetInAttackRange()
    {
        Action<CLMechAttack>();
    }

    public void OnAttackTargetOutOfAttackRange()
    {
        ChaseTarget = AttackTarget;
        Action<CLMechChase>();
    }

    public void OnAttackTargetDie()
    {
        AttackTarget = null;
        SearchEnemyMech();
        if (EnemyMech != null)
        {
            AttackTarget = EnemyMech;
            Action<CLMechAttack>();
        }
        else
        {
            AttackTarget = EnemyBase;
            ChaseTarget = EnemyBase;
            Action<CLMechChase>();
        }
    }

    void OnHealthDie()
    {
        Action<CLMechDestroy>();
    }

    public void OnChaseNoPath()
    {
        //print("OnNoPath " + gameObject.name );
        Action<CLMechIdle>();
    }

    public void AimAttackTarget()
    {
        Equip.Body.RotateTo.RotateToTargetHorizontality(AttackTarget.transform.position);
    }

    public bool AttackTargetIsAlive()
    {
        return AttackTarget != null && AttackTarget.GetComponent<CLHealth>().IsAlive();
    }

    public bool AttackTargetInAttackRange()
    {
        return Equip.WeaponL.AttackRange.IsTargetIn(gameObject, AttackTarget, transform.forward, 0f);
    }

    public void SearchEnemyMech()
    {
        foreach (GameObject mech in GameObject.FindGameObjectsWithTag("Mech"))
        {
            if (IsEnemyMech(mech)
                && mech.GetComponent<CLHealth>().IsAlive()
                && Equip.WeaponL.AttackRange.IsTargetIn(gameObject, mech, transform.forward, 0f)
                )
            {
                EnemyMech = mech;
                return;
            }
        }
        EnemyMech = null;
    }

    public void SearchEnemyBase()
    {
        foreach (GameObject mechBase in GameObject.FindGameObjectsWithTag("Base"))
        {
            if (mechBase.GetComponent<CLBuilding>().PlayerID != PlayerID)
            {
                EnemyBase = mechBase;
                return;
            }
        }
        EnemyBase = null;
    }

    public List<CLMapGrid> SearchPathToComradeInAttack()
    {
        List<CLMapGrid> path = null;
        foreach (GameObject mech in GameObject.FindGameObjectsWithTag("Mech"))
        {
            if (IsComradeMech(mech)
                && mech.GetComponent<CLMech>().IsInAction<CLMechAttack>()
                )
            {
                path = BattleMap.FindPathIgnoreStartEnd(transform, mech.transform);
                if (path != null)
                {
                    return path;
                }
            }
        }
        return path;
    }

    bool IsEnemyMech(GameObject mech)
    {
        return mech.GetComponent<CLMech>().PlayerID != PlayerID;
    }

    bool IsComradeMech(GameObject mech)
    {
        return mech.GetComponent<CLMech>().PlayerID == PlayerID;
    }

    public bool IsEnemyMech()
    {
        return CLGame.Instance.PlayerID != PlayerID;
    }

    public bool IsPlayerMech()
    {
        return CLGame.Instance.PlayerID == PlayerID;
    }

    public bool AttackTargetIsMech()
    {
        return AttackTarget == EnemyMech;
    }

    public bool AttackTargetIsBase()
    {
        return AttackTarget == EnemyBase;
    }

    public void OpenFire()
    {
        //Equip.WeaponL.Action<CLMechWeaponFire>();
        //Equip.WeaponR.Action<CLMechWeaponFire>();
        Equip.WeaponL.OpenFire(AttackTarget, gameObject);
        Equip.WeaponR.OpenFire(AttackTarget, gameObject);
    }

    public void StopFire()
    {
        //Equip.WeaponL.Action<CLMechWeaponIdle>();
        //Equip.WeaponR.Action<CLMechWeaponIdle>();
        Equip.WeaponL.StopFire();
        Equip.WeaponR.StopFire();
    }
   
}