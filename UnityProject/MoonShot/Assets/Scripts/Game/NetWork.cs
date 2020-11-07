using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unity.mgobe;


[Serializable]
public struct SendObject
{
    public string method;
    public string value;
}

[Serializable]
public struct SyncObject
{
    public int hitCount;
}

public class NetWork : MonoBehaviour
{
    // Start is called before the first frame update
    public Game Game;
    void Start()
    {
    }


    void InitGlobal(string userId)
    {
        Global.OpenId = userId;
        Global.GameId = "obg-j31fmxqp";
        Global.SecretKey = "fe04f6219e679f6ac97dba515780b898dba0a209";
        Global.Server = "j31fmxqp.wxlagame.com";
    }

    public void InitNetWork(string userId)
    {
        InitGlobal(userId);
        GameInfoPara gameInfo = new GameInfoPara
        {
            GameId = Global.GameId,
            OpenId = Global.OpenId,
            SecretKey = Global.SecretKey,
        };

        ConfigPara config = new ConfigPara
        {
            Url = Global.Server,
            ReconnectMaxTimes = 5,
            ReconnectInterval = 1000,
            ResendInterval = 1000,
            ResendTimeout = 10000
        };

        Listener.Init(gameInfo, config, OnListenerInit);
    }

    enum EStartMode
    {
        Create,
        Join,
    }

    class WaitStartInfo
    {
        public EStartMode mode;
        public string roomName;
        public string password;
    }
    WaitStartInfo waitStartInfo;
    public void ReadyToCreate(string roomName, string password)
    {
        ReadyToStart(roomName, password, EStartMode.Create);
    }

    public void ReadyToJoin(string roomName, string password)
    {
        ReadyToStart(roomName, password, EStartMode.Join);
    }

    void ReadyToStart(string roomName, string password, EStartMode mode)
    {
        waitStartInfo = new WaitStartInfo
        {
            roomName = roomName,
            password = password,
            mode = mode,
        };
    }

    void OnListenerInit(ResponseEvent eve)
    {
        if (eve.Code == ErrCode.EcOk)
        {
            Global.Room = new Room(null);
            Debug.Log("初始化成功");
            Listener.Add(Global.Room);
            // TODO: more

            if (waitStartInfo != null)
            {
                AutoJoinRoom(); 
            }
        }

        InitBroadcast();
    }

    void InitBroadcast()
    {
        // 设置收帧广播回调函数
        Global.Room.OnRecvFrame = eve =>
        {
            RecvFrameBst bst = (RecvFrameBst)eve.Data;
            AddAction(() => this.OnFrame(bst.Frame));
        };

        // 设置消息接收广播回调函数
        Global.Room.OnRecvFromClient = eve =>
        {
            RecvFromClientBst bst = (RecvFromClientBst)eve.Data;
        };

        // 设置服务器接收广播回调函数
        Global.Room.OnRecvFromGameSvr = eve =>
        {
            RecvFromGameSvrBst bst = (RecvFromGameSvrBst)eve.Data;
        };

        // 设置房间改变广播回调函数
        Global.Room.OnChangeRoom = eve =>
        {
            this.OnChangeRoom();
        };

        /* //no match for now
        Room.OnMatch = eve =>
        {
            RefreshRoomList();
            Debugger.Log("on match!");
        };

        Room.OnCancelMatch = eve =>
        {
            RefreshRoomList();
            Debugger.Log("on cancel match! ");
        };
        */

    }


    #region Action
    List<Action> actionList = new List<Action>();
    static object onFrameLock = new object();
    private void AddAction(Action cb)
    {
        lock (onFrameLock)
        {
            actionList.Add(cb);
        }
    }
    #endregion

