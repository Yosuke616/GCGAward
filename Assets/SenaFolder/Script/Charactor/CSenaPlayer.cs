using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaPlayer : MonoBehaviour
{
    #region serialize field
    [Header("プレイヤーの最大HP")]
    [SerializeField] private int nMaxHp;        // プレイヤーのHPの最大値
    [Header("HPバー1マスのHP")]
    [SerializeField] private int nValHp;        // 1マスのHP量
    [SerializeField] private GameObject prefabHPBar;        // HPバーのプレハブ
    #endregion

    // 変数宣言
    #region variable
    private int nCurrentHp;     // 現在のHP
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentHp = 0;     // HPの初期化
        //SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

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
}
