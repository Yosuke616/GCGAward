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
    }
    #endregion

    #region serialize field
    [Header("スライダーのリセットにかける時間")]
    [SerializeField] private float fResetTime;      // スライダーのリセットにかける時間
    [SerializeField] private Slider objChargeSlider;
    #endregion

    // 変数宣言
    #region variable
    private KIND_CHRGSLIDERMOVE kSliderMove;
    private float fShowValue;
    private float fMaxValue;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objChargeSlider.value = 0.0f;
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
                ResetSlider();       // スライダーを元に戻す
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
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
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                kSliderMove = KIND_CHRGSLIDERMOVE.IDLE;      // 待機状態にする
                // 何もしない
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
        Debug.Log("MoveSlider");
        objChargeSlider.value = fShowValue / fMaxValue;
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
        Debug.Log("StopSlider");
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
        Debug.Log("ResetSlider");
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
