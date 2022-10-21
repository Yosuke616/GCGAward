using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CCharactorManager : MonoBehaviour
{
    // プレイヤーの状態
    #region plater state
    public enum CHARACTORSTATE
    {
        CHARACTOR_ALIVE = 0,       // 生存状態
        CHARACTOR_DEAD,            // 死亡状態
    }
    #endregion
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
    public int nCurrentHp;      // 現在のHP
    [System.NonSerialized]
    public int nOldHp;          // 1フレーム前のHP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
    private int nChangeBGHPBar;       // 変更するHPバーの番号(現在のHPから計算する)
    private int nChangeFrontBarIndex = 0;       // 変更するHPバーの番号(現在のHPから計算する)
    private int nCurrentFrontVal = 0;
    [System.NonSerialized]
    public int nCurrentAtk;                // 攻撃力
    private int nPerVal;                  // スライダー1つあたりの数値
    #endregion

    /*
     * @brief HPの初期化
　  */
    #region init hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
        nOldHp = nCurrentHp;
        nCurrentFrontVal = nMaxHp;
        nPerVal = nMaxHp / nValNum;
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
    #region calc change front bar num
    public virtual void CalcFrontBarNum()
    {
        // HPが満タンの時、番号が1つずれるため調整する
        if (nCurrentHp == nMaxHp)
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum);
    }
    #endregion

    #region calc change bg bar num
    public virtual void CalcBGBarNum()
    {
        // HPが満タンの時、番号が1つずれるため調整する
        if (nCurrentHp == nMaxHp)
            nChangeBGHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeBGHPBar = nCurrentHp / (nMaxHp / nValNum);
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
        // 現在のバーの値を取得する
        int barValue = objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().nCurrentValue;
        // 体力が減っているとき現在のHPスライダーの値と減少量を比較して差分を次のスライダーに反映させる
        if (num < 0)
        {
            if(Mathf.Abs(num) > barValue)
            {
                Debug.Log("<color=red>BarIndexChange</color>");
                // 現在のバーのあるだけの値を減らす
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(-1 * barValue);
                objFrontHPBar[nChangeFrontBarIndex - 1].GetComponent<CHPBar>().AddValue(-1 * (Mathf.Abs(num) - barValue));

            }
            else
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
        }
        // 体力がリセットされるとき現在見ているスライダーの値が0の場合1つ前のスライダーの値を比較する
        else
        {
            if(barValue <= 0)
            {
                Debug.Log("<color=yellow>BarIndexChange</color>");
            }
            else
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
        }
        // 
    }
    #endregion

    
//#region Add front bar
    //public void AddFrontBar(int num)
    //{
    //    //nCurrentFrontVal += num;
    //    float remain = objFrontHPBar[nChangeFrontBarIndex].GetComponent<Slider>().value;
    //    float perHPBar = nMaxHp / nValNum;
    //    float ChangeValue = Mathf.Abs((float)num / perHPBar);
    //    // 該当のHPバーの残り量が変更する値より少ない場合、HPバーをまたぐ処理を行う
    //    if (remain < ChangeValue)
    //    {
    //        objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(-1 * (int)(remain * perHPBar));
    //        //float dif = ChangeValue - remain;
    //        // 減らしきれなかった分を次のHPバーで減らす
    //        if (num < 0)
    //        {
    //            objFrontHPBar[nChangeFrontBarIndex - 1].GetComponent<CHPBar>().AddValue(-1 * (int)(dif * perHPBar));
    //        }
    //        else
    //        {
    //            //nChangeBGHPBar--;
    //            objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue((int)(dif * perHPBar));
    //        }
    //    }
    //    else
    //        // FrontHPBarの値を減らす
    //        objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
    //}
    //#endregion
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
        objBGHPBar[nChangeBGHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
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
        objBGHPBar[nChangeBGHPBar].GetComponent<CBGHPBar>().changeBarValue();
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
