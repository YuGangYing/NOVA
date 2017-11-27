using UnityEngine;
using System.Collections;

public class CLUIPanelAccount : CLUIPanel
{
    public UILabel Result;

    protected override void OnOpen()
    {
        if (CLGame.Instance.GameBattle.IsPlayerWin())
        {
            Result.text = "ʤ        ��";
        }
        else if (CLGame.Instance.GameBattle.IsEnemyWin())
        {
            Result.text = "ʧ        ��";
        }
        base.OnOpen();
    }

    void OnUIMessage(CLUIMessage msg)
    {
        if (msg.Type == CLUIMessageType.OnClick)
        {
            if (msg.Sender.name == "ButtonConfirm")
            {
                if (CLGame.Instance.SingleMode)
                {
                    CLGame.Instance.Action<CLGameEquip>();
                }
                else
                {
                    CLGame.Instance.GetComponent<CLGameBattle>().SenExitBattle();
                }
            }
        }
    }
}