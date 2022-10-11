using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CChargeSlider : MonoBehaviour
{
    // スライダーの動き
    #region kind move
    public enum KIND_CHRGSLIDERMOVE
    {
        IDLE = 0,   // 待機状態
        MOVE,       // 動いている状態
        STOP,       // 停止状態
        RESET,      // 元に戻している状態
        MAXCHARGE,  // 最大チャージ状態
    }
    #endregion

    #region serialize field
    [Header("スライダーのリセットにかける時間")]
    [SerializeField] private float fResetTime;           // スライダーのリセットにかける時間
    [SerializeField] private GameObject objChargeSlider;
    [SerializeField] private Image sliderImage;
    [Header("通常時のスライダーの色")]
    [SerializeField] private Color normalColor;          // 通常時のスライダーの色
    [Header("最大チャージ時のスライダーの色")]
    [SerializeField] private Color maxChargeColor;       // 最大チャージ時のスライダーの色
    [SerializeField] private GameObject objStepLine;     // 何段階目かを表すライン
    #endregion

    // 変数宣言
    #region variable
    private KIND_CHRGSLIDERMOVE kSliderMove;
    private float fShowValue;
    private float fMaxValue;
    private int nMaxStep;           // 表示するチャージ段階の数
    private Slider scSlider;
    private RectTransform rectTransform;        // スライダーのサイズ取得用
    private float sliderWidth;                  // スライダーの縦幅
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        scSlider = objChargeSlider.GetComponent<Slider>();
        scSlider.value = 0.0f;
        sliderImage.color = normalColor;

        // スライダーの横幅を取得する
        rectTransform = gameObject.GetComponent<RectTransform>();
        sliderWidth = rectTransform.sizeDelta.x;

        setStepLine();      // 段階を示す線を表示する
    }
    #endregion
    // Update is called once per frame
    #region update
    void Update()
    {
        switch (kSliderMove)
        {
            case KIND_CHRGSLIDERMOVE.MOVE:
                MoveSlider();       // スライダーを動かす
                break;

            case KIND_CHRGSLIDERMOVE.STOP:
                break;

            case KIND_CHRGSLIDERMOVE.RESET:
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                // 何もしない
                break;
            
            case KIND_CHRGSLIDERMOVE.MAXCHARGE:
                // 何もしない
                break;
        }
    }
    #endregion

    /*
     * @brief スライダーの動きの指示を出す
     * @sa CBow::Update()
     * @detail 指示が出た時一度だけ呼ばれる
    */
    #region set slider
    public void setSlider(KIND_CHRGSLIDERMOVE sliderMove)
    {
        switch (sliderMove)
        {
            case KIND_CHRGSLIDERMOVE.MOVE:
                kSliderMove = KIND_CHRGSLIDERMOVE.MOVE;     // 動いている状態にする
                break;

            case KIND_CHRGSLIDERMOVE.STOP:
                kSliderMove = KIND_CHRGSLIDERMOVE.STOP;     // 停止状態にする
                StopSlider();       // スライダーをその場で止める
                break;

            case KIND_CHRGSLIDERMOVE.RESET:
                kSliderMove = KIND_CHRGSLIDERMOVE.RESET;     // 元に戻す状態にする
                ResetSlider();       // スライダーを元に戻す
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                kSliderMove = KIND_CHRGSLIDERMOVE.IDLE;      // 待機状態にする
                // 何もしない
                break;

            case KIND_CHRGSLIDERMOVE.MAXCHARGE:
                kSliderMove = KIND_CHRGSLIDERMOVE.MAXCHARGE;      // 待機状態にする
                sliderImage.color = maxChargeColor;
                break;
        }
    }
    #endregion

    /*
     * @brief 段階数を表す線を表示する
     * @detail 最大段階数を取得して等間隔に線を生成する
    */
    #region set step line
    private void setStepLine()
    {
        //float startPosX = rectTransform.anchoredPosition.x - sliderWidth / 2;
        //GameObject[] objLines = new GameObject[nMaxStep];
        for (int i = 0; i < nMaxStep; ++i)
         {
            GameObject objLine = Instantiate(objStepLine);
            objLine.GetComponent<RectTransform>().position = new Vector2(0.0f, 0.0f);
            //objLine.transform.SetParent(transform, true);
        }

        // 最大段階数分線を表示する
    }
    #endregion

    /*
   * @brief スライダーを動かす
   * @sa CSlider::Update()
   * @detail 
   */
    #region move slider
    private void MoveSlider()
    {
        //Debug.Log("MoveSlider");
        scSlider.value = fShowValue / fMaxValue;
    }
    #endregion

    /*
    * @brief スライダーを止める
    * @sa CSlider::notifBowState()
    * @detail 
    */
    #region stop slider
    private void StopSlider()
    {
        //Debug.Log("StopSlider");
    }
    #endregion

    /*
    * @brief スライダーを戻す
    * @sa CSlider::notifBowState()
    * @detail 
    */
    #region reset slider
    private void ResetSlider()
    {
        //Debug.Log("ResetSlider");
        scSlider.value = 0.0f;
        sliderImage.color = normalColor;
    }
    #endregion

    /*
   * @brief チャージ時間を受け取る
   * @param time チャージ時間
   * @sa CChargeSlider::Update()
   * @details 毎フレームチャージ時間をチャージスライダーに伝える
   */
    #region get charge time
    public void GetChargeTime(float time)
    {
        fShowValue = time;
    }
    #endregion

    /*
  * @brief 最大チャージ時間を受け取る
  * @param maxTime 最大チャージ時間
  * @sa CChargeSlider::Update()
  * @details 毎フレームチャージ時間をチャージスライダーに伝える
  */
    #region get max charge time & step
    public void GetMaxChargeNum(float maxTime, int maxStep)
    {
        fMaxValue = maxTime;
        nMaxStep = maxStep;
    }
    #endregion

}
