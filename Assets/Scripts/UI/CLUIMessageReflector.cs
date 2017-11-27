using UnityEngine;
using System.Collections;

public class CLUIMessageReflector : MonoBehaviour
{
    void OnHover (bool isOver)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.OnHoverIsOver = isOver;
        msg.Type = CLUIMessageType.OnHover;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnPress (bool isDown)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.OnPressIsDown = isDown;
        msg.Type = CLUIMessageType.OnPress;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnSelect(bool selected)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.OnSelectSelected = selected;
        msg.Type = CLUIMessageType.OnSelect;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnClick()
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnClick;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnDoubleClick()
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnDoubleClick;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnDrag(Vector2 delta)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnDrag;
        msg.OnDragDelta = delta;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnDrop(GameObject gameObject)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnDrop;
        msg.OnDropObject = gameObject;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnInput(string text)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnInput;
        msg.OnInputText = text;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnTooltip(bool show)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnTooltip;
        msg.OnTooltipShow = show;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnScroll(float delta)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnScroll;
        msg.OnScrolDelta = delta;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }

    void OnKey(KeyCode key)
    {
        CLUIMessage msg = new CLUIMessage(gameObject);
        msg.Type = CLUIMessageType.OnKey;
        msg.OnKeyKey = key;
        SendMessageUpwards("OnUIMessage", msg, SendMessageOptions.DontRequireReceiver);
    }
}

public class CLUIMessage
{
    public CLUIMessageType Type;
    public GameObject Sender;
    public bool OnHoverIsOver;
    public bool OnPressIsDown;
    public bool OnSelectSelected;
    public Vector2 OnDragDelta;
    public GameObject OnDropObject;
    public string OnInputText;
    public bool OnTooltipShow;
    public float OnScrolDelta;
    public KeyCode OnKeyKey;

    public CLUIMessage(GameObject sender)
    {
        Sender = sender;
    }
}

public enum CLUIMessageType
{
    OnHover,
    OnPress,
    OnSelect,
    OnClick,
    OnDoubleClick,
    OnDrag,
    OnDrop,
    OnInput,
    OnTooltip,
    OnScroll,
    OnKey
}