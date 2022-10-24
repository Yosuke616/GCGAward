using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPBar))]
#endif

public class CFrontHPBar : CHPBar
{
    #region variable
    private int nOldValue;
    #endregion 

    private void Update()
    {
        Debug.Log(nNumber + "�Ԗ�:,�X���C�_�[�̒l" + nCurrentValue);
    }

    /*
     * @brief HP�̉��Z
     * @param num HP�̉��Z��
     * @sa �_���[�W����������Ƃ�
     * @details HP��num�����Z����
   �@*/
    #region add value
    public override void AddValue(int num)
    {
        nOldValue = nCurrentValue;
        nCurrentValue += num;
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion

    /*
     * @brief �o�[�̐��l�����Z�b�g����
     * @sa �`���[�W��Ԃ����������Ƃ�
   �@*/
    #region reset bar value
    public void ResetBarValue()
    {
        nCurrentValue = objPlayer.GetComponent<CSenaPlayer>().GetHp();
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion
}
