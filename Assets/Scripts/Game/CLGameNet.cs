using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLGameNet : MonoBehaviour
{
    public ZYSocket.ZYSocket zysocket = new ZYSocket.ZYSocket();
    float curHeartbeatTime;
    Dictionary<char, RecHandler> mRecHandlers = new Dictionary<char, RecHandler>();
    // Use this for initialization
    void Start()
    {
        if (!CLGame.Instance.SingleMode)
        {
            zysocket.Connect("124.207.119.35", 10000);
            //zysocket.Connect("192.168.1.204", 10000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CLGame.Instance.SingleMode)
        {
            SendHeartbeatPkg();
            HandleNetPkgs();
        }
    }

    void HandleNetPkgs()
    {
        // print(zysocket.cmds.Count);
        zysocket.ReceiveSorket();
        if (zysocket.cmds.Count > 0)
        {
            print(zysocket.cmds.Count);
            foreach (string stream in zysocket.cmds)
            {
                RecHandler handler = mRecHandlers[stream[0]];
                handler(stream);
            }
            zysocket.cmds.Clear();
        }
    }

    void SendHeartbeatPkg()
    {
        curHeartbeatTime += Time.deltaTime;
        if (curHeartbeatTime > 30)
        {
            zysocket.SendMessage("6");
            curHeartbeatTime = 0;
        }
    }

    public void AddRecHandler(char id, RecHandler handler)
    {
        mRecHandlers.Add(id, handler);
    }
}

public delegate void RecHandler(string stream);