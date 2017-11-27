using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLUIPanelBattle : CLUIPanel
{
    public UISprite WattBar;
    public UISprite HpL;
    public UISprite HpR;
    public UILabel LabelBirthout;
    public UILabel LabelNoEnergy;
    public GameObject MechBloodBarPrefab;
    public GameObject RadarPointPrefab;
    public Transform MechBloodBarRoot;
    public Transform RadarPointRoot00;
    public Transform RadarPointRoot01;
    public Transform RadarPointRoot;
    private Dictionary<CLMech, UISprite> mMechBloodBars = new Dictionary<CLMech,UISprite>();
    private Dictionary<CLMech, UISprite> mRadarPoints = new Dictionary<CLMech, UISprite>();

    protected override void OnOpen()
    {
        LabelBirthout.gameObject.SetActive(false);
        LabelNoEnergy.gameObject.SetActive(false);
        InitRadarPointRoot();
        base.OnOpen();
    }

    void InitRadarPointRoot()
    {
        CLBuilding playerBase = CLGame.Instance.GameBattle.PlayerBase();
        if (playerBase.PlayerID == 0)
        {
            RadarPointRoot = RadarPointRoot00;
        }
        else
        {
            RadarPointRoot = RadarPointRoot01;
        }
      //  print("InitRadarPointRoot: " + playerBase.PlayerID);
    }

    void OnUIMessage(CLUIMessage msg)
    {
        if (msg.Type == CLUIMessageType.OnClick)
        {
            if (msg.Sender.name == "MechBtn0")
            {
                CLGame.Instance.SetCurSlot(0);
                FocusMechBtn();
            }
            else if (msg.Sender.name == "MechBtn1")
            {
                CLGame.Instance.SetCurSlot(1);
                FocusMechBtn();
            }
            else if (msg.Sender.name == "MechBtn2")
            {
                CLGame.Instance.SetCurSlot(2);
                FocusMechBtn();
            }
        }
    }

    void FocusMechBtn()
    {
        CLMech curSlot = CLGame.Instance.CurSlot;
        for (int iSolt = 0; iSolt < CLGame.Instance.PlayerMechSolts.Count; iSolt++)
        {
            CLMech slot = CLGame.Instance.PlayerMechSolts[iSolt];
            string mechSoltBtnBGName = "MechBtn" + iSolt + "/Background";
            UISprite mechBtnSprite = transform.Find(mechSoltBtnBGName).GetComponent<UISprite>();
            if (curSlot == slot)
            {
                mechBtnSprite.color = Color.green;
            }
            else
            {
                mechBtnSprite.color = Color.white;
            }
        }
    }

    void OnEnable()
    {
        for (int iSolt = 0; iSolt < CLGame.Instance.PlayerMechSolts.Count; iSolt++)
        {
            CLMechEquip equip = CLGame.Instance.PlayerMechSolts[iSolt].GetComponent<CLMechEquip>();
            string mechSoltBtnSpriteName = "MechBtn" + iSolt + "/Sprite";
            UISprite mechBtnSprite = transform.Find(mechSoltBtnSpriteName).GetComponent<UISprite>();
            mechBtnSprite.atlas = CLGameUI.Instance.PanelEquip.ListWeapon.transform.Find("ButtonWeapon/Sprite").GetComponent<UISprite>().atlas;
            if (equip.WeaponL.name == "Mech01_Weapon_L")
            {
                mechBtnSprite.spriteName = "Weapon_01";
            }
            else if (equip.WeaponL.name == "Mech02_Weapon_L")
            {
                mechBtnSprite.spriteName = "Weapon_02";
            }
            else if (equip.WeaponL.name == "Mech03_Weapon_L")
            {
                mechBtnSprite.spriteName = "Weapon_03";
            }
        }
        FocusMechBtn();
    }

    void OnDisable()
    {
        ClearMechBloodBar();
        ClearRadar();
    }

    void Update()
    {
        // watt
        WattBar.fillAmount = CLGame.Instance.Watt / CLGame.Instance.WattMax;
        // base blood
        CLBuilding enemyBase = CLGame.Instance.GameBattle.EnemyBase();
        CLBuilding playerBase = CLGame.Instance.GameBattle.PlayerBase();
        UpdateBaseBloodBar(enemyBase, HpR);
        UpdateBaseBloodBar(playerBase, HpL);
        UpdateMechBloodBars();
        UpdateRadarPoints();
    }

    void UpdateMechBloodBars()
    {
        foreach (KeyValuePair<CLMech, UISprite> value in mMechBloodBars)
        {
            CLMech mech = value.Key;
            UISprite bloodBar = value.Value;
            bloodBar.fillAmount = mech.GetComponent<CLHealth>().HealthPoint
                    / mech.GetComponent<CLHealth>().HealthPointMax;
            Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector3 barPosition = camera.WorldToScreenPoint(mech.transform.position);
            bloodBar.transform.localPosition =
                new Vector3(
                    barPosition.x / camera.pixelWidth - 0.5f,
                    barPosition.y / camera.pixelHeight - 0.5f + 0.1f,
                    bloodBar.transform.position.z);
        }
    }

    void UpdateRadarPoints()
    {
        CLMap battleMap = CLGame.Instance.GameBattle.BattleMap;
        float batWidth = battleMap.Width * battleMap.GridWidth;
        float batHeight = battleMap.Height * battleMap.GridWidth;
        Vector3 radarPos = new Vector3();
        foreach (KeyValuePair<CLMech, UISprite> value in mRadarPoints)
        {
            CLMech mech = value.Key;
            UISprite radarPoint = value.Value;
            radarPos.x = mech.transform.position.x / batWidth - 0.5f;
         //     mech.transform.position
            radarPos.y = mech.transform.position.z / batHeight - 0.5f;
            radarPoint.transform.localPosition = radarPos;
        }
    }

    void UpdateBaseBloodBar(CLBuilding mechBase, UISprite hpBar)
    {
        if (mechBase != null)
        {
            CLHealth health = mechBase.GetComponent<CLHealth>();
            hpBar.fillAmount = health.HealthPoint / health.HealthPointMax;
        }
    }

    public void OnNotEnoughWatt()
    {
        CLGameUI.Instance.PanelBattle.LabelBirthout.gameObject.SetActive(false);
        CLGameUI.Instance.PanelBattle.LabelNoEnergy.gameObject.SetActive(true);
    }

    public void OnEnoughWatt()
    {
        LabelBirthout.gameObject.SetActive(false);
    }

    public void OnOutBirth()
    {
       LabelBirthout.gameObject.SetActive(true);
       LabelNoEnergy.gameObject.SetActive(false);
    }

    public UISprite AddMechBloodBar(CLMech mech)
    {
        UISprite newBloodBar = CLGameObject.Instantiate<UISprite>(MechBloodBarPrefab);
        newBloodBar.transform.parent = MechBloodBarRoot;
        newBloodBar.gameObject.SetActive(true);
        mMechBloodBars.Add(mech, newBloodBar);
        return newBloodBar;
    }

    public UISprite AddRadarPoint(CLMech mech)
    {
        UISprite newRadarPoint = CLGameObject.Instantiate<UISprite>(RadarPointPrefab);
        newRadarPoint.transform.parent = RadarPointRoot;
        newRadarPoint.gameObject.SetActive(true);
        newRadarPoint.transform.localScale = RadarPointPrefab.transform.localScale;
        mRadarPoints.Add(mech, newRadarPoint);
        if (mech.IsEnemyMech())
        {
            newRadarPoint.color = Color.red;
        }
        else
        {
            newRadarPoint.color = Color.green;
        }
        return newRadarPoint;
    }

    public void DelMechBloodBar(CLMech mech)
    {
        if (mMechBloodBars.ContainsKey(mech))
        {
            GameObject.Destroy(mMechBloodBars[mech].gameObject);
            mMechBloodBars.Remove(mech);
        }
    }

    public void DelRadarPoint(CLMech mech)
    {
        if (mRadarPoints.ContainsKey(mech))
        {
            GameObject.Destroy(mRadarPoints[mech].gameObject);
            mRadarPoints.Remove(mech);
        }
    }

    public void ClearMechBloodBar()
    {
        foreach (KeyValuePair<CLMech, UISprite> value in mMechBloodBars)
        {
            GameObject.Destroy(value.Value.gameObject);
        }
        mMechBloodBars.Clear();
    }

    public void ClearRadar()
    {
        foreach (KeyValuePair<CLMech, UISprite> value in mRadarPoints)
        {
            GameObject.Destroy(value.Value.gameObject);
        }
        mRadarPoints.Clear();
    }
}