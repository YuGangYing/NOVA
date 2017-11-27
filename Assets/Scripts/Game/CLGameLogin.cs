using UnityEngine;
using System.Collections;

public class CLGameLogin : CLAction<CLGame>
{
    public override void OnAddAction()
    {
        Actor.Net.AddRecHandler('0', new RecHandler(RecPlayerLogin));
        base.OnAddAction();
    }

    public override void Enter()
    {
        CLGameUI.Instance.PanelLogin.Open();
        Application.LoadLevel("Login");
        CLAudio.PlayLoop(CLGameAudio.Instance.GetComponent<AudioSource>(), CLGameAudio.Instance.Login);
        base.Enter();
    }

    public override void Exit()
    {
        CLGameUI.Instance.PanelLogin.Close();
        base.Exit();
    }

    public void SenPlayerLogin()
    {
        string stream = "";
        LoginPkg loginPkg = new LoginPkg();
        loginPkg.Write(ref stream);
        Actor.Net.zysocket.SendMessage(stream);
        print("SenPlayerLogin");
    }

    public void RecPlayerLogin(string stream)
    {
        LoginPkg loginPkg = new LoginPkg();
        loginPkg.Read(ref stream);
        Actor.PlayerID = loginPkg.PlayerID - '0';
        Actor.EnemyID = (Actor.PlayerID + 1) % 2;
        CLGame.Instance.Action<CLGameEquip>();
        print("RecPlayerLogin " + "playerid:" + Actor.PlayerID + " enenmyid:" + CLGame.Instance.EnemyID);
    }
}