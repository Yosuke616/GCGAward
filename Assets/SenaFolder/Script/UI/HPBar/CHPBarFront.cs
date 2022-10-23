using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarFront : MonoBehaviour
{
    #region variable
    private int nPlayerMaxHp;               // プレイヤーの最大HP
    private GameObject objPlayer;           // プレイヤーのオブジェクト
    private Slider slider;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの情報の取得
        objPlayer = GameObject.FindWithTag("Player").gameObject;        // プレイヤーのオブジェクトを取得する
        nPlayerMaxHp = objPlayer.GetComponent<CCharactorManager>().nMaxHp;  // プレイヤーの最大HPを取得する

        // スライダーの値の設定
        slider = GetComponent<Slider>();
        slider.maxValue = nPlayerMaxHp;         // スライダーの最大値
        slider.value = nPlayerMaxHp;            // スライダーの現在値
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * @brief バーの値を変更する
     * @param num 変更する量
     * @sa 矢が発射されたとき/敵に攻撃を受けた時
　  */
    #region move bar
    public void MoveBar(int num)
    {
        slider.value += num;
    }
    #endregion 

    
}
