using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CCharactorManager
{
    #region serialize field
    [SerializeField] private GameObject sceneManager;
    //[SerializeField] private int nAddScore;
    [SerializeField] private GameObject objDamageUI;
    #endregion

    // 変数宣言
    #region variable
    private CScore scScore;     // スコアの情報格納用
    private GameObject objPlayer;
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();       // HPの初期化
        InitAtk();      // 攻撃力の初期化
        SetHPBar();
        objPlayer = GameObject.FindWithTag("Player");
        //scScore = sceneManager.GetComponent<CScore>();      // スコアの情報を取得する
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        Debug.Log("EnemyAtk" + nCurrentAtk);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // 矢が当たった場合、自身と矢を消滅させる
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // スコアを加算する
            Destroy(collision.gameObject);      // 矢を消滅させる
            // 当たった矢のダメージ数を取得する
            int DamageNum = collision.gameObject.GetComponent<CArrow>().GetArrowAtk();
            // ダメージ通知
            ChangeHp(-1 * DamageNum);
            objDamageUI.GetComponent<CDamageUI>().TellDamaged(DamageNum);
        }
    }
    #endregion
}