    void Update()
    {
        if (actionList.Count != 0)
        {
            lock (onFrameLock)
            {
                foreach (var item in actionList)
                {
                    try
                    {
                        item?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
                actionList.Clear();
            }
        }
    }


    #region Room

    void AutoJoinRoom()
    {
        // 1 check if is in
        Room.GetMyRoom((e) =>
        {
            if (e.Code == ErrCode.EcOk)
            {
                var roomInfo = (GetRoomByRoomIdRsp)e.Data;
                Global.Room.InitRoom(roomInfo.RoomInfo);
                Debug.Log("is in room, joining game");
                Debug.Log(roomInfo);
                AddAction(() => this.StartGame());
                return;
            }
            else if (e.Code == ErrCode.EcRoomPlayerNotInRoom)
            {
                Debug.Log("is not in, joining room");
                switch (waitStartInfo.mode) {
                    case EStartMode.Create:
                        AddAction(() => this.CreateRoom());
                        break;
                    case EStartMode.Join:
                        AddAction(() => this.JoinRoom());
                        break;
                }
                return;
            }

            Debug.Log("bug");
        });
    }

    void RefreshRoomList()
    {
        
    }

    void OnJoinRoom()
    {

    }

    PlayerInfoPara MakeMyPlayerInfo()
    {
        return new PlayerInfoPara
        {
            Name = Global.OpenId,
            CustomPlayerStatus = 0,
            CustomProfile = "TODO",
        };
    }

    void CreateRoom()
    {
        var param = new CreateRoomPara
        {
            RoomName = waitStartInfo.roomName,
            RoomType = "TODO",
            MaxPlayers = 4,
            IsPrivate = !string.IsNullOrEmpty(waitStartInfo.password),
            CustomProperties = "TODO",
            PlayerInfo = MakeMyPlayerInfo(),
        };

        Global.Room.CreateRoom(param, (e) =>
        {
            Debug.Log("create room callback");
            Debug.Log(e);

            if (e.Code == ErrCode.EcOk)
            {
                AddAction(() => this.StartGame());
            }
        });
    }

    void JoinRoom()
    {
        var param = new JoinRoomPara
        {
            PlayerInfo = new PlayerInfoPara
            {
                Name = Global.OpenId,
                CustomPlayerStatus = 0,
                CustomProfile = "TODO",
            },
        };

        Global.Room.InitRoom(new RoomInfo()
        {
            Id = waitStartInfo.roomName,
        });

        Global.Room.JoinRoom(param, (e) => {
            Debug.Log(e);
            if (e.Code == ErrCode.EcOk)
            {
                AddAction(() => this.StartGame());
            }
        });
 
    }

    void OnChangeRoom()
    {
        Debug.Log("change room");
    }

    #endregion

    #region frame
    void OnFrame(Frame frame)
    {
        if (frame.Items != null)
        {
            foreach (FrameItem item in frame.Items)
            {
                Debug.Log(item);
                if (item.PlayerId != Player.Id)
                {
                    SendObject syncObject = JsonUtility.FromJson<SendObject>(item.Data);
                    switch (syncObject.method)
                    {
                        case "CommonResource":
                            Game.SetCommonResource(int.Parse(syncObject.value));
                            break;
                        case "HitPlayer":
                            Game.OnHitPlayer(syncObject.value);
                            break;
                        case "SyncUp":
                            Game.SyncForPlayer(item.PlayerId, JsonUtility.FromJson<SyncObject>(syncObject.value));
                            break;
                    }
                }
            }
        }
    }

    public void SendFrame(SendObject frameData)
    {
        var param = new SendFramePara
        {
            Data = JsonUtility.ToJson(frameData),
        };
        Debug.Log(param.Data);
        Global.Room.SendFrame(param, (e) => {
            // TODO
            Debug.Log("send frame callback");
            Debug.Log(e);
        });
    }
    #endregion

    void StartGame()
    {
        Debug.Log("start game");
        Game.StartGame();
        Global.Room.StartFrameSync((e) =>
        {
            Debug.Log("start frame sync");
            Debug.Log(e);
            /*
            var param = new RequestFramePara
            {
                BeginFrameId = 0,
                EndFrameId = 99999,
            };
            Global.Room.RequestFrame(param, (e2) => {
                Debug.Log(e2);
            });
            */
        });


    }
}
