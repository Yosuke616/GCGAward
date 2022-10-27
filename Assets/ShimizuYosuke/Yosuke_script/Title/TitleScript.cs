using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class TitleScript : MonoBehaviour
{
    //オブジェクトの設定
    [SerializeField] GameObject Start_Btn;
    [SerializeField] GameObject Tutorial_Btn;
    [SerializeField] GameObject Option_Btn;
    [SerializeField] GameObject End_Btn;

    //オプション出すよーん
    [SerializeField] private GameObject Pause_UI;

    //【】を出して動かしておく
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //外側に動くか内側に動くか
    //trueで外側 falseで内側
    private bool bInOrOut;
    private int nCnt;
    //矢印を動かしたときに一回だけ座標を変える変数
    private bool bChangeFlg;

    //時間設定
    [Header("どの位の時間でボタン制御するか")]
    [SerializeField] private int DELTTIME = 10;
    private int nDeltTime;

    //デフォルトの場所
    private Vector3 Str_Pos;
    private Vector3 Tuto_Pos;
    private Vector3 Opt_Pos;
    private Vector3 End_Pos;

    //必要になってくるボタンを追加していく
    public enum TITLE_BUTTON {
        START_BUTTON,
        TUTORIAL_BUTTON,
        OPTION_BUTTON,
        END_BUTTON,

        MAX_BUTTON
    }

    private TITLE_BUTTON eButton;

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        //初めは始めるボタンを選択しておく
        eButton = TITLE_BUTTON.START_BUTTON;
        Pause_UI.SetActive(false);

        //デフォルトの場所を保存しておく
        Str_Pos = Start_Btn.transform.position;
        Tuto_Pos = Tutorial_Btn.transform.position;
        Opt_Pos = Option_Btn.transform.position;
        End_Pos = End_Btn.transform.position;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        nDeltTime++;

        //ボタンでの処理
        //時間がある程度立つと移動するようにする
        if (nDeltTime > DELTTIME) {
            //ボタンの選択ができるようにする
            if (Input.GetKey(KeyCode.W)) {
                eButton--;
                if (eButton < 0) {
                    eButton = TITLE_BUTTON.MAX_BUTTON - 1;
                }
                bChangeFlg = true;
                nCnt = 0;
                nDeltTime = 0;
                bInOrOut = true;
            }
            if (Input.GetKey(KeyCode.S)) {
                eButton++;
                if (eButton >= TITLE_BUTTON.MAX_BUTTON) {
                    eButton = 0;
                }
                bChangeFlg = true;
                nCnt = 0;
                nDeltTime = 0;
                bInOrOut = true;
            }
        }

        //大きさもデフォルトに変える
        Start_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Tutorial_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        Option_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        End_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //場所をデフォルトに保存しておく
        Start_Btn.transform.position = Str_Pos;
        Tutorial_Btn.transform.position = Tuto_Pos;
        Option_Btn.transform.position = Opt_Pos;
        End_Btn.transform.position = End_Pos;
        //日本語を非表示にしておく
        GameObject Str_Chil = Start_Btn.transform.GetChild(0).gameObject;
        GameObject Tuto_Chil = Tutorial_Btn.transform.GetChild(0).gameObject;
        GameObject Opt_Chil = Option_Btn.transform.GetChild(0).gameObject;
        GameObject End_Chil = End_Btn.transform.GetChild(0).gameObject;
        Str_Chil.SetActive(false);
        Tuto_Chil.SetActive(false);
        Opt_Chil.SetActive(false);
        End_Chil.SetActive(false);

        //選ばれている列挙体変数によって処理する内容を変える
        switch (eButton) {
            case TITLE_BUTTON.START_BUTTON:
                //座標を少し右にずらす
                Start_Btn.transform.position = new Vector3(Str_Pos.x - 100.0f, Str_Pos.y, Str_Pos.z);
                Start_Btn.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                //日本語を出す
                Str_Chil.SetActive(true);
                //かっこの場所を変更する
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Start_Btn.transform.position.x + 240.0f, Start_Btn.transform.position.y, Start_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Start_Btn.transform.position.x - 250.0f, Start_Btn.transform.position.y, Start_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.TUTORIAL_BUTTON:
                //座標を左にずらす
                Tutorial_Btn.transform.position = new Vector3(Tuto_Pos.x - 100.0f,Tuto_Pos.y,Tuto_Pos.z);
                Tutorial_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //日本語を出す
                Tuto_Chil.SetActive(true);
                //かっこの場所を変更する
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Tutorial_Btn.transform.position.x + 335.0f, Tutorial_Btn.transform.position.y, Tutorial_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Tutorial_Btn.transform.position.x - 325.0f, Tutorial_Btn.transform.position.y, Tutorial_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.OPTION_BUTTON:
                Option_Btn.transform.position = new Vector3(Opt_Pos.x - 100.0f, Opt_Pos.y, Opt_Pos.z);
                Option_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Opt_Chil.SetActive(true);
                //かっこの場所を変更する
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Option_Btn.transform.position.x + 260.0f, Option_Btn.transform.position.y-20.0f, Option_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Option_Btn.transform.position.x - 250.0f, Option_Btn.transform.position.y-20.0f, Option_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.END_BUTTON:
                End_Btn.transform.position = new Vector3(End_Pos.x - 100.0f, End_Pos.y, End_Pos.z);
                End_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                End_Chil.SetActive(true);
                //かっこの場所を変更する
                if (bChangeFlg)
                {
                    Right_Bracket.transform.position = new Vector3(End_Btn.transform.position.x + 175.0f, End_Btn.transform.position.y+15.0f, End_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(End_Btn.transform.position.x - 125.0f, End_Btn.transform.position.y+15.0f, End_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.MAX_BUTTON:
                //大きさもデフォルトに変える
                Start_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Tutorial_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Option_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                End_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //場所をデフォルトに保存しておく
                Start_Btn.transform.position = Str_Pos;
                Tutorial_Btn.transform.position = Tuto_Pos;
                Option_Btn.transform.position = Opt_Pos;
                End_Btn.transform.position = End_Pos;
                //日本語を非表示にしておく
                Str_Chil = Start_Btn.transform.GetChild(0).gameObject;
                Tuto_Chil = Tutorial_Btn.transform.GetChild(0).gameObject;
                Opt_Chil = Option_Btn.transform.GetChild(0).gameObject;
                End_Chil = End_Btn.transform.GetChild(0).gameObject;
                Str_Chil.SetActive(false);
                Tuto_Chil.SetActive(false);
                Opt_Chil.SetActive(false);
                End_Chil.SetActive(false);
                break;
            default: break;
        }

        //カーソルが動かす関数
        //かっこの場所を変更する
        MoveBrackets();


        //ボタンが押されたときの処理
        if (Input.GetKey(KeyCode.Return) || Input.GetMouseButtonDown(0)) {
            switch (eButton)
            {
                case TITLE_BUTTON.START_BUTTON:
                    SceneManager.LoadScene("YosukeScene");
                    break;
                case TITLE_BUTTON.TUTORIAL_BUTTON:
                    SceneManager.LoadScene("GameScene");
                    break;
                case TITLE_BUTTON.OPTION_BUTTON:
                    if (Mathf.Approximately(Time.timeScale, 1f))
                    {
                        Time.timeScale = 0f;
                        Pause_UI.SetActive(true);
                    }
                    break;
                case TITLE_BUTTON.END_BUTTON:
                    //UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                    break;
                default: break;
            }
        }

    }

    //ボタンにセットさせる
    public void SetButton(int nButton) {
        TITLE_BUTTON btn = (TITLE_BUTTON)Enum.ToObject(typeof(TITLE_BUTTON), nButton);
        eButton = btn;
    }

    private void MoveBrackets() {
        if (bInOrOut)
        {
            //外側
            Right_Bracket.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30) {
                nCnt = 0;
                bInOrOut = false;
            }

        }
        else {
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

    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        nDeltTime = 0;
        bInOrOut = true;
    }
}
