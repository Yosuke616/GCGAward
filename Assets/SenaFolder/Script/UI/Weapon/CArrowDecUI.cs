using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrowDecUI : MonoBehaviour
{
    #region serialize field
    [Header("光った時の色")]
    [SerializeField] private Color colorOn;
    #endregion

    #region variable
    private bool isSwitch;      // 光っているかどうか
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isSwitch = false;       // 光らないようにする
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * @brief 状態の更新(毎フレーム実行される)
    * @param PLAYERSTATE プレイヤーの状態
    * @sa CPlayer::Update
    * @details プレイヤーの状態を取得し、状態に合わせた更新処理を実行する
  　*/
    #region set switch on
    public void setSwitch(bool flg)
    {
        isSwitch = flg;
    }
    #endregion
}
