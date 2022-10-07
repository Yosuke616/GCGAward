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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        bMove = false;      // 移動しないようにする
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

    // Update is called once per frame
    void Update()
    {

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
    */
    #region notificate bow state
    public void setCursur(KIND_CURSURMOVE cursurMove)
    {
        switch(cursurMove)
        {
            case KIND_CURSURMOVE.MOVE:
                // カーソルを動かす
                MoveCursur();
                break;

            case KIND_CURSURMOVE.STOP:
                // カーソルをその場で止める
                StopCursur();
                break;

            case KIND_CURSURMOVE.RESET:
                // カーソルを戻す
                ResetCursur();
                break;

            case KIND_CURSURMOVE.IDLE:
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

}
