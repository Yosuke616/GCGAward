using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @brief 弓のカーソル表示
 * @details 弓のチャージ時間に応じて中心によっていく演出
 */

public class CCursur : MonoBehaviour
{
    // カーソルの動き
    public enum KIND_CURSURMOVE
    {
        IDLE = 0,   // 待機状態
        MOVE,       // 動いている状態
        STOP,       // 停止状態
        RESET,      // 元に戻している状態
    }
    #region serialize field
    [SerializeField] private GameObject objCenter;      // カーソルの中心点
    #endregion

    // 変数宣言
    #region valiable
    private Vector2 fDistance;
    private bool bMove;     // 移動が必要かどうか
    private float fChargeCnt;
    private KIND_CURSURMOVE kCursurMove;
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        bMove = false;      // 移動しないようにする
        kCursurMove = KIND_CURSURMOVE.IDLE;     // 待機状態にする
        // カーソルの中心点との座標を比較して自身の位置を把握する
        calcPosition();

        #region debug log
        //if (fDistance.x < 0)
        //    Debug.Log("left");
        //else if (fDistance.x > 0)
        //    Debug.Log("right");
        //else if (fDistance.y > 0)
        //    Debug.Log("up");
        //else if (fDistance.y < 0)
        //    Debug.Log("bottom");
        #endregion

    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        switch(kCursurMove)
        {
            // 動いてる時
            case KIND_CURSURMOVE.MOVE:
                MoveCursur();
                break;
            // 停止している時
            case KIND_CURSURMOVE.STOP:
                break;
            // 元に戻している時
            case KIND_CURSURMOVE.RESET:
                break;
            // 待機している時
            case KIND_CURSURMOVE.IDLE:
                break;
        }
    }

    /*
    * @brief カーソルの位置を把握する
    * @sa CCursur::Start()
    * @detail 周りのカーソルは中心点とX座標かY座標のどちらかのみ異なっているということを利用して自身の位置を把握する
    */
    #region calc position
    private void calcPosition()
    {
        if (transform.position.x != objCenter.transform.position.x)
            fDistance.x = transform.position.x - objCenter.transform.position.x;
        else if (transform.position.y != objCenter.transform.position.y)
            fDistance.y = transform.position.y - objCenter.transform.position.y;
        else
            Debug.Log("<color=red>calcDistanceError</color>");
    }
    #endregion

    /*
    * @brief カーソルを移動するかどうか指示を出す
    * @sa CBow::Update()
    * @detail 指示が出た時一度だけ呼ばれる
    */
    #region notificate bow state
    public void setCursur(KIND_CURSURMOVE cursurMove)
    {
        switch(cursurMove)
        {
            case KIND_CURSURMOVE.MOVE:
                kCursurMove = KIND_CURSURMOVE.MOVE;     // 動いている状態にする
                break;

            case KIND_CURSURMOVE.STOP:
                kCursurMove = KIND_CURSURMOVE.STOP;     // 停止状態にする
                StopCursur();       // カーソルをその場で止める
                break;

            case KIND_CURSURMOVE.RESET:
                kCursurMove = KIND_CURSURMOVE.RESET;     // 元に戻す状態にする
                ResetCursur();      // カーソルを戻す
                break;

            case KIND_CURSURMOVE.IDLE:
                kCursurMove = KIND_CURSURMOVE.IDLE;      // 元に戻す状態にする
                // 何もしない
                break;
        }
    }
    #endregion

    /*
    * @brief カーソルを動かす
    * @sa CCursur::notifBowState()
    * @detail 
    */
    #region move cursur
    private void MoveCursur()
    {
        Debug.Log("MoveCursur");
        if (fDistance.x != 0.0f)
        {
            Vector3 pos = transform.position;
            pos.x -= (fDistance.x / fChargeCnt) * Time.deltaTime;
            transform.position = pos;
        }
        else if (fDistance.y != 0.0f)
        {
            Vector3 pos = transform.position;
            pos.y -= (fDistance.y / fChargeCnt) * Time.deltaTime;
            transform.position = pos;
        }
    }
    #endregion

    /*
    * @brief カーソルを止める
    * @sa CCursur::notifBowState()
    * @detail 
    */
    #region stop cursur
    private void StopCursur()
    {
        Debug.Log("StopCursur");
    }
    #endregion

    /*
    * @brief カーソルを戻す
    * @sa CCursur::notifBowState()
    * @detail 
    */
    #region move cursur
    private void ResetCursur()
    {
        Debug.Log("ResetCursur");
    }
    #endregion

    /*
   * @brief 最大チャージ時間の取得
   * @sa CBow::Start();
   * @detail 
   */
    #region set charge max time
    public void SetChargeMaxTime(float time)
    {
        fChargeCnt = time;
    }
    #endregion

}
