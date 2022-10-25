using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class GameOverScript : MonoBehaviour
{
    //必要になってくるボタンの設定
    [SerializeField] private GameObject Retory_Btn;
    [SerializeField] private GameObject Title_Btn;

    //【】を出して動かしていく
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //数字を取得する為に変数を用意しておく
    [Header("スコア用テキスト")]
    [SerializeField] private GameObject HeadShot;
    [SerializeField] private GameObject BreakEnemy;
    [SerializeField] private GameObject WaveRun;
    [SerializeField] private GameObject TotalScore;

    //設定するための変数
    private Text Head;
    private Text Break;
    private Text Wave;
    private Text Total;


    //外側に動くか内側に動くか
    //trueで外側 falseで内側
    private bool bInOrOut;
    private int nCnt;
    //矢印を動かしたときに一回だけ座標を変える変数
    private bool bChangeFlg;

    //ゲームオーバーUI動かせるようにするフラグ
    //trueで動かせる falseで動かせない
    private bool bUseFlg;

    //スコア(仮)からスコアを引っ張てっくる
    private WaveManager WM; 

    //必要になってくるボタンを追加する
    public enum RESULT_BUTTON {
        RETORY_BUTTON,
        TITLE_BUTTON,

        MAX_BUTTON
    }

    private RESULT_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        bUseFlg = false;
        eButton = RESULT_BUTTON.RETORY_BUTTON;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;

        //文字のコンポーネントの取得
        Head = HeadShot.GetComponent<Text>();
        Break = BreakEnemy.GetComponent<Text>();
        Wave = WaveRun.GetComponent<Text>();
        Total = TotalScore.GetComponent<Text>();

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        int num;
        //文字を数字に変える
        num = WM.GetHeadShot();
        Head.text = num.ToString();
        num = WM.GetBreakEnemy();
        Break.text = num.ToString();
        num = WM.GetWave();
        Wave.text = num.ToString();
        num = WM.GetScore();
        Total.text = num.ToString();

    }

    // Update is called once per frame
    void Update()
    {

            Debug.Log(2345676543);
        if (bUseFlg) {
            //スコア表示
            int num;
            //文字を数字に変える
            num = WM.GetHeadShot();
            Debug.Log(num);
            Head.text = num.ToString();
            num = WM.GetBreakEnemy();
            Break.text = num.ToString();
            num = WM.GetWave();
            Wave.text = num.ToString();
            num = WM.GetScore();
            Total.text = num.ToString();

            //ボタンをデフォルトの大きさにしていく
            Retory_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Title_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

            //オブジェクトを変化させる関数を作成する
            GameObject ChangeObj = SetPointObj();

            //拡大する
            ChangeObj.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

            //【】を動かす関数
            MoveBrackets();

            //クリックして決定できる
            if (Input.GetMouseButtonDown(0)) {
                SelectButton();
            }
        }
    }

    public void SetUseFlg(bool bUse) {
        bUseFlg = bUse;
    }

    private GameObject SetPointObj() {
        //選ばれている列挙体によって処理する内容を変更する
        GameObject obj = null;
        switch(eButton){
            case RESULT_BUTTON.RETORY_BUTTON:
                obj = Retory_Btn;
                break;
            case RESULT_BUTTON.TITLE_BUTTON:
                obj = Title_Btn;
                break;
        }

        if (bChangeFlg) {
            Right_Bracket.transform.position = new Vector3(obj.transform.position.x + 185.0f, obj.transform.position.y, obj.transform.position.z);
            Left_Bracket.transform.position = new Vector3(obj.transform.position.x - 50.0f, obj.transform.position.y, obj.transform.position.z);
            bChangeFlg = false;
        }

        return obj;
    }

    private void MoveBrackets(){
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
        else {
            //内側
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

    //マウスで列挙体をセットさせる
    public void SetButton(int nButton) {
        RESULT_BUTTON btn = (RESULT_BUTTON)Enum.ToObject(typeof(RESULT_BUTTON),nButton);
        eButton = btn;
    }

    //カーソル移動をさせる
    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        bInOrOut = true;
    }

    private void SelectButton() {
        switch (eButton) {
            case RESULT_BUTTON.RETORY_BUTTON:
                SceneManager.LoadScene("TitleScene2");
                break;
            case RESULT_BUTTON.TITLE_BUTTON:
                SceneManager.LoadScene("TitleScene2");
                break;
        }
    }

}
