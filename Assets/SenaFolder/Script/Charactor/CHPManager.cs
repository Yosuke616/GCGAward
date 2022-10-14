using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHPManager : MonoBehaviour
{
    #region serialize field
    [Header("キャラクターの最大HP")]
    [SerializeField, Range(1, 100)] public int nMaxHp;        // キャラクターのHPの最大値
    [SerializeField] public GameObject HPFrontBarGroup;
    [SerializeField] public GameObject HPBGBarGroup;
    [Header("HPバーの分割数")]
    [SerializeField] public int nValNum;        // 1マスのHP量
    #endregion

    #region variable
    [System.NonSerialized]
    public int nCurrentHp;     // 現在のHP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
    #endregion 

    #region set hp bar
    public void SetHPBar()
    {
        objFrontHPBar = new GameObject[nValNum];
        objBGHPBar = new GameObject[nValNum];
        //cBGHPBar = HPBGBar.GetComponent<CBGHPBar>();
        for (int num = 0; num < nValNum; ++num)
        {
            objFrontHPBar[num] = HPFrontBarGroup.transform.GetChild(num).gameObject;
            objFrontHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
            objBGHPBar[num] = HPBGBarGroup.transform.GetChild(num).gameObject;
            objBGHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
        }
    }
    #endregion

    /*
   * @brief 前面のHPバーを変更する
   * @param num 変更する量
   * @param BarIndex 変更するバーの番号
   * @sa 弓がチャージされたとき/敵のHPが減った時
   * @details 消費されるHPに応じてFrontHPBarのBarIndex番目の数値を変更する
　  */
    #region Add front bar
    public void AddFrontBar(int num, int BarIndex)
    {
        // FrontHPBarの値を減らす
        objFrontHPBar[BarIndex].GetComponent<CHPBar>().AddValue(num);
    }
    #endregion

    #region reset hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
    }
    #endregion 
}
