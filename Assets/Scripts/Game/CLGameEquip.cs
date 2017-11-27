using UnityEngine;
using System;
using System.Collections;

public class CLGameEquip : CLAction<CLGame>
{
    public override void Enter()
    {
        CLGameUI.Instance.PanelLoading.Open();
        InitPlayerMechSlots();
        if (Actor.SingleMode)
        {
            InitEnemyMechSlots();
        }
        Application.LoadLevelAsync("Equipment");
        CLAudio.PlayLoop(CLGameAudio.Instance.GetComponent<AudioSource>(), CLGameAudio.Instance.Equip);
        base.Enter();
    }

    public override void Exit()
    {
        CLGameUI.Instance.PanelEquip.Close();
        base.Exit();
    }

    public override void OnAddAction()
    {
        Actor.Net.AddRecHandler('1', new RecHandler(RecEnemyReady));
        Actor.Net.AddRecHandler('2', new RecHandler(RecEnemyEnterBattle));
        base.OnAddAction();
    }

    public void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == "Equipment")
        {
            CLGameUI.Instance.PanelLoading.Close();
            CLGameUI.Instance.PanelEquip.Open();
        }
    }

    public GameObject InitMech(int legID, int bodyID, int weaponID)
    {
        GameObject newMechObj = new GameObject();
        newMechObj.name = "Mech";
        CLMechEquip equip = newMechObj.AddComponent<CLMechEquip>();
        equip.Leg = CLGameObject.Instantiate(Actor.LegPrefabs[legID]).GetComponent<CLMechLeg>();
        equip.Body = CLGameObject.Instantiate(Actor.BodyPrefabs[bodyID]).GetComponent<CLMechBody>();
        equip.WeaponL = CLGameObject.Instantiate(Actor.WeaponLPrefabs[weaponID]).GetComponent<CLMechWeapon>();
        equip.WeaponR = CLGameObject.Instantiate(Actor.WeaponRPrefabs[weaponID]).GetComponent<CLMechWeapon>();
        equip.Assemble();
        CLMech mech = newMechObj.AddComponent<CLMech>();
        mech.PlayerID = Actor.PlayerID;
        mech.Equip = equip;
        CLHealth health = newMechObj.AddComponent<CLHealth>();
        health.HealthPoint = 100;
        health.HealthPointMax = 100;
        DontDestroyOnLoad(newMechObj);
        newMechObj.SetActive(false);
        return newMechObj;
    }

    public void InitPlayerMechSlots()
    {
        if (Actor.PlayerMechSolts.Count == 0)
        {
            for (int iMech = 0; iMech < Actor.MechSlotCount; iMech++)
            {
                Actor.PlayerMechSolts.Add(InitMech(iMech, iMech, iMech).GetComponent<CLMech>());
            }
        }
    }

    public void InitEnemyMechSlots()
    {
        if (Actor.EnemyMechSolts.Count == 0)
        {
            for (int iMech = 0; iMech < Actor.MechSlotCount; iMech++)
            {
                Actor.EnemyMechSolts.Add(InitMech(iMech, iMech, iMech).GetComponent<CLMech>());
            }
        }
    }

    public void SenPlayerReady()
    {
        string stream = "";
        ReadyPkg readyPkg = new ReadyPkg();
        for (int iSlot = 0; iSlot < 3; iSlot++)
        {
            readyPkg.LegID[iSlot] = (UInt16)Actor.PlayerMechSolts[iSlot].Equip.Leg.PrefabID;
            readyPkg.BodyID[iSlot] = (UInt16)Actor.PlayerMechSolts[iSlot].Equip.Body.PrefabID;
            readyPkg.WeaponID[iSlot] = (UInt16)Actor.PlayerMechSolts[iSlot].Equip.WeaponL.PrefabID;
        }
        readyPkg.Write(ref stream);
        Actor.Net.zysocket.SendMessage(stream);
        print("SenPlayerReady");
    }

    public void RecEnemyReady(string stream)
    {
        print("RecEnemyReady");
    }

    public void SenPlayerEnterBattle()
    {
        string stream = "";
        BattleStartPkg battleStartPkg = new BattleStartPkg();
        battleStartPkg.Write(ref stream);
        Actor.Net.zysocket.SendMessage(stream);
        print("SenPlayerEnterBattle");
    }

    public void RecEnemyEnterBattle(string stream)
    {
        CLGame game = CLGame.Instance;
        foreach (CLMech enemySlot in game.EnemyMechSolts)
        {
            Destroy(enemySlot.gameObject);
        }
        game.EnemyMechSolts.Clear();
        BattleStartPkg battleStartPkg = new BattleStartPkg();
        battleStartPkg.Read(ref stream);
        for (int iSlot = 0; iSlot < 3; iSlot++)
        {
            game.EnemyMechSolts.Add(
                InitMech(
                battleStartPkg.LegID[iSlot],
                battleStartPkg.BodyID[iSlot],
                battleStartPkg.WeaponID[iSlot]).GetComponent<CLMech>());
        }
        CLGame.Instance.Action<CLGameBattle>();
        print("RecEnemyEnterBattle");
    }
}