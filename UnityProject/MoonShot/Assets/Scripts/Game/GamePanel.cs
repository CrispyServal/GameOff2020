using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.unity.mgobe;
using UnityEngine.UI;
using TMPro;

public class GamePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Game Game;
    public TMP_Text CommonResouceCount;

    void Start()
    {
        foreach (MPlayer player in players)
        {
            player.Game = Game;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRoomId(string s)
    {
        this.transform.Find("RoomId").GetComponent<TMP_Text>().text = "RoomId: " + s;
    }

    public void SetCommonResouceCount(int count)
    {
        CommonResouceCount.text = count.ToString();
    }

    public void OnCommonResouceAddButtonClick()
    {
        Game.AddCommonResource(true);
        CommonResouceCount.text = Game.GetCommonResouceCount().ToString();
    }

    public List<MPlayer> players;
    public void RefreshPlayerList(RoomInfo roomInfo)
    {
        var playerInfoList = roomInfo.PlayerList;
        for (int i = 0; i < players.Count; i++)
        {
            if (i < playerInfoList.Count)
            {
                players[i].Init(playerInfoList[i].Id);
                players[i].Show();
            }
            else
            {
                players[i].Hide();
            }
        }
    }
}
