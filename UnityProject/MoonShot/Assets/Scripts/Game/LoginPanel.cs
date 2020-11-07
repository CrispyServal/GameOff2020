using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField UserId;
    public TMP_InputField RoomName;
    public TMP_InputField RoomPassword;
    public NetWork NetWork;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreate()
    {
        Debug.Log(UserId.text);
        if (NetWork != null)
        {
            NetWork.InitNetWork(UserId.text);
            NetWork.ReadyToCreate(RoomName.text, RoomPassword.text);
        }
        else
        {
            Debug.LogError("on create. no network instance");
        }
    }

    public void OnJoin()
    {
        Debug.Log(UserId.text);
        if (NetWork != null)
        {
            NetWork.InitNetWork(UserId.text);
            NetWork.ReadyToJoin(RoomName.text, RoomPassword.text);
        }
        else
        {
            Debug.LogError("on join. no network instance");
        }

    }
}
