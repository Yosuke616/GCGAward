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
    [SerializeField] private float fResetTime;      // スライダーのリセットにかける時間
    [SerializeField] private GameObject objChargeSlider;
    [SerializeField] private Image sliderImage;
    [Header("通常時のスライダーの色")]
    [SerializeField] private Color normalColor;     // 通常時のスライダーの色
    [Header("最大チャージ時のスライダーの色")]
    [SerializeField] private Color maxChargeColor;     // 最大チャージ時のスライダーの色
    #endregion

    // 変数宣言
    #region variable
    private KIND_CHRGSLIDERMOVE kSliderMove;
    private float fShowValue;
    private float fMaxValue;
    private Slider scSlider;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        scSlider = objChargeSlider.GetComponent<Slider>();
        scSlider.value = 0.0f;
        sliderImage.color = normalColor;
    }

    // Update is called once per frame
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
    #region get max charge time
    public void GetMaxChargeTime(float maxTime)
    {
        fMaxValue = maxTime;
    }
    #endregion

}
