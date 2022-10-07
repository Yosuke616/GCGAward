using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CBow : MonoBehaviour
{
    // ��̏��
    #region state
    public enum STATE_BOW
    {
        BOW_CHARGE = 0,       // �`���[�W���
        BOW_NORMAL,           // �ʏ���
        BOW_SHOT,             // ���˂���Ă�����
        BOW_CHARGEMAX,        // �ő�`���[�W���
    }
    #endregion

    #region serialize field
    [SerializeField] private GameObject PrefabArrow;       // ��̃I�u�W�F�N�g
    [SerializeField] private GameObject spawner;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private CChargeSlider scChargeSlider;       // �`���[�W���Ԃ�\���X���C�_�[
    #endregion
    #region variable
    private STATE_BOW g_state;
    private GameObject objArrow;
    private float fChargeTime;
    private GameObject[] objCursur;          // �J�[�\��
    #endregion

    // Start is called before the first frame update
    #region init
    void Start()
    {
        g_state = STATE_BOW.BOW_NORMAL;
        fChargeTime = 0;
        TellMaxChargeTime();        // �X���C�_�[�ɍő�`���[�W���Ԃ�`����
        // �J�[�\���̃T�C�h�̃I�u�W�F�N�g��S�Ď擾����
        objCursur = GameObject.FindGameObjectsWithTag("CursurSide");
        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().SetChargeMaxTime(maxChargeTime);
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        // �X�V����
        UpdateState(g_state);
        // ���N���b�N�Ń`���[�W
        #region charge action
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < objCursur.Length; ++i)
                objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.MOVE);  // �J�[�\���𓮂���
            scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MOVE);
            ChangeState(STATE_BOW.BOW_CHARGE);      // �`���[�W��ԂɕύX����
        }
        #endregion
        // �`���[�W���ɍ��N���b�N�������ꂽ��`���[�W����
        // �`���[�W���ɉE�N���b�N�������ꂽ�甭��
        #region release action
        if (g_state == STATE_BOW.BOW_CHARGE || g_state == STATE_BOW.BOW_CHARGEMAX)
        {
            // ���N���b�N�������ꂽ��`���[�W����
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.RESET);  // �J�[�\�������ɖ߂�
                ChangeState(STATE_BOW.BOW_NORMAL);      // �ʏ��ԂɕύX����
            }

            // �`���[�W���ɉE�N���b�N�������ꂽ�甭��
            if (Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.RESET);  // �J�[�\�������ɖ߂�
                ChangeState(STATE_BOW.BOW_SHOT);      // ���ˏ�ԂɕύX����
            }
        }
        #endregion
        //Debug.Log(g_state);
        //Debug.Log("Charge" + (int)fChargeTime);
    }
    #endregion

    /*
    * @brief ��Ԃ�ύX�������Ɉ�x�����Ă΂��֐�
    * @param changeState �ύX�����̏��
    * @sa arrow::Update()
    * @details ��Ԃ�ύX�������Ƃ��ɐ��l�̏������Ȃǎn�߂Ɉ�x�������s���鏈��������
    */
    #region charge state
    private void ChangeState(STATE_BOW changeState)
    {
        switch(changeState)
        {
            // �ʏ���
            case STATE_BOW.BOW_NORMAL:
                g_state = STATE_BOW.BOW_NORMAL;
                fChargeTime = 0.0f;     // �`���[�W��0�ɂ���
                Destroy(objArrow);      // ������ł�����
                break;

            // �`���[�W���
            case STATE_BOW.BOW_CHARGE:
                g_state = STATE_BOW.BOW_CHARGE;
                // ��𕐊�̎q�I�u�W�F�N�g�Ƃ��ďo��
                objArrow = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.identity);
                objArrow.transform.parent = this.transform;
                objArrow.transform.localRotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                objArrow.GetComponent<CArrow>().Shot((int)fChargeTime);        // ��𔭎˂���
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                g_state = STATE_BOW.BOW_CHARGEMAX;
                Debug.Log("ChargeMax");
                break;
        }
    }
    #endregion

    /*
    * @brief ��Ԃ��Ƃ̍X�V����
    * @param UpdateState �X�V������
    * @sa CBow::Update()
    * @details ��̏�Ԃ��擾���Ė��t���[�����s���鏈��������
    */
    #region update state
    private void UpdateState(STATE_BOW UpdateState)
    {
        switch(UpdateState)
        {
            // �ʏ���
            case STATE_BOW.BOW_NORMAL:
                break;

            // �`���[�W���
            case STATE_BOW.BOW_CHARGE:
                fChargeTime += Time.deltaTime;
                TellChargeTime();       // �`���[�W���Ԃ��X���C�_�[�ɓ`����
                // maxChargeTime�ȏ�`���[�W����ƍő�`���[�W��Ԃɂ���
                if (fChargeTime > maxChargeTime)
                {
                    //scCursur.setCursur(CCursur.KIND_CURSURMOVE.STOP);      // �J�[�\�����~����
                    ChangeState(STATE_BOW.BOW_CHARGEMAX);
                }
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                break;
        }
    }
    #endregion

    /*
    * @brief �`���[�W���Ԃ�`����
    * @details ���t���[���`���[�W���Ԃ��`���[�W�X���C�_�[�ɓ`����
    */
    #region tell charge time
    private void TellChargeTime()
    {
        scChargeSlider.GetChargeTime(fChargeTime);
    }
    #endregion

    /*
    * @brief �ő�`���[�W���Ԃ�`����
    * @return float �ő�`���[�W����
    * @sa CChargeSlider::Start()
    * @details �ő�`���[�W���Ԃ��`���[�W�X���C�_�[�ɓ`����
    */
    #region tell max charge time
    private void TellMaxChargeTime()
    {
        scChargeSlider.GetMaxChargeTime(maxChargeTime);
    }
    #endregion
}
