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
        MAXCHARGE,  // �ő�`���[�W���
    }
    #endregion

    #region serialize field
    [Header("�X���C�_�[�̃��Z�b�g�ɂ����鎞��")]
    [SerializeField] private float fResetTime;           // �X���C�_�[�̃��Z�b�g�ɂ����鎞��
    [SerializeField] private GameObject objChargeSlider;
    [SerializeField] private Image sliderImage;
    [Header("�ʏ펞�̃X���C�_�[�̐F")]
    [SerializeField] private Color normalColor;          // �ʏ펞�̃X���C�_�[�̐F
    [Header("�ő�`���[�W���̃X���C�_�[�̐F")]
    [SerializeField] private Color maxChargeColor;       // �ő�`���[�W���̃X���C�_�[�̐F
    [SerializeField] private GameObject objStepLine;     // ���i�K�ڂ���\�����C��
    #endregion

    // �ϐ��錾
    #region variable
    private KIND_CHRGSLIDERMOVE kSliderMove;
    private float fShowValue;
    private float fMaxValue;
    private int nMaxStep;           // �\������`���[�W�i�K�̐�
    private Slider scSlider;
    private RectTransform rectTransform;        // �X���C�_�[�̃T�C�Y�擾�p
    private float sliderWidth;                  // �X���C�_�[�̏c��
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        scSlider = objChargeSlider.GetComponent<Slider>();
        scSlider.value = 0.0f;
        sliderImage.color = normalColor;

        // �X���C�_�[�̉������擾����
        rectTransform = gameObject.GetComponent<RectTransform>();
        sliderWidth = rectTransform.sizeDelta.x;

        setStepLine();      // �i�K����������\������
    }
    #endregion
    // Update is called once per frame
    #region update
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
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                // �������Ȃ�
                break;
            
            case KIND_CHRGSLIDERMOVE.MAXCHARGE:
                // �������Ȃ�
                break;
        }
    }
    #endregion

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
                ResetSlider();       // �X���C�_�[�����ɖ߂�
                break;

            case KIND_CHRGSLIDERMOVE.IDLE:
                kSliderMove = KIND_CHRGSLIDERMOVE.IDLE;      // �ҋ@��Ԃɂ���
                // �������Ȃ�
                break;

            case KIND_CHRGSLIDERMOVE.MAXCHARGE:
                kSliderMove = KIND_CHRGSLIDERMOVE.MAXCHARGE;      // �ҋ@��Ԃɂ���
                sliderImage.color = maxChargeColor;
                break;
        }
    }
    #endregion

    /*
     * @brief �i�K����\������\������
     * @detail �ő�i�K�����擾���ē��Ԋu�ɐ��𐶐�����
    */
    #region set step line
    private void setStepLine()
    {
        //float startPosX = rectTransform.anchoredPosition.x - sliderWidth / 2;
        //GameObject[] objLines = new GameObject[nMaxStep];
        for (int i = 0; i < nMaxStep; ++i)
         {
            GameObject objLine = Instantiate(objStepLine);
            objLine.GetComponent<RectTransform>().position = new Vector2(0.0f, 0.0f);
            //objLine.transform.SetParent(transform, true);
        }

        // �ő�i�K��������\������
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
        //Debug.Log("MoveSlider");
        scSlider.value = fShowValue / fMaxValue;
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
        //Debug.Log("StopSlider");
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
        //Debug.Log("ResetSlider");
        scSlider.value = 0.0f;
        sliderImage.color = normalColor;
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
    #region get max charge time & step
    public void GetMaxChargeNum(float maxTime, int maxStep)
    {
        fMaxValue = maxTime;
        nMaxStep = maxStep;
    }
    #endregion

}
