using UnityEngine;
using System.Collections;

public class CLUIPanelUpdate : CLUIPanel
{
    public UISlider ProgressBar;

    protected override void OnOpen()
    {
        ProgressBar.sliderValue = 0f;
        base.OnOpen();
    }

    void Update()
    {
        ProgressBar.sliderValue = CLGame.Instance.GameUpdate.UpdateProgress;
    }


}