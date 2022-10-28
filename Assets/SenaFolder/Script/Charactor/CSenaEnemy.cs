using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CCharactorManager
{
    #region serialize field
    //[SerializeField] private GameObject sceneManager;
    //[SerializeField] private int nAddScore;
    [SerializeField] private GameObject objDamageUI;
    [SerializeField] private GameObject objHitEffect;
    [Header("敵の消滅時間")]
    [SerializeField] private float fDestroyTime;
    #endregion

    // 変数宣言
    #region variable
    private CScore scScore;     // スコアの情報格納用
    private GameObject objPlayer;
    private CHARACTORSTATE state;
    private GameObject hitEffect;

    private const string key_isDamage = "isDamage";
    private const string key_isDeath = "isDeath";
    private Animator animator;

    private WaveManager WM;

    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();       // HPの初期化
        InitAtk();      // 攻撃力の初期化
        //SetHPBar();
        objPlayer = GameObject.FindWithTag("Player");
        //scScore = sceneManager.GetComponent<CScore>();      // スコアの情報を取得する

        this.animator = GetComponent<Animator>();

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        UpdateState(state);
        //Debug.Log("EnemyAtk" + nCurrentAtk);
        
    }
    #endregion

    #region change state
    private void ChangeState(CHARACTORSTATE state)
    {
        switch(state)
        {
            // 生存状態の時
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                break;

            // 死亡状態の時
            case CHARACTORSTATE.CHARACTOR_DEAD:
                //float fLifeTime = objDamageUI.GetComponent<CDamageUI>().fLifeTime;
                //StartCoroutine("DestroyHitEffect",(objHitEffect,fLifeTime));        // 1秒後に
                //HPの回復
                CSenaPlayer obj = GameObject.FindGameObjectWithTag("Player").GetComponent<CSenaPlayer>();
                //obj.ChangeHPFront(10);
                obj.ChangeHp(10);
                WM.AddScore(100);
                WM.AddBreakEnemy();
                WM.DecEnemy();
                StartCoroutine("DestroyEnemy", fDestroyTime);
                break;
        }
    }
    #endregion

    // 毎フレーム実行される
    #region update state
    private void UpdateState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // 生存状態の時
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                //Debug.Log("arrive");
                //Debug.Log("現在のHP:" + nCurrentHp);
                // HPが0になったときに死亡状態にする
                if (nCurrentHp <= 0)
                    ChangeState(CHARACTORSTATE.CHARACTOR_DEAD);
                break;
            
            // 死亡状態の時
            case CHARACTORSTATE.CHARACTOR_DEAD:
                break;

        }
    }
    #endregion

    /*
    * @brief ヒットエフェクトの削除
    * @param GameObject ヒットエフェクトのオブジェクト
    * @details ヒットエフェクトのオブジェクトを取得して消滅させる
  　*/
    //#region destroy hit effect
    //private IEnumerator DestroyHitEffect(GameObject effect, float lifeTime)
    //{
    //    yield return new WaitForSeconds(lifeTime);
    //    Destroy(effect);    
    //}
    //#endregion

    /*
     * @brief 敵オブジェクトの消滅
     * @param float 消滅までの時間
     * @details fTime秒後に敵オブジェクトを消滅させる
　   */
    #region destroy enemy
    private IEnumerator DestroyEnemy(float fTime)
    {
        //死亡アニメーションを流す
        this.animator.SetBool(key_isDeath, true);
        yield return new WaitForSeconds(fTime);
        Destroy(gameObject);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // 矢が当たった場合、自身と矢を消滅させる
        if (collision.gameObject.tag == "Arrow")
        {
            //アニメーションを流す
            this.animator.SetBool(key_isDamage, true);
            //Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // スコアを加算する
            Destroy(collision.gameObject);      // 矢を消滅させる
            // 当たった矢のダメージ数を取得する
            int DamageNum = collision.gameObject.GetComponent<CArrow>().GetArrowAtk();
            // ヒットエフェクト再生
            hitEffect = Instantiate(objHitEffect);
            // ダメージ通知
            ChangeHp(-1 * DamageNum);
            //if(nCurrentHp > 0)
            //objDamageUI.GetComponent<CDamageUI>().TellDamaged(DamageNum);
            // ヒットカーソルの再生
            GetComponent<CEnemyDamage>().ArrowHit();

        }
        else {
            this.animator.SetBool(key_isDamage, false);
        }
    }
    #endregion
}
