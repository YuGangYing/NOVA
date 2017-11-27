using UnityEngine;
using System.Collections;

public class CLUIPanelEquip : CLUIPanel
{
    public GameObject ListLeg;
    public GameObject ListBody;
    public GameObject ListWeapon;
    public GameObject ListPro;
    private CLMech mCurEditMech;

    void OnEnable()
    {
        EnableListLeg();
        mCurEditMech = CLGame.Instance.PlayerMechSolts[0];
        mCurEditMech.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        if (mCurEditMech != null)
        {
            mCurEditMech.gameObject.SetActive(false);
            mCurEditMech = CLGame.Instance.PlayerMechSolts[0];
        }
    }

    void OnUIMessage(CLUIMessage msg)
    {
        if (msg.Type == CLUIMessageType.OnClick)
        {
            if (msg.Sender.name == "ButtonEquipTypeLeg")
            {
                EnableListLeg();
            }
            else if (msg.Sender.name == "ButtonEquipTypeBody")
            {
                EnableListBody();
            }
            else if (msg.Sender.name == "ButtonEquipTypeWeapon")
            {
                EnableListWeapon();
            }
            else if (msg.Sender.name == "ButtonWeapon")
            {
                CLUIButtonEquip equip = msg.Sender.GetComponent<CLUIButtonEquip>();
                CLMechEquip mechEquip = mCurEditMech.GetComponent<CLMechEquip>();
                mechEquip.SetWeaponL(equip.PrefabL);
                mechEquip.SetWeaponR(equip.PrefabR);
            }
            else if (msg.Sender.name == "ButtonLeg")
            {
                CLUIButtonEquip equip = msg.Sender.GetComponent<CLUIButtonEquip>();
                CLMechEquip mechEquip = mCurEditMech.GetComponent<CLMechEquip>();
                mechEquip.SetLeg(equip.Prefab);
            }
            else if (msg.Sender.name == "ButtonBody")
            {
                CLUIButtonEquip equip = msg.Sender.GetComponent<CLUIButtonEquip>();
                CLMechEquip mechEquip = mCurEditMech.GetComponent<CLMechEquip>();
                mechEquip.SetBody(equip.Prefab);
            }
            else if (msg.Sender.name == "ButtonStart")
            {
                if (CLGame.Instance.SingleMode)
                {
                    CLGame.Instance.Action<CLGameBattle>();
                }
                else
                {
                    CLGame.Instance.GetComponent<CLGameEquip>().SenPlayerReady();
                }
            }
            else if (msg.Sender.name == "ButtonPreMechSlot")
            {
                int slotCount = CLGame.Instance.PlayerMechSolts.Count;
                int curIndex = CLGame.Instance.PlayerMechSolts.IndexOf(mCurEditMech);
                mCurEditMech.gameObject.SetActive(false);
                curIndex = (curIndex + slotCount - 1) % slotCount;
                mCurEditMech = CLGame.Instance.PlayerMechSolts[curIndex];
                mCurEditMech.gameObject.SetActive(true);
            }
            else if (msg.Sender.name == "ButtonNexMechSlot")
            {
                int slotCount = CLGame.Instance.PlayerMechSolts.Count;
                int curIndex = CLGame.Instance.PlayerMechSolts.IndexOf(mCurEditMech);
                mCurEditMech.gameObject.SetActive(false);
                curIndex = (curIndex + slotCount + 1) % slotCount;
                mCurEditMech = CLGame.Instance.PlayerMechSolts[curIndex];
                mCurEditMech.gameObject.SetActive(true);
            }
        }
    }

    void EnableListLeg()
    {
        ListLeg.SetActive(true);
        ListBody.SetActive(false);
        ListWeapon.SetActive(false);
    }

    void EnableListBody()
    {
        ListLeg.SetActive(false);
        ListBody.SetActive(true);
        ListWeapon.SetActive(false);
    }

    void EnableListWeapon()
    {
        ListLeg.SetActive(false);
        ListBody.SetActive(false);
        ListWeapon.SetActive(true);
    }
}