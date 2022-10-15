using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CCharactorManager))]
#endif

public class CSenaPlayer : CCharactorManager
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
    //[SerializeField] private GameObject prefabHPBar;        // HPバーのプレハブ
    [SerializeField] private GameObject DeadEffect;
    [SerializeField] private GameObject objWeapon;              // 武器オブジェクト
    [SerializeField] private GameObject GameOverUI;
    #endregion

    // 変数宣言
    #region variable
    private PLAYERSTATE playerState;
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();      // HPの初期化
        //InitAtk();      // 攻撃力の初期化
        playerState = PLAYERSTATE.PLAYER_ALIVE;     // 生存状態に設定する
        SetHPBar();     // HPバーUIの情報を取得する
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        UpdateState(playerState);
        //Debug.Log(nCurrentHp);

        if (Input.GetKeyDown(KeyCode.K))
            nCurrentHp = 0;

        //Debug.Log("PlayerAtk" + nCurrentAtk);
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
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    nCurrentHp = 0;
                }
                #endregion
                // 変更するバーの番号の変更
                CalcBarNum();
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
     * @brief 前面のHPバーを変更する
     * @param num 変更する量
     * @sa 弓がチャージされたとき
     * @details 消費されるHPに応じてFrontHPBarのnChangeHPBar番目の数値を変更する
　  */
    #region dec front hp bar
    public void DecFrontHPBar(int num)
    {
        AddFrontBar(num);
    }
    #endregion

    /*
     * @brief 背面のHPバーを変更する
     * @param num 変更する量
     * @sa 弓がチャージされたとき
     * @details 消費されるHPに応じてBGHPBarのnChangeHPBar番目の数値を変更する
　  */
    #region dec bg hp bar
    public void DecBGHPBar(int num)
    {
        AddBGBar(num);
    }
    #endregion

    /*
     * @brief HPバーのリセット
     * @param num HPの加算量
     * @sa ダメージをくらったとき
     * @details HPにnumを加算する
  　*/
    #region reset hp bar
    //public void ResetHPBar()
    //{
    //    objFrontHPBar[nChangeHPBar].GetComponent<CFrontHPBar>().ResetBarValue();
    //}
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
        GameOverUI.SetActive(true);
    }
    #endregion

    #region add hp
    public void AddHp(int num)
    {
        ChangeHp(num);
    }
    #endregion
}
