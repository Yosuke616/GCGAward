using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CHPManager
{
    #region serialize field
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private int nAddScore;
    #endregion

    // 変数宣言
    #region variable
    private CScore scScore;     // スコアの情報格納用
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        InitHP();      // HPの初期化
        SetHPBar();
        //scScore = sceneManager.GetComponent<CScore>();      // スコアの情報を取得する
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentHP" + nCurrentHp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 矢が当たった場合、自身と矢を消滅させる
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // スコアを加算する
            Destroy(collision.gameObject);      // 矢を消滅させる
            ChangeHp(-1);
        }
    }
}
