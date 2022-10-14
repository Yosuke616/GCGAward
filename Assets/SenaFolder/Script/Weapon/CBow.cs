using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CBow : MonoBehaviour
{
    // 矢の状態
    #region state
    public enum STATE_BOW
    {
        BOW_CHARGE = 0,       // チャージ状態
        BOW_NORMAL,           // 通常状態
        BOW_SHOT,             // 発射されている状態
        BOW_CHARGEMAX,        // 最大チャージ状態
        BOW_RESET,            // チャージリセット状態
    }
    #endregion

    #region serialize field
    [SerializeField] private GameObject PrefabArrow;       // 矢のオブジェクト
    [SerializeField] private GameObject spawner;
    [Header("チャージ最大段階")]
    [SerializeField, Range(1,10)] private int nMaxChargeStep = 1;
    [Header("チャージ時間(1段階)")]
    [SerializeField, Range(0.1f, 10.0f)] private float fValChargeTime = 0.5f;
    [Header("生成する矢の大きさ(0.5fがちょうどいいかも)")]
    [SerializeField, Range(0.1f, 1.0f)] private float arrowSize;
    [SerializeField] private GameObject objPlayer;          // プレイヤーオブジェクト
    [SerializeField] private CChargeSlider scChargeSlider;       // チャージ時間を表すスライダー
    [Header("一矢撃つごとに消費するHP量")]
    [SerializeField] public int nAtkDecHp;      // 一矢でのHP消費量
    [Header("威力調整に使うHP量")]
    [SerializeField] private int nAdjustHp;      // 調整時のHP消費量
    [Header("最大調整段階数")]
    [SerializeField] private int maxDecStep;
    #endregion

    #region variable
    private STATE_BOW g_state;              // 弓の状態
    public const int nMaxArrow = 10;
    private GameObject[] objArrow = new GameObject[nMaxArrow];            // 弓オブジェクト
    private float fChargeTime;              // チャージボタンを押している時間 
    private GameObject[] objCursur;         // カーソル
    private float maxChargeTime;            // 最大チャージ時間(Initで計算して格納する)
    private float currentChargeStep;        // 現在のチャージ段階数
    private bool isAdjust;                  // 使用するHPを調整したかどうか
    private int nCurrentStep;               // 現在の威力段階数
    private int nAtkValue;                  // 矢の攻撃力
    private int nCurrentArrowSetNum = 0;    // 現在構えている矢の番号
    private int nUseHP = 0;                 // 矢を撃つのに使用するHP
    private bool isShot = false;            // 矢を撃ったかどうか
    #endregion

    // Start is called before the first frame update
    #region init
    void Start()
    {
        g_state = STATE_BOW.BOW_NORMAL;
        fChargeTime = 0;
        // カーソルのサイドのオブジェクトを全て取得する
        objCursur = GameObject.FindGameObjectsWithTag("CursurSide");
        maxChargeTime = fValChargeTime * nMaxChargeStep;
        TellMaxChargeTime();        // スライダーに最大チャージ時間を伝える

        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().SetChargeMaxTime(maxChargeTime);
        currentChargeStep = 0.0f;
        nCurrentStep = 0;       // 威力段階数の初期化

        isAdjust = false;       // 使用HP未調整状態にする
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        // 更新処理
        UpdateState(g_state);
        // 左クリックでチャージ
        #region charge action
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < objCursur.Length; ++i)
                objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.MOVE);  // カーソルを動かす
            scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MOVE);       // スライダーを動かす
            nUseHP = nAtkDecHp + nAdjustHp * nCurrentStep;
            objPlayer.GetComponent<CSenaPlayer>().DecFrontHPBar(-1 * nUseHP);
            ChangeState(STATE_BOW.BOW_CHARGE);      // チャージ状態に変更する
        }
        #endregion
        // チャージ中に左クリックが離されたらチャージ解除
        // チャージ中に右クリックが押されたら発射
        #region release action
        if (g_state == STATE_BOW.BOW_CHARGE || g_state == STATE_BOW.BOW_CHARGEMAX)
        {
            // 左クリックが離されたらチャージ解除
            if (Input.GetMouseButtonUp(0))
            {
                ChangeState(STATE_BOW.BOW_RESET);       // チャージをリセットする
                ChangeState(STATE_BOW.BOW_NORMAL);      // 通常状態に変更する
                Destroy(objArrow[nCurrentArrowSetNum].gameObject);
            }

            // チャージ中に右クリックが押されたら発射
            if (Input.GetMouseButtonDown(1))
            {
                ChangeState(STATE_BOW.BOW_SHOT);      // 発射状態に変更する
                ChangeState(STATE_BOW.BOW_RESET);       // チャージをリセットする
            }
        }
        #endregion
        //Debug.Log(g_state);
        //Debug.Log("Charge" + (int)fChargeTime);
        
        // 消費するHP量の調整
        #region adjust dec hp step
        if (Input.GetKeyDown(KeyCode.Q))
            AdjustUseHP(false);
        if (Input.GetKeyDown(KeyCode.E))
            AdjustUseHP(true);

        //Debug.Log("Step:" + nCurrentStep);
        #endregion
    }
    #endregion

    /*
    * @brief 状態を変更した時に一度だけ呼ばれる関数
    * @param changeState 変更する先の状態
    * @sa arrow::Update()
    * @details 状態を変更したいときに数値の初期化など始めに一度だけ実行する処理を入れる
    */
    #region charge state
    private void ChangeState(STATE_BOW changeState)
    {
        switch(changeState)
        {
            // 通常状態
            case STATE_BOW.BOW_NORMAL:
                g_state = STATE_BOW.BOW_NORMAL;
                break;

            // チャージ状態
            case STATE_BOW.BOW_CHARGE:
                g_state = STATE_BOW.BOW_CHARGE;
                CreateArrow();      // 矢を生成する
                break;

            // 発射状態
            case STATE_BOW.BOW_SHOT:
                g_state = STATE_BOW.BOW_SHOT;
                isShot = true;
                objPlayer.GetComponent<CHPManager>().SetHpBarAnim();
                objPlayer.GetComponent<CSenaPlayer>().DecBGHPBar(-1 * nUseHP);
                objArrow[nCurrentArrowSetNum].GetComponent<CArrow>().Shot((int)fChargeTime);        // 矢を発射する
                break;

            // 最大チャージ状態
            case STATE_BOW.BOW_CHARGEMAX:
                g_state = STATE_BOW.BOW_CHARGEMAX;
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.STOP);  // カーソルを止める
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MAXCHARGE);       // スライダーを止める
                Debug.Log("ChargeMax");
                break;

            // チャージリセット状態
            case STATE_BOW.BOW_RESET:
                g_state = STATE_BOW.BOW_RESET;
                // 矢を発射していなければHPバーをリセットする
                if (!isShot)
                    objPlayer.GetComponent<CSenaPlayer>().DecFrontHPBar(nUseHP);
                else
                    isShot = false;
                //objPlayer.GetComponent<CSenaPlayer>().ResetHPBar();
                ResetCharge();      // チャージをリセットする
                break;
        }
    }
    #endregion

    /*
    * @brief 状態ごとの更新処理
    * @param UpdateState 更新する状態
    * @sa CBow::Update()
    * @details 矢の状態を取得して毎フレーム実行する処理を書く
    */
    #region update state
    private void UpdateState(STATE_BOW UpdateState)
    {
        switch (UpdateState)
        {
            // 通常状態
            case STATE_BOW.BOW_NORMAL:
                break;

            // チャージ状態
            case STATE_BOW.BOW_CHARGE:
                fChargeTime += Time.deltaTime;
                TellChargeTime();       // チャージ時間をスライダーに伝える
                if (fChargeTime > (currentChargeStep + 1) * fValChargeTime)
                    ++currentChargeStep;        // チャージを1段階上げる
                
                // 最大チャージ段階になったら
                if (currentChargeStep >= nMaxChargeStep)
                    ChangeState(STATE_BOW.BOW_CHARGEMAX);
                //Debug.Log("ChargeTime:" + fChargeTime.ToString("F1"));
                //Debug.Log("ChargeStep:" + currentChargeStep);
                break;

            // 発射状態
            case STATE_BOW.BOW_SHOT:
                break;

            // 最大チャージ状態
            case STATE_BOW.BOW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                break;

            // チャージリセット状態
            case STATE_BOW.BOW_RESET:
                break;
        }
    }
    #endregion

    /*
    * @brief 矢を生成する
    * @details 矢を弓の子オブジェクトとして生成する
    */
    #region create arrow
    private void CreateArrow()
    {
        for (int i = 0; i < nMaxArrow; ++i)
        {
            if (objArrow[i] == null)
            {
                // 矢を武器の子オブジェクトとして出す
                objArrow[i] = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.identity);
                objArrow[i].transform.parent = this.transform;
                objArrow[i].transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                objArrow[i].transform.localScale = new Vector3(arrowSize, arrowSize, arrowSize);
                nCurrentArrowSetNum = i;
                i = nMaxArrow;
            }
        }
    }
    #endregion
    /*
    * @brief チャージ時間を伝える
    * @details 毎フレームチャージ時間をチャージスライダーに伝える
    */
    #region tell charge time
    private void TellChargeTime()
    {
        scChargeSlider.GetChargeTime(fChargeTime);
    }
    #endregion

    /*
    * @brief 最大チャージ時間を伝える
    * @return float 最大チャージ時間
    * @sa CChargeSlider::Start()
    * @details 最大チャージ時間をチャージスライダーに伝える
    */
    #region tell max charge time
    private void TellMaxChargeTime()
    {
        scChargeSlider.GetMaxChargeNum(maxChargeTime, nMaxChargeStep);
    }
    #endregion

    /*
    * @brief チャージ状態をリセットする
    * @details チャージ状態を解除した時にチャージ時間やチャージステップを初期化する
    */
    #region reset charge
    private void ResetCharge()
    {
        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.RESET);  // カーソルを元に戻す
        scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.RESET);       // スライダーを元に戻す
        fChargeTime = 0.0f;     // チャージを0にする
        currentChargeStep = 0;  // チャージ段階を0に戻す
    }
    #endregion

    /*
    * @brief 使用するHPを調整する
    * @param bool 調整の段階数を増やすかどうか(true → 増やす/ false →　減らす)
    * @details　対応のキーが押された場合、使用するHPの調整を行う
    */
    #region adjust use hp
    private void AdjustUseHP(bool add)
    {
        isAdjust = true;
        // 段階数を増やす
        if (add)
        {
            ++nCurrentStep;
            // 上限値の設定
            if (nCurrentStep > maxDecStep)
                nCurrentStep = maxDecStep;
            // 削れるHPを増やす
            //else
                //objPlayer.GetComponent<CSenaPlayer>().AddHp(-1 * nAdjustHp);
        }
        // 段階数を減らす
        else
        {
            --nCurrentStep;
            // 下限値の設定
            if (nCurrentStep < 0)
                nCurrentStep = 0;
            // 削れるHPを減らす
            //else
                //objPlayer.GetComponent<CSenaPlayer>().AddHp(nAdjustHp);
        }
    }
    #endregion

    /*
    * @brief 威力調整段階数の情報を渡す
    * @return int 威力調整段階数
    */
    #region get step
    public int GetStep()
    {
        return nCurrentStep;
    }
    #endregion

    /*
    * @brief 最大威力調整段階数の情報を渡す
    * @return int 最大威力調整段階数
    */
    #region get max step
    public int GetMaxStep()
    {
        return maxDecStep;
    }
    #endregion
}
