using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using com.unity.mgobe;

public class Game : MonoBehaviour
{

    public NetWork NetWork;
    public GameObject GamePanel;
    public GameObject LoginPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GamePanel.SetActive(true);
        LoginPanel.SetActive(false);

        var roomId = Global.Room.RoomInfo.Id;
        Debug.Log("StartGame. roomid: " + roomId);
        GamePanel.transform.Find("RoomId").GetComponent<TMP_Text>().text = "RoomId" + roomId;
    }
}
