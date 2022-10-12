using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBar : MonoBehaviour
{
    #region serialize field
    [Header("通常時のHPバーの色")]
    [SerializeField] private Color normalColor;          // 通常時のスライダーの色
    [Header("HPが少ないときのHPバーの色")]
    [SerializeField] private Color maxChargeColor;       // 最大チャージ時のスライダーの色
    [Header("HPが0になったときの")]
    [SerializeField] private Image hpBarImage;           // HPバーのテクスチャ
    #endregion

    #region variable
    private Slider scHPSlider;
    private int nNumber;                // 何番目のHPバーか
    [System.NonSerialized]
    public int nCurrentValue;          // 現在のスライダーの値
    [System.NonSerialized]
    public int nMaxValue;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        hpBarImage.color = normalColor;
        scHPSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("value" + nCurrentValue);
    }

    /*
     * @brief HPバーの個別設定
     * @param setNumber 何番目のHPバーか
     * @sa CPlayer::SetHpUI
     * @details 個々のHPバーに必要な設定を行う
   　*/
    #region set HPBar param
    public void SetHpBarParam(int num, int max)
    {
        nNumber = num;      // 何番目のHPバーか
        nMaxValue = max;
        nCurrentValue = max;
    }
    #endregion

    /*
     * @brief HPバーの数値設定
     * @param num 設定する数値
     * @sa CPlayer::Update
     * @details 引数を足す
   　*/
    #region add value
    public virtual void AddValue(int num)
    {
        nCurrentValue += num;
    }
    #endregion
    
    #region set value
    public void SetValue(int num, int nMax)
    {
        scHPSlider.value = (float)num / (float)nMax;
    }
    #endregion
}
