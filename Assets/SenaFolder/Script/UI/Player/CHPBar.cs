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
    private int nCurrentValue;          // ���݂̃X���C�_�[�̒l
    private int nMaxValue;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        hpBarImage.color = normalColor;
        nCurrentValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    #endregion
}
