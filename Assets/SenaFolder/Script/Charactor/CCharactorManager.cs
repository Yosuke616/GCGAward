using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCharactorManager : MonoBehaviour
{
    #region serialize field
    [Header("キャラクターの最大HP")]
    [SerializeField, Range(1, 100)] public int nMaxHp;        // キャラクターのHPの最大値
    [SerializeField] public GameObject HPFrontBarGroup;
    [SerializeField] public GameObject HPBGBarGroup;
    [Header("HPバーの分割数")]
    [SerializeField] public int nValNum;        // 1マスのHP量
    //[Header("攻撃力のデフォルト値")]
    //[SerializeField] private int nDefAtk;       // 攻撃力の初期値
    #endregion

    #region variable
    [System.NonSerialized]
    public int nCurrentHp;     // 現在のHP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
    private int nChangeHPBar;       // 変更するHPバーの番号(現在のHPから計算する)
    private int nChangeFrontBarIndex = 0;       // 変更するHPバーの番号(現在のHPから計算する)
    private int nCurrentFrontVal = 0;
    [System.NonSerialized]
    public int nCurrentAtk;                // 攻撃力
    #endregion

    /*
     * @brief HPの初期化
　  */
    #region init hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
        nCurrentFrontVal = nMaxHp;
    }
    #endregion

    /*
     * @brief 攻撃力の初期化
　  */
    #region init atk
    public void InitAtk()
    {
        //nCurrentAtk = nDefAtk;
    }
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
    #region calc change  bar num
    public virtual void CalcBarNum()
    {
        // HPが満タンの時、番号が1つずれるため調整する
        if (nCurrentHp == nMaxHp)
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum);
    }
    #endregion

    /*
     * @brief 変更するバーの番号の変更
     * @param num 変更する量
     * @param BarIndex 変更するバーの番号
     * @sa 弓がチャージされたとき/敵のHPが減った時
     * @details 消費されるHPに応じてFrontHPBarのBarIndex番目の数値を変更する
　  */
    #region calc front  bar num
    public virtual void CalcFrontBarNum()
    {
        // HPが満タンの時、番号が1つずれるため調整する
        if (nCurrentFrontVal == nMaxHp)
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum);
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
        //nCurrentFrontVal += num;
        float remain = objFrontHPBar[nChangeFrontBarIndex].GetComponent<Slider>().value;
        float perHPBar =  nMaxHp / nValNum;
        float ChangeValue = Mathf.Abs((float)num / perHPBar);
        // 該当のHPバーの残り量が変更する値より少ない場合、HPバーをまたぐ処理を行う
        if (remain < ChangeValue)
        {
            objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(-1 * (int)(remain * perHPBar));
            float dif = ChangeValue - remain;
            // 減らしきれなかった分を次のHPバーで減らす
            if (num < 0)
            {
                objFrontHPBar[nChangeFrontBarIndex - 1].GetComponent<CHPBar>().AddValue(-1 * (int)(dif * perHPBar));
            }
            else
            {
                //nChangeHPBar--;
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue((int)(dif * perHPBar));
            }
        }
        else
            // FrontHPBarの値を減らす
            objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
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
        SetHpBarAnim();
        AddBGBar(num);
    }
    #endregion

    #region set hp bar animation
    public void SetHpBarAnim()
    {
        objBGHPBar[nChangeHPBar].GetComponent<CBGHPBar>().changeBarValue();
    }
    #endregion

    /*
     * @brief 現在の攻撃力の情報を渡す
     * @return int 攻撃力
     * @sa 自身の攻撃が当たった時に相手の方で呼ばれる
   　*/
    #region get atk
    public int GetAtk()
    {
        return nCurrentAtk;
    }
    #endregion
}
