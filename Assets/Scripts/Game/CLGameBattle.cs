using UnityEngine;
using System;
using System.Collections;

public class CLGameBattle : CLAction<CLGame>
{
    Vector3 mCurMousePosition;
    CLMap mBattleMap;
    float mAutoAddEnemyMechTime;
    GameObject mBaseFogView00;
    GameObject mBaseFogView01;

    public override void Enter()
    {
        CLGameUI.Instance.PanelLoading.Open();
        Application.LoadLevelAsync("Battle01");
        Actor.CurSlot = Actor.PlayerMechSolts[0];
        Actor.Watt = Actor.WattMax;
        CLAudio.PlayLoop(CLGameAudio.Instance.GetComponent<AudioSource>(), CLGameAudio.Instance.Battle);
        base.Enter();
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mCurMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)
            && (mCurMousePosition - Input.mousePosition).magnitude < 10
            )
        {
            Bounds inputScreenArea = new Bounds(
                new Vector3(Screen.width / 2f, Screen.height / 2f + 0.07f * Screen.height, 0f),
                new Vector3(Screen.width * 0.79f, Screen.height * 0.62f, 0f));
            if (inputScreenArea.Contains(Input.mousePosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 2000f, 1 << 9))
                {
                    CLGame game = CLGame.Instance;
                    CLMechBirthArea birthArea = hitInfo.collider.GetComponent<CLMechBirthArea>();
                    if (birthArea.ID == game.PlayerID)
                    {
                        CLMapGrid birthGrid = mBattleMap.Grid(hitInfo.point);
                        if (birthGrid.Walkable)
                        {
                            int slotIndex = game.PlayerMechSolts.IndexOf(game.CurSlot);
                            if (!CLGame.Instance.SingleMode)
                            {
                                SenPlayerAddMech(slotIndex, birthGrid.X, birthGrid.Y);
                            }
                            if (IsWattEnough(Actor.MechWatt))
                            {
                                AddPlayerMech(birthGrid.X, birthGrid.Y, birthArea.BirthDirection);
                                DecreaseWatt(Actor.MechWatt);
                                CLGameUI.Instance.PanelBattle.OnEnoughWatt();
                            }
                            else
                            {
                                CLGameUI.Instance.PanelBattle.OnNotEnoughWatt();
                                CLAudio.PlayOneShot(CLGameAudio.Instance.GetComponent<AudioSource>(), CLGameAudio.Instance.NotEnoughEnergy);
                            }
                        }
                    }
                    else
                    {
                        CLGameUI.Instance.PanelBattle.OnOutBirth();
                    }

                }
                else
                {
                    CLGameUI.Instance.PanelBattle.OnOutBirth();
                }
            }
        }
    }

    public override void Execute()
    {
        IncreaseWatt();
        HandleInput();
       // UpdateUI();
        if (Actor.SingleMode)
        {
            AutoAddEnemyMech();
        }
        base.Execute();
    }

    public override void Exit()
    {
        CLGameUI.Instance.PanelBattle.Close();
        mAutoAddEnemyMechTime = 0f;
        if (GameObject.Find("BaseFogView00") != null)
        {
            GameObject.Find("BaseFogView00").SetActive(true);
        }
        if (GameObject.Find("BaseFogView01") != null)
        {
            GameObject.Find("BaseFogView01").SetActive(true);
        }
        base.Exit();
    }

    public override void OnAddAction()
    {
        Actor.Net.AddRecHandler('3', new RecHandler(RecEnemyAddMech));
        Actor.Net.AddRecHandler('4', new RecHandler(RecAccount));
        Actor.Net.AddRecHandler('5', new RecHandler(RecExitBattle));
        base.OnAddAction();
    }

    public void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == "Battle01")
        {
            InitBattleCameras();
            InitBaseFogViews();
            Actor.UI.PanelLoading.Close();
            Actor.UI.PanelBattle.Open();
            mBattleMap = GameObject.Find("Map").GetComponent<CLMap>();
        }
    }

    public void InitBaseFogViews()
    {
        mBaseFogView00 = GameObject.Find("BaseFogView00");
        mBaseFogView01 = GameObject.Find("BaseFogView01");
        if (Actor.PlayerID == 0)
        {
            mBaseFogView00.SetActive(true);
            mBaseFogView01.SetActive(false);
        }
        else
        {
            mBaseFogView01.SetActive(true);
            mBaseFogView00.SetActive(false);
        }
    }

    public void InitBattleCameras()
    {
        foreach (GameObject cameraObj in GameObject.FindGameObjectsWithTag("BattleCamera"))
        {
            CLCameraControl camera = cameraObj.GetComponent<CLCameraControl>();
            if (camera.PlayerID == Actor.PlayerID)
            {
                camera.ZoomPoint.gameObject.SetActive(true);
            }
            else
            {
                camera.ZoomPoint.gameObject.SetActive(false);
            }
        }
    }

    public bool IsEnemyBase(GameObject building)
    {
        return building.GetComponent<CLBuilding>().PlayerID != Actor.PlayerID;
    }

    public bool IsPlayerBase(GameObject building)
    {
        return building.GetComponent<CLBuilding>().PlayerID == Actor.PlayerID;
    }

    public bool IsEnemyWin()
    {
        return PlayerBase().GetComponent<CLHealth>().IsDead();
    }

    public bool IsPlayerWin()
    {
        return EnemyBase().GetComponent<CLHealth>().IsDead();
    }

    public void IncreaseWatt()
    {
        Actor.Watt = Mathf.Clamp(
            Actor.Watt + Time.deltaTime * Actor.WattIncreaseSpeed,
            0f, Actor.WattMax);
        if (Actor.Watt > Actor.MechWatt
            &&  CLGameUI.Instance.PanelBattle.LabelNoEnergy.gameObject.activeSelf
            )
        {
            CLGameUI.Instance.PanelBattle.LabelNoEnergy.gameObject.SetActive(false);
        }
    }

    public void DecreaseWatt(float watt)
    {
        Actor.Watt = Mathf.Clamp(
          Actor.Watt - watt,
          0f, Actor.WattMax);
    }

    public bool IsWattEnough(float watt)
    {
        return Actor.Watt >= watt;
    }

    public bool WattIsEmpty()
    {
        return Actor.Watt <= 0f;
    }

    public void AutoAddEnemyMech()
    {
        if (mTime - mAutoAddEnemyMechTime >Actor.AutoAddEnemyMechRate)
        {
            Vector3 birthPoint = GameObject.Find("BirthArea01").transform.position;
            CLMapGrid birthGrid = mBattleMap.Grid(birthPoint);
            AddEnemyMech(UnityEngine.Random.Range(0, 3), birthGrid.X, birthGrid.Y);
            mAutoAddEnemyMechTime = mTime;
        }
    }

    public void AddEnemyMech(int slot, int x, int y)
    {
        GameObject MechRoot = GameObject.Find("MechRoot");
        if (MechRoot == null)
        {
            MechRoot = new GameObject("MechRoot");
        }
        GameObject newMech = CLGameObject.Instantiate(CLGame.Instance.EnemyMechSolts[slot].gameObject);
        newMech.transform.parent = MechRoot.transform;
        newMech.transform.position = mBattleMap.Grid(x, y).Center;
        newMech.GetComponent<CLMech>().PlayerID = Actor.EnemyID;
        newMech.transform.forward = EnemyBirthArea().BirthDirection;
        newMech.SetActive(true);
        newMech.name = "Mech" + newMech.GetInstanceID();
        newMech.SendMessage("OnEnterBattle", SendMessageOptions.DontRequireReceiver);
        //GameObject radarPoint = CLGameObject.Instantiate("RadarPoint");
        //radarPoint.transform.parent = newMech.transform;
        //radarPoint.transform.localPosition = Vector3.zero;
        //radarPoint.renderer.material.SetColor("_TintColor", Color.red);
    }

    public void AddPlayerMech(int x, int y, Vector3 direction)
    {
        GameObject MechRoot = GameObject.Find("MechRoot");
        if (MechRoot == null)
        {
            MechRoot = new GameObject("MechRoot");
        }
        CLMap battleMap = GameObject.Find("Map").GetComponent<CLMap>();
        GameObject newMech = CLGameObject.Instantiate(CLGame.Instance.CurSlot.gameObject);
        newMech.transform.parent = MechRoot.transform;
        newMech.transform.position = battleMap.Grid(x, y).Center;
        newMech.GetComponent<CLMech>().PlayerID = Actor.PlayerID;
        newMech.transform.forward = direction;
        newMech.SetActive(true);
        newMech.name = "Mech" + newMech.GetInstanceID();
        newMech.SendMessage("OnEnterBattle", SendMessageOptions.DontRequireReceiver);
        GameObject fogView = CLGameObject.Instantiate("FogView");
        fogView.transform.parent = newMech.transform;
        fogView.transform.localPosition = Vector3.zero;
        //GameObject radarPoint = CLGameObject.Instantiate("RadarPoint");
        //radarPoint.transform.parent = newMech.transform;
        //radarPoint.transform.localPosition = Vector3.zero;
        //radarPoint.renderer.material.SetColor("_TintColor", Color.green);

    }

    public CLBuilding PlayerBase()
    {
        foreach (GameObject mechBase in GameObject.FindGameObjectsWithTag("Base"))
        {
            if (mechBase.GetComponent<CLBuilding>().PlayerID == Actor.PlayerID)
            {
                return mechBase.GetComponent<CLBuilding>();
            }
        }
        return null;
    }

    public CLBuilding EnemyBase()
    {
        foreach (GameObject mechBase in GameObject.FindGameObjectsWithTag("Base"))
        {
            if (mechBase.GetComponent<CLBuilding>().PlayerID != Actor.PlayerID)
            {
                return mechBase.GetComponent<CLBuilding>();
            }
        }
        return null;
    }

    public CLMechBirthArea EnemyBirthArea()
    {
        foreach (GameObject mechBase in GameObject.FindGameObjectsWithTag("BirthArea"))
        {
            if (mechBase.GetComponent<CLMechBirthArea>().ID != Actor.PlayerID)
            {
                return mechBase.GetComponent<CLMechBirthArea>();
            }
        }
        return null;
    }

    public void SenPlayerAddMech(int slot, int x, int y)
    {
        string stream = "";
        NewMechPkg newMechPkg = new NewMechPkg();
        newMechPkg.MechSlot = (UInt16)slot;
        newMechPkg.MapX = (UInt16)x;
        newMechPkg.MapY = (UInt16)y;
        newMechPkg.Write(ref stream);
        CLGame.Instance.Net.zysocket.SendMessage(stream);
        print("SenPlayerAddMech");
    }

    public void RecEnemyAddMech(string stream)
    {
        NewMechPkg addMechPkg = new NewMechPkg();
        addMechPkg.Read(ref stream);
        AddEnemyMech(addMechPkg.MechSlot, addMechPkg.MapX, addMechPkg.MapY);
        print("RecEnemyAddMech");
    }

    public void SenAccount()
    {
        string stream = "";
        AccountPkg accountPkg = new AccountPkg();
        accountPkg.Write(ref stream);
        CLGame.Instance.Net.zysocket.SendMessage(stream);
        print("SenAccount");
    }

    public void RecAccount(string stream)
    {
        print("RecAccount");

    }

    public void SenExitBattle()
    {
        string stream = "";
        ExitBattlePkg exitBattlePkg = new ExitBattlePkg();
        exitBattlePkg.Write(ref stream);
        CLGame.Instance.Net.zysocket.SendMessage(stream);
        print("SenExitBattle");
    }

    public void RecExitBattle(string stream)
    {
        CLGame.Instance.Action<CLGameEquip>();
        print("RecExitBattle");
    }

    public CLMap BattleMap
    {
        get
        {
            return mBattleMap;
        }
    }
}