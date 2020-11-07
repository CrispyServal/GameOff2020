using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using com.unity.mgobe;

public class Game : MonoBehaviour
{

    public NetWork NetWork;
    public GamePanel GamePanel;
    public GameObject LoginPanel;
    void Start()
    {
        StartCoroutine("CSyncUp");
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isInGame = false;
    public void StartGame()
    {
        GamePanel.gameObject.SetActive(true);
        LoginPanel.SetActive(false);

        var roomId = Global.Room.RoomInfo.Id;
        Debug.Log("StartGame. roomid: " + roomId);
        GamePanel.SetRoomId(roomId);


        isInGame = true;

        GamePanel.RefreshPlayerList(Global.Room.RoomInfo);
    }

    public void OnSomeOneJoin()
    {
        GamePanel.RefreshPlayerList(Global.Room.RoomInfo);
    }

    public void AddCommonResource(bool needSyncToOther = false)
    {
        // TDOO
        this.CommonResouceCount += 1;
        if (needSyncToOther)
        {
            NetWork.SendFrame(new SendObject
            {
                method = "CommonResource",
                value = this.CommonResouceCount.ToString(),
            });
        }
        GamePanel.SetCommonResouceCount(this.CommonResouceCount);
    }

    public void SetCommonResource(int value)
    {
        this.CommonResouceCount = value;
        GamePanel.SetCommonResouceCount(value);
    }

    public void OnHitPlayer(string targetId)
    {
        foreach (var player in GamePanel.players) {
            if (player.PlayerId == targetId) {
                player.OnHitByOther();
            }
        }
        //if (targetId == Player.Id)
            //SyncUp();
    }

    private int CommonResouceCount = 0;
    private int myHitCount = 0;
       
    public int GetCommonResouceCount()
    {
        return CommonResouceCount;
    }

    public void HitPlayer(string id)
    {
        if (id == Player.Id)
        {
            Debug.Log("hit myself");
            return;
        }

        NetWork.SendFrame(new SendObject
        {
            method = "HitPlayer",
            value = id,
        });
    }

    IEnumerator CSyncUp()
    {
        while (true)
        {
            yield return null;
            if (isInGame)
            {
                //SyncUp();
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void SyncUp()
    {
        Debug.Log("sync up");
        NetWork.SendFrame(new SendObject
        {
            method = "SyncUp",
            value = JsonUtility.ToJson(new SyncObject
            {
                hitCount = myHitCount,
            }),
        });
    }

    public void SyncForPlayer(string targtId, SyncObject syncObject)
    {
        if (!isInGame)
            return;
        foreach (var player in GamePanel.players)
        {
            player.TrySync(targtId, syncObject);
        }    
    }
}
