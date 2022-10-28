using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarFront : MonoBehaviour
{
    #region variable
    private int nMaxHp;               // プレイヤーの最大HP
    private GameObject objParent;           // プレイヤーのオブジェクト
    private Slider slider;
    private bool isMove;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objParent = transform.root.gameObject;
        if (objParent.name == "Canvas")
        {
            objParent = GameObject.FindWithTag("Player");
        }
        nMaxHp = objParent.GetComponent<CCharactorManager>().nMaxHp;  // プレイヤーの最大HPを取得する

        // スライダーの値の設定
        slider = GetComponent<Slider>();
        slider.maxValue = nMaxHp;         // スライダーの最大値
        slider.value = nMaxHp;            // スライダーの現在値
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMove)
            slider.value = objParent.GetComponent<CCharactorManager>().nCurrentHp;
    }

    /*
     * @brief バーの値を変更する
     * @param num 変更する量
     * @sa 矢が発射されたとき/敵に攻撃を受けた時
　  */
    #region move bar
    public void MoveBar(int num)
    {
        isMove = true;
        slider.value += num;
    }
    #endregion 

    public void ResetBar()
    {
        isMove = false;
    }


}
