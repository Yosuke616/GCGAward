using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPBar))]
#endif

public class CBGHPBar : CHPBar
{
    #region serialize field
    [Header("HP�o�[���o�̊J�n����")]
    [SerializeField, Range(0.1f, 10.0f)] private float fBarDelay;
    [Header("HP�o�[���o�̍��x����")]
    [SerializeField, Range(0.01f, 1.0f)] private float fBarStagingValue;
    #endregion

    #region variable
    private float fSetValue;     // �Z�b�g����ŏI���l
    private bool isStaging = false;     // �������o����
    private float fDecNum;
    private float fStartValue;
    private float fCurrentValue;
    #endregion

    void Update()
    {
        if(isStaging)
        {
            fCurrentValue -= fDecNum;       // �����ʕ��A���l��ύX����
            SetValue(fCurrentValue, nMaxValue);

            // �Z�b�g����ŏI���l�ɓ��B�����牉�o���I������
            if(fCurrentValue < fSetValue)
                isStaging = false;
        }
    }

    /*
     * @brief �o�[�̐��l���ω������ʒm���󂯎��
     * @sa �v���C���[���_���[�W���󂯂���
     * @details �ʒm���󂯎�������ɉ��o�R���[�`�����J�n����
    */
    #region staging hp bar
    public void changeBarValue()
    {
        // �R���[�`���̋N��
        StartCoroutine(DelayStaging());
    }
    #endregion

    #region delay staging
    private IEnumerator DelayStaging()
    {
        // �w�肵�����ԑ҂�
        yield return new WaitForSeconds(fBarDelay);
        //Debug.Log("stagingHPBar");
        StagingBar(nCurrentValue, nMaxValue);       // �Z�b�g���������l������
    }
    #endregion

    #region staging bar
    private void StagingBar(int num, int nMax)
    {
        isStaging = true; 
        fStartValue = scHPSlider.value;
        fCurrentValue = fStartValue * nMax;
        fSetValue = num;
        fDecNum = fBarStagingValue;       // �Z�b�g���������l / ���o�p�����Ԃ�1�t���[���Ō��炷�ʂ��v������
    }
    #endregion
}
