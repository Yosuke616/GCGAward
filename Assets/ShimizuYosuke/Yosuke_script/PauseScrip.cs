using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PauseScrip : MonoBehaviour
{
    //必要になってくるボタンの設定
    [SerializeField] private GameObject Option_Btn;
    [SerializeField] private GameObject Controll_Btn;
    [SerializeField] private GameObject Title_Btn;

    //さらに出現させるUI
    [SerializeField] private GameObject Option_UI;
    [SerializeField] private GameObject Controll_UI;
    [SerializeField] private GameObject Title_Ui;

    //【】を出して動かしていく
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //外側に動くか内側に動くか
    //trueで外側 falseで内側
    private bool bInOrOut;
    private int nCnt;
    //矢印を動かしたときに一回だけ座標を変える変数
    private bool bChangeFlg;

    //操作できるかどうかのフラグ
    //trueで操作できる falseで操作不可
    private bool bPause;
    

    //必要になってくるボタンを追加していく
    public enum PAUSE_BUTTON {
        OPTION_BUTTON,
        CONTROLL_BUTTON,
        TITLE_BUTTON,

        MAXBUTTON
    }

    private PAUSE_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        eButton = PAUSE_BUTTON.OPTION_BUTTON;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;
        bPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        //操作フラグがオンの時に操作できる
        if (bPause) {
            //ポーズメニューに入った時の操作
            //大きさをデフォルトに変更しておく
            Option_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Controll_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Title_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

            //マウスでボタンを探す

            //オブジェクトを変化させる変数を作成する
            GameObject ChangeObj = SetPointObj();

            //拡大する
            ChangeObj.transform.localScale = new Vector3(1.25f,1.25f,1.25f);

            //【】を動かすやつ
            MoveBrackets();

        }
        else {
            //オプションや操作説明の中身の操作

        }
    }

    private GameObject SetPointObj() {
        //選ばれている列挙体によって処理する内容を変更する
        GameObject obj = null;
        switch (eButton)
        {
            case PAUSE_BUTTON.OPTION_BUTTON:
                obj = Option_Btn;
                break;
            case PAUSE_BUTTON.CONTROLL_BUTTON:
                obj = Controll_Btn;
                break;
            case PAUSE_BUTTON.TITLE_BUTTON:
                obj = Title_Btn;
                break;
        }

        if (bChangeFlg) {
            Right_Bracket.transform.position = new Vector3(obj.transform.position.x + 200.0f, obj.transform.position.y, obj.transform.position.z);
            Left_Bracket.transform.position = new Vector3(obj.transform.position.x - 150.0f, obj.transform.position.y, obj.transform.position.z);
            bChangeFlg = false;
        }

        return obj;
    }

    public void SetButton(int nButton) {
        PAUSE_BUTTON btn = (PAUSE_BUTTON)Enum.ToObject(typeof(PAUSE_BUTTON),nButton);
        eButton = btn;
    }

    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        bInOrOut = true;
    }

    private void MoveBrackets() {
        if (bInOrOut)
        {
            //外側
            Right_Bracket.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30)
            {
                nCnt = 0;
                bInOrOut = false;
            }

        }
        else
        {
            //内側
            Right_Bracket.transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position -= new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30)
            {
                nCnt = 0;
                bInOrOut = true;
            }
        }
    }

}
