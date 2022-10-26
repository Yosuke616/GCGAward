using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    //[System.NonSerialized]
    public enum STATE_FADE
    {
        FADE_IN = 0,
        FADE_OUT,
        FADED_MAX,
    }
    private Image image;            // 画像の情報
    private float fTimer;           // 経過時間
    private bool isFade;            // フェードしているかどうか
    private float fFadeTime;        // フェードにかける時間
    public bool isFadeFin;

    // Start is called before the first frame update
    void Start()
    {
        isFade = false;
        isFadeFin = false;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFade)
        {
            fTimer += Time.deltaTime;       // タイマー更新

            // 一定時間経過したらフェードを終了させる
            if(fTimer > fFadeTime)
            {
                isFadeFin = true;           // フェードが終了したことを知らせる
            }
        }
    }

    public void PanelFade(STATE_FADE fadeState, float time)
    {
        isFade = true;          // フェード中にする
        fFadeTime = time;

        switch (fadeState)
        {
            // フェードイン
            case STATE_FADE.FADE_IN:
                //image.color.a +=  
                break;

            // フェードアウト
            case STATE_FADE.FADE_OUT:
                break;
        }
    }
}
