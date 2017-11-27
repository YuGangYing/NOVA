using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ZYSocket;

public class CLGame : CLActor<CLGame>
{
    public bool SingleMode;
    public int PlayerID;
    public int EnemyID;
    public GameObject DefaultMechPrefab;
    public List<CLMech> PlayerMechSolts = new List<CLMech>();
    public List<CLMech> EnemyMechSolts = new List<CLMech>();
    public List<GameObject> LegPrefabs;
    public List<GameObject> BodyPrefabs;
    public List<GameObject> WeaponLPrefabs;
    public List<GameObject> WeaponRPrefabs;
    public CLMech CurSlot;
    public int MechSlotCount = 3;
    public CLGameUI UI;
    public CLGameNet Net;
    public float WattMax = 100;
    public float Watt = 100;
    public float WattIncreaseSpeed = 5;
    public float MechWatt = 20;
    public float AutoAddEnemyMechRate = 2f;
    public AudioClip NotEnoughEnergy;
    public CLGameUpdate GameUpdate;
    public CLGameLogin GameLogin;
    public CLGameEquip GameEquip;
    public CLGameBattle GameBattle;
    public CLGameAccount GameAccount;

    static CLGame sSingletonInstance;

    CLGame()
    {
        mActor = this;
        CLGame.sSingletonInstance = this;
    }

    void Awake()
    {
        GameUpdate = AddAction<CLGameUpdate>();
        GameLogin = AddAction<CLGameLogin>();
        GameEquip = AddAction<CLGameEquip>();
        GameBattle = AddAction<CLGameBattle>();
        GameAccount = AddAction<CLGameAccount>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Action<CLGameUpdate>();
    }

    public static CLGame Instance
    {
        get
        {
            return sSingletonInstance;
        }
    }

    public void OnGameUpdateOver()
    {
        Action<CLGameLogin>();
    }

    public void SetCurSlot(int index)
    {
        CurSlot = PlayerMechSolts[index];
    }
}