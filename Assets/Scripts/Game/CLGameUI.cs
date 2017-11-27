using UnityEngine;
using System.Collections;

public class CLGameUI : MonoBehaviour
{
    public CLUIPanelUpdate PanelUpdate;
    public CLUIPanelLogin PanelLogin;
    public CLUIPanelEquip PanelEquip;
    public CLUIPanelBattle PanelBattle;
    public CLUIPanelAccount PanelAccount;
    public CLUIPanelLoading PanelLoading;
    public GameObject GameUIRoot;
    static CLGameUI sSingletonInstance;

    CLGameUI()
    {
        CLGameUI.sSingletonInstance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(GameUIRoot);
    }

    public static CLGameUI Instance
    {
        get
        {
            return sSingletonInstance;
        }
    }
}