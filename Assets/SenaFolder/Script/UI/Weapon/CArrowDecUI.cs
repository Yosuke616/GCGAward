using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CArrowDecUI : MonoBehaviour
{
    #region serialize field
    [Header("光った時の色")]
    [SerializeField] private Color colorOn;
    [Header("通常時の色")]
    [SerializeField] private Color colorOff;
    #endregion

    #region variable
    private bool isSwitch;      // 光っているかどうか
    private Image texture;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isSwitch = false;       // 光らないようにする
        // テクスチャ情報を取得する
        texture = GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * @brief スイッチの切替
    * @param bool 設定したいbool変数
    * @sa 矢の威力を変更しているとき
    * @details UIのスイッチのON/OFFを切り替える
  　*/
    #region set switch
    public void setSwitch(bool flg)
    {
        isSwitch = flg;
        changeTex(flg);
    }
    #endregion

    /*
    * @brief テクスチャを変更する
    * @param bool スイッチの状態
    * @sa CArrowDecUI::setSwitch
    * @details スイッチが切り替わった時にテクスチャを切り替える
  　*/
    #region change texture
    private void changeTex(bool flg)
    {
        switch (flg)
        {
            case true:
                texture.color = colorOn;
                break;
            case false:
                texture.color = colorOff;
                break;
        }

    }
    #endregion
}
