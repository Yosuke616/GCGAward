using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBar : MonoBehaviour
{
    #region serialize field
    [Header("�ʏ펞��HP�o�[�̐F")]
    [SerializeField] private Color normalColor;          // �ʏ펞�̃X���C�_�[�̐F
    [Header("HP�����Ȃ��Ƃ���HP�o�[�̐F")]
    [SerializeField] private Color maxChargeColor;       // �ő�`���[�W���̃X���C�_�[�̐F
    [Header("HP��0�ɂȂ����Ƃ���")]
    [SerializeField] private Image hpBarImage;           // HP�o�[�̃e�N�X�`��
    #endregion

    #region variable
    private Slider scHPSlider;
    private int nNumber;                // ���Ԗڂ�HP�o�[��
    [System.NonSerialized]
    public int nCurrentValue;          // ���݂̃X���C�_�[�̒l
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
     * @brief HP�o�[�̌ʐݒ�
     * @param setNumber ���Ԗڂ�HP�o�[��
     * @sa CPlayer::SetHpUI
     * @details �X��HP�o�[�ɕK�v�Ȑݒ���s��
   �@*/
    #region set HPBar param
    public void SetHpBarParam(int num, int max)
    {
        nNumber = num;      // ���Ԗڂ�HP�o�[��
        nMaxValue = max;
        nCurrentValue = max;
    }
    #endregion

    /*
     * @brief HP�o�[�̐��l�ݒ�
     * @param num �ݒ肷�鐔�l
     * @sa CPlayer::Update
     * @details �����𑫂�
   �@*/
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
