using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CCharactorManager))]
#endif

public class CSenaPlayer : CCharactorManager
{
    #region serialize field
    //[SerializeField] private GameObject prefabHPBar;        // HPバーのプレハブ
    [SerializeField] private GameObject DeadEffect;
    [SerializeField] private GameObject objWeapon;              // 武器オブジェクト
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject HPNumUI;                // HPバーの数字表記UI
    #endregion

    // 変数宣言
    #region variable
    private CHARACTORSTATE playerState;
    private CHPText hpText;
    private GameObject[] objHPTexts;

    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();      // HPの初期化
        //InitAtk();      // 攻撃力の初期化
        playerState = CHARACTORSTATE.CHARACTOR_ALIVE;     // 生存状態に設定する
        //SetHPBar();     // HPバーUIの情報を取得する

        // HPの数字表示UIオブジェクトを取得する
        var children = new GameObject[HPNumUI.transform.childCount];
        for(int i = 0; i < children.Length; ++i)
            children[i] = HPNumUI.transform.GetChild(i).gameObject;
        objHPTexts = children;
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

        //Debug.Log("HP" + nCurrentHp);
        //Debug.Log("PlayerAtk" + nCurrentAtk);
    }
    #endregion

    /*
    * @brief 状態の更新(毎フレーム実行される)
    * @param CHARACTORSTATE プレイヤーの状態
    * @sa CPlayer::Update
    * @details プレイヤーの状態を取得し、状態に合わせた更新処理を実行する
  　*/
    #region update state
    private void UpdateState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // 生存状態の時
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                // Zキー→HPを減らす(デバッグ用)
                #region debug dec hp
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    nCurrentHp = 0;
                }
                #endregion

                //CalcFrontBarNum();
                // HPが変更された場合、HP数字表示UIを変更する
                if (nCurrentHp != nOldHp)
                   
                // HPが0になったら死亡状態に変更する
                if (nCurrentHp <= 0)
                    ChangeState(CHARACTORSTATE.CHARACTOR_DEAD);
                break;
            // 死亡状態の時
            case CHARACTORSTATE.CHARACTOR_DEAD:
                break;
        }
        nOldHp = nCurrentHp;    
    }
    #endregion

    /*
    * @brief 状態の更新(状態が変更されたときに1度だけ実行される)
    * @param CHARACTORSTATE 変更先の状態
    * @sa CPlayer::UpdateState
    * @details プレイヤーの状態を取得し、状態に合わせた処理を実行する
  　*/
    #region change state
    private void ChangeState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // 生存状態に変更する時
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                // 特に何もしない(復活とかそういう仕様があったら追加する)
                playerState = CHARACTORSTATE.CHARACTOR_ALIVE;
                break;
            // 死亡状態の時
            case CHARACTORSTATE.CHARACTOR_DEAD:
                playerState = CHARACTORSTATE.CHARACTOR_DEAD;
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
     * @details 消費されるHPに応じてFrontHPBarのnChangeBGHPBar番目の数値を変更する
　  */
    #region dec front hp bar
    public void DecFrontHPBar(int num)
    {
        //AddFrontBar(num);
    }
    #endregion

    /*
     * @brief 背面のHPバーを変更する
     * @param num 変更する量
     * @sa 弓がチャージされたとき
     * @details 消費されるHPに応じてBGHPBarのnChangeBGHPBar番目の数値を変更する
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
    //    objFrontHPBar[nChangeBGHPBar].GetComponent<CFrontHPBar>().ResetBarValue();
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
        Kesu elf = GameObject.Find("ElfPlayer").GetComponent<Kesu>();
        elf.SetDeathAnim();
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
        GameOverUI.SetActive(true);
        GameOverScript GOS = GameObject.Find("EventSystem").GetComponent<GameOverScript>();
        GOS.SetUseFlg(true);
    }
    #endregion

    public override void ChangeHPFront(int num)
    {
        //nCurrentHp += num;
        for(int i = 0; i < objHPTexts.Length; ++i)
        {
            objHPTexts[i].GetComponent<CHPText>().ChangeHPNum(num);
        }
        HPFrontBar.GetComponent<CHPBarFront>().MoveBar(num);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            Kesu elf = GameObject.Find("ElfPlayer").GetComponent<Kesu>();
            elf.SetDamageAnim();

            ChangeHPFront(-10);
            ChangeHPBG(-10);
        }
    }

}
