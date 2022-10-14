using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaPlayer : MonoBehaviour
{
    // プレイヤーの状態
    #region plater state
    public enum PLAYERSTATE
    {
        PLAYER_ALIVE = 0,       // 生存状態
        PLAYER_DEAD,            // 死亡状態
    }
    #endregion

    #region serialize field
    [Header("プレイヤーの最大HP")]
    [SerializeField,Range(1,100)] private int nMaxHp;        // プレイヤーのHPの最大値
    [Header("HPバーの分割数")]
    [SerializeField] private int nValNum;        // 1マスのHP量
    
    [SerializeField] private GameObject prefabHPBar;        // HPバーのプレハブ
    [SerializeField] private GameObject HPFrontBarGroup;
    [SerializeField] private GameObject HPBGBarGroup;
    [SerializeField] private GameObject DeadEffect;
    #endregion

    // 変数宣言
    #region variable
    private int nCurrentHp;     // 現在のHP
    PLAYERSTATE playerState;
    private GameObject[] objFrontHPBar;
    private GameObject[] objBGHPBar;
    private int nChangeHPBar;       // 変更するHPバーの番号(現在のHPから計算する)
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        nCurrentHp = nMaxHp;     // HPの初期化
        playerState = PLAYERSTATE.PLAYER_ALIVE;     // 生存状態に設定する
        SetHPBar();     // HPバーUIの情報を取得する
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        UpdateState(playerState);
        Debug.Log(nCurrentHp);

        if (Input.GetKeyDown(KeyCode.K))
            nCurrentHp = 0;

        if (nCurrentHp <= 0)
            Debug.Log("Dead");
    }
    #endregion

    /*
    * @brief 状態の更新(毎フレーム実行される)
    * @param PLAYERSTATE プレイヤーの状態
    * @sa CPlayer::Update
    * @details プレイヤーの状態を取得し、状態に合わせた更新処理を実行する
  　*/
    #region update state
    private void UpdateState(PLAYERSTATE state)
    {
        switch (state)
        {
            // 生存状態の時
            case PLAYERSTATE.PLAYER_ALIVE:
                // Zキー→HPを減らす(デバッグ用)
                #region debug dec hp
                //if (Input.GetKeyDown(KeyCode.Z))
                //{
                //    AddHp(-1);
                //}
                #endregion

                // 変更するHPバーの番号の計算
                // HPが満タンの時、番号が1つずれるため調整する
                if (nCurrentHp == nMaxHp)
                    nChangeHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;     
                else
                    nChangeHPBar = nCurrentHp / (nMaxHp / nValNum);   
                // HPが0になったら死亡状態に変更する
                if (nCurrentHp <= 0)
                    ChangeState(PLAYERSTATE.PLAYER_DEAD);
                break;
            // 死亡状態の時
            case PLAYERSTATE.PLAYER_DEAD:
                break;
        }
    }
    #endregion

    /*
    * @brief 弓がチャージされたときに実行する処理
    * @param nDecHP HPの消費量
    * @sa 弓がチャージされたとき
    * @details 消費されるHPに応じてFrontHPBarの数値を変更する
 　  */
    #region set hp bar
    private void SetHPBar()
    {
        objFrontHPBar = new GameObject[nValNum];
        objBGHPBar = new GameObject[nValNum];
        //cBGHPBar = HPBGBar.GetComponent<CBGHPBar>();
        for (int num = 0; num < nValNum; ++num)
        {
            objFrontHPBar[num] = HPFrontBarGroup.transform.GetChild(num).gameObject;
            objFrontHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
            objBGHPBar[num] = HPBGBarGroup.transform.GetChild(num).gameObject;
            objBGHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
        }
    }
    #endregion

    /*
    * @brief 弓がチャージされたときに実行する処理
    * @param nDecHP HPの消費量
    * @sa 弓がチャージされたとき
    * @details 消費されるHPに応じてFrontHPBarの数値を変更する
 　  */
    #region dec front bar
    public void DecFrontBar(int nDecHP)
    {
        // FrontHPBarの値を減らす
        objFrontHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
    }
    #endregion

    /*
    * @brief 弓が発射されたときに実行する処理
    * @param nDecHP HPの消費量
    * @sa 弓が発射されたとき
    * @details 実際のHPを減らす
 　  */
    #region dec bg bar
    public void DecBGBar(int nDecHP)
    {
        // HPを減らす
        objBGHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
        nCurrentHp += nDecHP;
    }
    #endregion

   

    /*
    * @brief 状態の更新(状態が変更されたときに1度だけ実行される)
    * @param PLAYERSTATE 変更先の状態
    * @sa CPlayer::UpdateState
    * @details プレイヤーの状態を取得し、状態に合わせた処理を実行する
  　*/
    #region change state
    private void ChangeState(PLAYERSTATE state)
    {
        switch (state)
        {
            // 生存状態に変更する時
            case PLAYERSTATE.PLAYER_ALIVE:
                // 特に何もしない(復活とかそういう仕様があったら追加する)
                playerState = PLAYERSTATE.PLAYER_ALIVE;
                break;
            // 死亡状態の時
            case PLAYERSTATE.PLAYER_DEAD:
                playerState = PLAYERSTATE.PLAYER_DEAD;
                Instantiate(DeadEffect, transform.position, Quaternion.identity);
                StartCoroutine("DestroyPlayer");
                Debug.Log("<color=red>GAMEOVER</color>");
                break;
        }

    }
    #endregion


    /*
     * @brief HPの加算
     * @param num HPの加算量
     * @sa ダメージをくらったとき
     * @details HPにnumを加算する
   　*/
    #region add hp
    public void AddHp(int num)
    {
        DecFrontBar(num);
        DecBGBar(num);
    }
    #endregion

    /*
    * @brief HPバーのリセット
    * @param num HPの加算量
    * @sa ダメージをくらったとき
    * @details HPにnumを加算する
  　*/
    #region reset hp bar
    public void ResetHPBar()
    {
        objFrontHPBar[nChangeHPBar].GetComponent<CFrontHPBar>().ResetBarValue();
    }
    #endregion

    #region set hp bar
    public void SetHpBar()
    {
        objBGHPBar[nChangeHPBar].GetComponent<CBGHPBar>().changeBarValue();
    }
    #endregion

    /*
     * @brief HPの取得
     * @return int プレイヤーのHP
 　  */
    #region get hp
    public int GetHp()
    {
        return nCurrentHp;
    }
    #endregion

    #region set hp
    public void SetHp(int num)
    {
        nCurrentHp += num;
    }
    #endregion

    #region destroy player

    private IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    #endregion 

}
