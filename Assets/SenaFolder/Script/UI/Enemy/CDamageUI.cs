using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageUI : MonoBehaviour
{
    #region serialize field
    [Header("ダメージ数UIの色")]
    [SerializeField] private Color textColor;
    [SerializeField] private GameObject objDamageUI;
    #endregion

    #region valiable
    private int nShowNum = 0;           // 表示する数値
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * @brief ダメージを受けた通知を受け取る
     * @param DamageNum 受けたダメージ数
     * @sa オブジェクトがダメージを受けた時
     * @details ダメージを受けたオブジェクトからダメージ量を受け取る
    */
    #region tell damaged
    public void TellDamaged(int DamageNum)
    {
        nShowNum = DamageNum;
        Instantiate(objDamageUI);
    }
    #endregion
}
