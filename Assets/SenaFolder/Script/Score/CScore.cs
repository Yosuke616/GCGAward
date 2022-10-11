using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スコア管理
public class CScore : MonoBehaviour
{
    // 変数宣言
    #region variable
    private int g_nScore;       // スコア
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        g_nScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(g_nScore);
    }

    /*
      * @brief スコアの変更
      * @param num 加算する数(マイナスも可能)
      * @sa 敵を倒したとき
      * @details 引数の数値をスコアに加算する
    */
    public void addScore(int num)
    {
        g_nScore += num;
    }
}
