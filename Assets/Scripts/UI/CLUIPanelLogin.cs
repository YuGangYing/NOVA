using UnityEngine;
using System.Collections;

public class CLUIPanelLogin : CLUIPanel
{
    void OnUIMessage(CLUIMessage msg)
    {
        if (msg.Type == CLUIMessageType.OnClick)
        {
            if (msg.Sender.name == "ButtonLogin")
            {
                if (CLGame.Instance.SingleMode)
                {
                    CLGame.Instance.Action<CLGameEquip>();
                }
                else
                {
                    CLGame.Instance.GameLogin.SenPlayerLogin();
                }
            }
        }
    }
}