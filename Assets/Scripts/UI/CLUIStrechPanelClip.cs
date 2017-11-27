using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CLUIStrechPanelClip : MonoBehaviour
{
    public UIPanel ClippedPanel;
    public UIStretch Stretch;
    public Vector2 Center;
    public Vector2 Size;
    public Vector2 Softness;

    // Update is called once per frame
    void Start()
    {
        Vector4 clipRange;
        clipRange.x = Center.x * Screen.width;
        clipRange.y = Center.y * Screen.height;
        clipRange.z = Size.x * Screen.width;
        clipRange.w = Size.y * Screen.height;
        //clipRange.x = Center.x * Stretch.transform.localScale.x;
        //clipRange.y = Center.y * Stretch.transform.localScale.y;
        //clipRange.z = Size.x * Stretch.transform.localScale.x;
        //clipRange.w = Size.y * Stretch.transform.localScale.y;
        ClippedPanel.clipRange = clipRange;
    }
}
