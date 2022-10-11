using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int nMaxChargeStep;
    [Header("チャージ時間(1段階)")]
    [SerializeField] private float fValChargeTime;
    [SerializeField] private CChargeSlider scChargeSlider;       // チャージ時間を表すスライダー
    #endregion
    #region variable
    private STATE_BOW g_state;              // 弓の状態
    private GameObject objArrow;            // 弓オブジェクト
    private float fChargeTime;              // チャージボタンを押している時間 
    private GameObject[] objCursur;         // カーソル
    private float maxChargeTime;            // 最大チャージ時間(Initで計算して格納する)
    private float currentChargeStep;        // 現在のチャージ段階数
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
                Destroy(objArrow);      // 矢を消滅させる
                break;

            // チャージ状態
            case STATE_BOW.BOW_CHARGE:
                g_state = STATE_BOW.BOW_CHARGE;
                // 矢を武器の子オブジェクトとして出す
                objArrow = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.identity);
                objArrow.transform.parent = this.transform;
                objArrow.transform.localRotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
                break;

            // 発射状態
            case STATE_BOW.BOW_SHOT:
                objArrow.GetComponent<CArrow>().Shot((int)fChargeTime);        // 矢を発射する
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
        scChargeSlider.GetMaxChargeTime(maxChargeTime);
    }
    #endregion

    /*
    * @brief チャージ状態をリセットする
    * @details チャージ状態を解除した時にチャージ時間やチャージステップを初期化する
    */
    private void ResetCharge()
    {
        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.RESET);  // カーソルを元に戻す
        scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.RESET);       // スライダーを元に戻す
        fChargeTime = 0.0f;     // チャージを0にする
        currentChargeStep = 0;  // チャージ段階を0に戻す
    }
}
