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
    [Header("HP�o�[���o�̒x������")]
    [SerializeField, Range(0.1f, 10.0f)] private float fHPBarDelay;
    #endregion

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
        yield return new WaitForSeconds(fHPBarDelay);
        Debug.Log("stagingHPBar");
        SetValue(nCurrentValue, nMaxValue);
    }
    #endregion
}
