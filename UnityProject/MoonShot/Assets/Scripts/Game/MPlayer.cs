using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MPlayer : MonoBehaviour
{

    public Game Game;
    public TMP_Text HitCountText;
    public TMP_Text IdText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string PlayerId {get; set;}
    bool isInit;
    int hitCount = 0;

    public void Init(string id)
    {
        PlayerId = id;
        IdText.text = id;
        isInit = true;
    }

    public void OnHitByOther()
    {
        hitCount += 1;
        HitCountText.text = hitCount.ToString();
    }

    public void OnHitButtonClick()
    {
        if (!isInit)
            return;
        Game.HitPlayer(PlayerId);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void TrySync(string id, SyncObject syncObject)
    {
        if (id == PlayerId)
        {
            hitCount = syncObject.hitCount;
            HitCountText.text = hitCount.ToString();
        }
    }
}
