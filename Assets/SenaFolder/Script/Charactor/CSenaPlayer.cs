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
    [SerializeField] private GameObject HPBarGroup;
    [SerializeField] private GameObject HPBarStaging;
    #endregion

    // 変数宣言
    #region variable
    private int nCurrentHp;     // 現在のHP
    PLAYERSTATE playerState;
    private GameObject[] objHPBar;
    private CBGHPBar cBGHPBar;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentHp = nMaxHp;     // HPの初期化
        playerState = PLAYERSTATE.PLAYER_ALIVE;     // 生存状態に設定する
        objHPBar = new GameObject[nValNum];
        cBGHPBar = HPBarStaging.GetComponent<CBGHPBar>();
        for (int num = 0; num < objHPBar.Length; ++num)
        {
            objHPBar[num] = HPBarGroup.transform.GetChild(num).gameObject;
            objHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
        }
        HPBarStaging.GetComponent<CHPBar>().SetHpBarParam(0, nMaxHp / nValNum);

        //SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(playerState);
        Debug.Log(nCurrentHp);
    }

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
                //if (Input.GetKeyDown(KeyCode.Z))
                //{
                //    AddHp(-1);
                //}
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
                Debug.Log("<color=red>GAMEOVER</color>");
                break;
        }

    }
    #endregion

    /*
     * @brief HPバーのセット
     * @param setNum 設置するHPバーの個数
     * @sa CPlayer::Start
     * @details HPの分割数を設定し、連続してHPバーを設置する
   　*/
    #region set hp UI
    private void SetHpUI()
    {
        //for (int num = 0; num < 5; ++num)
        //{
        //    GameObject hpBar = Instantiate(prefabHPBar);
        //    hpBar.GetComponent<CHPBar>().SetHpBarParam(num);
        //    hpBar.transform.SetParent()
        //}
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
        //nCurrentHp += num;
        //int changeBarNum = nCurrentHp / (nMaxHp / nValNum);
        objHPBar[0].GetComponent<CHPBar>().AddValue(num);
        HPBarStaging.GetComponent<CHPBar>().AddValue(num);
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
        objHPBar[0].GetComponent<CFrontHPBar>().ResetBarValue();
    }
    #endregion

    #region set hp bar
    public void SetHpBar()
    {
        cBGHPBar.changeBarValue();
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
}
