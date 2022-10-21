using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPBar))]
#endif

public class CFrontHPBar : CHPBar
{
    #region variable
    private int nOldValue;
    #endregion 

    private void Update()
    {
        Debug.Log(nNumber + "番目:,スライダーの値" + nCurrentValue);
    }

    /*
     * @brief HPの加算
     * @param num HPの加算量
     * @sa ダメージをくらったとき
     * @details HPにnumを加算する
   　*/
    #region add value
    public override void AddValue(int num)
    {
        nOldValue = nCurrentValue;
        nCurrentValue += num;
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion

    /*
     * @brief バーの数値をリセットする
     * @sa チャージ状態を解除したとき
   　*/
    #region reset bar value
    public void ResetBarValue()
    {
        nCurrentValue = objPlayer.GetComponent<CSenaPlayer>().GetHp();
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion
}
