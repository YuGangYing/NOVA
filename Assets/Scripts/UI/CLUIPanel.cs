using UnityEngine;
using System.Collections;

public abstract class CLUIPanel : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        OnOpen();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnClose();
    }

    protected virtual void OnOpen()
    {

    }

    protected virtual void OnClose()
    {
    }
}