using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPBar))]
#endif

public class CBGHPBar : CHPBar
{
    #region serialize field
    [Header("HPバー演出の遅延時間")]
    [SerializeField, Range(0.1f, 10.0f)] private float fHPBarDelay;
    #endregion

    /*
     * @brief バーの数値が変化した通知を受け取る
     * @sa プレイヤーがダメージを受けた時
     * @details 通知を受け取った時に演出コルーチンを開始する
    */
    #region staging hp bar
    public void changeBarValue()
    {
        // コルーチンの起動
        StartCoroutine(DelayStaging());
    }
    #endregion

    #region delay staging
    private IEnumerator DelayStaging()
    {
        // 指定した時間待つ
        yield return new WaitForSeconds(fHPBarDelay);
        Debug.Log("stagingHPBar");
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion
}
