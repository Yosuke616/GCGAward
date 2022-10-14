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
    [Header("HPバー演出の開始時間")]
    [SerializeField, Range(0.1f, 10.0f)] private float fBarDelay;
    [Header("HPバー演出の削れ度合い")]
    [SerializeField, Range(0.01f, 1.0f)] private float fBarStagingValue;
    #endregion

    #region variable
    private float fSetValue;     // セットする最終数値
    private bool isStaging = false;     // 減少演出中か
    private float fDecNum;
    private float fStartValue;
    private float fCurrentValue;
    #endregion

    void Update()
    {
        if(isStaging)
        {
            fCurrentValue -= fDecNum;       // 減少量分、数値を変更する
            SetValue(fCurrentValue, nMaxValue);

            // セットする最終数値に到達したら演出を終了する
            if(fCurrentValue < fSetValue)
                isStaging = false;
        }
    }

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
        yield return new WaitForSeconds(fBarDelay);
        //Debug.Log("stagingHPBar");
        StagingBar(nCurrentValue, nMaxValue);       // セットしたい数値を入れる
    }
    #endregion

    #region staging bar
    private void StagingBar(int num, int nMax)
    {
        isStaging = true; 
        fStartValue = scHPSlider.value;
        fCurrentValue = fStartValue * nMax;
        fSetValue = num;
        fDecNum = fBarStagingValue;       // セットしたい数値 / 演出継続時間で1フレームで減らす量を計測する
    }
    #endregion
}
