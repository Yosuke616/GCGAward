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
    private int nChangeHPBar;       // 変更するHPバーの番号(現在のHPから計算する)
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
     * @brief 変更するバーの番号の変更
     * @param num 変更する量
     * @param BarIndex 変更するバーの番号
     * @sa 弓がチャージされたとき/敵のHPが減った時
     * @details 消費されるHPに応じてFrontHPBarのBarIndex番目の数値を変更する
　  */
    #region calc change hp bar num
    public void CalcBarNum()
    {
        // HPが満タンの時、番号が1つずれるため調整する
        if (nCurrentHp == nMaxHp)
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum);
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
    public void AddFrontBar(int num)
    {
        // FrontHPBarの値を減らす
        objFrontHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(num);
    }
    #endregion

    /*
     * @brief 前面のHPバーを変更する
     * @param num 変更する量
     * @param BarIndex 変更するバーの番号
     * @sa 弓がチャージされたとき/敵のHPが減った時
     * @details 消費されるHPに応じてFrontHPBarのBarIndex番目の数値を変更する
　  */
    #region dec bg bar
    public void AddBGBar(int nDecHP)
    {
        // HPを減らす
        objBGHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
        nCurrentHp += nDecHP;
    }
    #endregion

    #region reset hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
    }
    #endregion 

    /*
     * @brief HPの変更
     * @param num HPの加算量
     * @sa ダメージをくらったとき / 回復した時
     * @details HPにnumを加算する
   　*/
    #region change hp
    public void ChangeHp(int num)
    {
        AddFrontBar(num);
        AddBGBar(num);
    }
    #endregion

    #region set hp bar animation
    public void SetHpBarAnim()
    {
        objBGHPBar[nChangeHPBar].GetComponent<CBGHPBar>().changeBarValue();
    }
    #endregion
}
