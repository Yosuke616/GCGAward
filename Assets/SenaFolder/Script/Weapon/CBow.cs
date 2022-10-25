using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Effekseer;

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
        BOW_COLLDOWN,         // クールダウン状態
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
    [Header("威力の段階を1上げるごとに増える攻撃力")]
    [SerializeField] private int nAddAtk;
    [Header("攻撃力の初期値")]
    [SerializeField] private int nDefAtk;
    [Header("構えてるときのキラキラ音")]
    [SerializeField] private AudioClip seCharge;
    [Header("段階が上がった時に鳴らす音")]
    [SerializeField] private AudioClip[] seUpStep;
    [Header("発射音")]
    [SerializeField] private AudioClip seShot;
    [Header("クールダウンタイム")]
    [SerializeField] private float fDownTime;
    #endregion

    #region variable
    private STATE_BOW g_state;              // 弓の状態
    public const int nMaxArrow = 10;
    private GameObject[] objArrow = new GameObject[nMaxArrow];            // 弓オブジェクト
    private float fChargeTime;              // チャージボタンを押している時間 
    private GameObject[] objCursur;         // カーソル
    private float maxChargeTime;            // 最大チャージ時間(Initで計算して格納する)
    private int currentChargeStep;        // 現在のチャージ段階数
    private int nOldStep = 0;                   // 過去のチャージ段階数
    private bool isAdjust;                  // 使用するHPを調整したかどうか
    private int nCurrentAtkStep;            // 現在の威力段階数
    private int nCurrentArrowSetNum = 0;    // 現在構えている矢の番号
    //private int nUseHP = 0;                 // 矢を撃つのに使用するHP
    private bool isShot = false;            // 矢を撃ったかどうか
    private AudioSource audioSource;
    private GameObject objString;
    private EffekseerEmitter effString;
    private float fTimer;
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
        currentChargeStep = 0;
        nCurrentAtkStep = 0;       // 威力段階数の初期化

        isAdjust = false;       // 使用HP未調整状態にする
        audioSource = GetComponent<AudioSource>();
        fTimer = 0.0f;
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
        if (g_state != STATE_BOW.BOW_COLLDOWN)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeState(STATE_BOW.BOW_CHARGE);    // チャージ状態に変更する
            }
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
                //ChangeState(STATE_BOW.BOW_COLLDOWN);    // クールダウンタイム状態に遷移する
                //ChangeState(STATE_BOW.BOW_NORMAL);      // 通常状態に変更する
                Destroy(objArrow[nCurrentArrowSetNum].gameObject);
            }

            // チャージ中に右クリックが押されたら発射
            if (Input.GetMouseButtonDown(1))
            {
                ChangeState(STATE_BOW.BOW_SHOT);      // 発射状態に変更する
                //ChangeState(STATE_BOW.BOW_COLLDOWN);    // クールダウンタイム状態に遷移する
                //ChangeState(STATE_BOW.BOW_RESET);       // チャージをリセットする
            }
        }
        #endregion
        Debug.Log(g_state);
        Debug.Log("fTimer:" + fTimer);
        //Debug.Log("Charge" + (int)fChargeTime);
        
        // 消費するHP量の調整
        #region adjust dec hp step
        if (Input.GetKeyDown(KeyCode.Q))
            AdjustUseHP(false);
        if (Input.GetKeyDown(KeyCode.E))
            AdjustUseHP(true);

        //Debug.Log("Step:" + nCurrentAtkStep);
        #endregion

        //Debug.Log("威力段階数" + nCurrentAtkStep);
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

                // 弦の形を変更する
                //ChangeStringShape();
                objString = transform.Find("eff_string").gameObject;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
                effString.enabled = true;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(0);
                effString.enabled = false;

                // カーソルを動かす
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.MOVE); 
                
                // チャージスライダーを動かす
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MOVE);      

                // 威力分を加える
                int nUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(-1 * nUseHP);

                nOldStep = nCurrentAtkStep;

                // 効果音再生
                audioSource.PlayOneShot(seUpStep[currentChargeStep]);
                audioSource.PlayOneShot(seCharge);
                break;

            // 発射状態
            case STATE_BOW.BOW_SHOT:
                g_state = STATE_BOW.BOW_SHOT;
                isShot = true;
                audioSource.PlayOneShot(seShot);        // 効果音の再生
                //objPlayer.GetComponent<CCharactorManager>().SetHpBarAnim();
                //objPlayer.GetComponent<CSenaPlayer>().DecBGHPBar(-1 * nShotUseHP);

                int nShotUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                // PlayerのHPを発射に使うHP+威力調整に使うHP分減らす
                objPlayer.GetComponent<CCharactorManager>().ChangeHP(-1 * nShotUseHP);
                int nAtkValue = nDefAtk + nAddAtk * nCurrentAtkStep;                 // 矢の攻撃力
                objArrow[nCurrentArrowSetNum].GetComponent<CArrow>().Shot((int)fChargeTime, nAtkValue); // 矢を発射する
                ChangeState(STATE_BOW.BOW_RESET);    // リセットする
                break;

            // 最大チャージ状態
            case STATE_BOW.BOW_CHARGEMAX:
                g_state = STATE_BOW.BOW_CHARGEMAX;
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.STOP);  // カーソルを止める
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MAXCHARGE);       // スライダーを止める

                //Debug.Log("ChargeMax");
                break;

            // チャージリセット状態
            case STATE_BOW.BOW_RESET:
                g_state = STATE_BOW.BOW_RESET;

                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(0);
                effString.enabled = true;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
                effString.enabled = false;

                ChangeState(STATE_BOW.BOW_COLLDOWN);    // クールダウンタイム状態に遷移する


                // 矢を発射していなければHPバーをリセットする
                if (!isShot)
                {
                    int nResetUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                    objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(nResetUseHP);
                }
                else
                    isShot = false;
                //objPlayer.GetComponent<CSenaPlayer>().ResetHPBar();
                ResetCharge();      // チャージをリセットする
                nCurrentAtkStep = 0;            // 段階数をリセットする
                break;

            // クールダウン状態
            case STATE_BOW.BOW_COLLDOWN:
                g_state = STATE_BOW.BOW_COLLDOWN;
                fTimer = 0.0f;              // タイマーの初期化
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

                Debug.Log("現在のステップ:" + nCurrentAtkStep);
                // 段階数が変わった時HPバーを修正する
                if (nOldStep != nCurrentAtkStep)
                {
                    Debug.Log("前ステップ:" + nOldStep);
                    Debug.Log("StepChange");
                    int nChargeUseHP = nAdjustHp * (nCurrentAtkStep - nOldStep);
                    objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(-1 * nChargeUseHP);
                }
                if (fChargeTime > (currentChargeStep + 1) * fValChargeTime)
                {
                    ++currentChargeStep;        // チャージを1段階上げる
                    audioSource.PlayOneShot(seUpStep[currentChargeStep]);
                }

                // 最大チャージ段階になったら
                if (currentChargeStep >= nMaxChargeStep)
                    ChangeState(STATE_BOW.BOW_CHARGEMAX);

                nOldStep = nCurrentAtkStep;
                //Debug.Log("ChargeTime:" + fChargeTime.ToString("F1"));
                //Debug.Log("ChargeStep:" + currentChargeStep);
                break;

            // 発射状態
            case STATE_BOW.BOW_SHOT:
                break;

            // 最大チャージ状態
            case STATE_BOW.BOW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                // 段階数が変わった時HPバーを修正する
                if (nOldStep != nCurrentAtkStep)
                {
                    int nChargeUseHP = nAdjustHp * (nCurrentAtkStep - nOldStep);
                    objPlayer.GetComponent<CSenaPlayer>().ChangeHPFront(-1 * nChargeUseHP);
                }
                nOldStep = nCurrentAtkStep;

                break;

            // チャージリセット状態
            case STATE_BOW.BOW_RESET:
                break;

            // クールダウン状態
            case STATE_BOW.BOW_COLLDOWN:
                fTimer += Time.deltaTime;              // タイマー更新

                // クールダウンタイムが終了したら通常状態に戻る
                if(fTimer > fDownTime)
                {
                    ChangeState(STATE_BOW.BOW_NORMAL);
                }
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
        scChargeSlider.GetChargeTime(fChargeTime, currentChargeStep);
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
            ++nCurrentAtkStep;
            // 上限値の設定
            if (nCurrentAtkStep > maxDecStep)
                nCurrentAtkStep = maxDecStep;
            // 削れるHPを増やす
            //else
                //objPlayer.GetComponent<CSenaPlayer>().AddHp(-1 * nAdjustHp);
        }
        // 段階数を減らす
        else
        {
            --nCurrentAtkStep;
            // 下限値の設定
            if (nCurrentAtkStep < 0)
                nCurrentAtkStep = 0;
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
        return nCurrentAtkStep;
    }
    #endregion

    /*
     * @brief チャージ段階数の情報を渡す
     * @return int チャージ段階数
    */
    #region get charge step
    public int GetChargeStep()
    {
        return currentChargeStep;
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
    
    /*
    * @brief 弦の形を変更する
    */
    //#region change string shape
    //private void ChangeStringShape(bool flg)
    //{
    //    GameObject objString = transform.Find("eff_string").gameObject;
    //    EffekseerEmitter effString;
    //    effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
    //    effString.enabled = true;
    //    effString = objString.GetComponent<CEffectManager>().GetEmitterEff((0);
    //    effString.enabled = false;
    //}
    //#endregion
}
