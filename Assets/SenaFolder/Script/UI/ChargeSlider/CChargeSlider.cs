using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CChargeSlider : MonoBehaviour
{
    // �X���C�_�[�̓���
    #region kind move
    public enum KIND_CHRGSLIDERMOVE
    {
        IDLE = 0,   // �ҋ@���
        MOVE,       // �����Ă�����
        STOP,       // ��~���
        RESET,      // ���ɖ߂��Ă�����
    }
    #endregion

    #region serialize field
    [Header("�X���C�_�[�̃��Z�b�g�ɂ����鎞��")]
    [SerializeField] private float fResetTime;      // �X���C�_�[�̃��Z�b�g�ɂ����鎞��
    [SerializeField] private Slider objChargeSlider;
    #endregion

    // �ϐ��錾
    #region variable
    private KIND_CHRGSLIDERMOVE kSliderMove;
    private float fShowValue;
    private float fMaxValue;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objChargeSlider.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (kSliderMove)
        {
            case KIND_CHRGSLIDERMOVE.MOVE:
                MoveSlider();       // �X���C�_�[�𓮂���
                break;

            case KIND_CHRGSLIDERMOVE.STOP:
                break;

            case KIND_CHRGSLIDERMOVE.RESET:
                ResetSlider();       // �X���C�_�[�����ɖ߂�
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                // �������Ȃ�
                break;
        }
    }

    /*
     * @brief �X���C�_�[�̓����̎w�����o��
     * @sa CBow::Update()
     * @detail �w�����o������x�����Ă΂��
    */
    #region set slider
    public void setSlider(KIND_CHRGSLIDERMOVE sliderMove)
    {
        switch (sliderMove)
        {
            case KIND_CHRGSLIDERMOVE.MOVE:
                kSliderMove = KIND_CHRGSLIDERMOVE.MOVE;     // �����Ă����Ԃɂ���
                break;

            case KIND_CHRGSLIDERMOVE.STOP:
                kSliderMove = KIND_CHRGSLIDERMOVE.STOP;     // ��~��Ԃɂ���
                StopSlider();       // �X���C�_�[�����̏�Ŏ~�߂�
                break;

            case KIND_CHRGSLIDERMOVE.RESET:
                kSliderMove = KIND_CHRGSLIDERMOVE.RESET;     // ���ɖ߂���Ԃɂ���
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                kSliderMove = KIND_CHRGSLIDERMOVE.IDLE;      // �ҋ@��Ԃɂ���
                // �������Ȃ�
                break;
        }
    }
    #endregion

    /*
   * @brief �X���C�_�[�𓮂���
   * @sa CSlider::Update()
   * @detail 
   */
    #region move slider
    private void MoveSlider()
    {
        Debug.Log("MoveSlider");
        objChargeSlider.value = fShowValue / fMaxValue;
    }
    #endregion

    /*
    * @brief �X���C�_�[���~�߂�
    * @sa CSlider::notifBowState()
    * @detail 
    */
    #region stop slider
    private void StopSlider()
    {
        Debug.Log("StopSlider");
    }
    #endregion

    /*
    * @brief �X���C�_�[��߂�
    * @sa CSlider::notifBowState()
    * @detail 
    */
    #region reset slider
    private void ResetSlider()
    {
        Debug.Log("ResetSlider");
    }
    #endregion

    /*
   * @brief �`���[�W���Ԃ��󂯎��
   * @param time �`���[�W����
   * @sa CChargeSlider::Update()
   * @details ���t���[���`���[�W���Ԃ��`���[�W�X���C�_�[�ɓ`����
   */
    #region get charge time
    public void GetChargeTime(float time)
    {
        fShowValue = time;
    }
    #endregion

    /*
  * @brief �ő�`���[�W���Ԃ��󂯎��
  * @param maxTime �ő�`���[�W����
  * @sa CChargeSlider::Update()
  * @details ���t���[���`���[�W���Ԃ��`���[�W�X���C�_�[�ɓ`����
  */
    #region get max charge time
    public void GetMaxChargeTime(float maxTime)
    {
        fMaxValue = maxTime;
    }
    #endregion

}
