using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        BOW_RESET,            // �`���[�W���Z�b�g���
    }
    #endregion

    #region serialize field
    [SerializeField] private GameObject PrefabArrow;       // ��̃I�u�W�F�N�g
    [SerializeField] private GameObject spawner;
    [Header("�`���[�W�ő�i�K")]
    [SerializeField, Range(1,10)] private int nMaxChargeStep = 1;
    [Header("�`���[�W����(1�i�K)")]
    [SerializeField, Range(0.1f, 10.0f)] private float fValChargeTime = 0.5f;
    [Header("���������̑傫��(0.5f�����傤�ǂ�������)")]
    [SerializeField, Range(0.1f, 1.0f)] private float arrowSize;
    [SerializeField] private GameObject objPlayer;          // �v���C���[�I�u�W�F�N�g
    [SerializeField] private CChargeSlider scChargeSlider;       // �`���[�W���Ԃ�\���X���C�_�[
    [Header("�����Ƃɏ����HP��")]
    [SerializeField] public int nAtkDecHp;      // ���ł�HP�����
    [Header("�З͒����Ɏg��HP��")]
    [SerializeField] private int nAdjustHp;      // ��������HP�����
    [Header("�ő咲���i�K��")]
    [SerializeField] private int maxDecStep;
    #endregion

    #region variable
    private STATE_BOW g_state;              // �|�̏��
    private GameObject objArrow;            // �|�I�u�W�F�N�g
    private float fChargeTime;              // �`���[�W�{�^���������Ă��鎞�� 
    private GameObject[] objCursur;         // �J�[�\��
    private float maxChargeTime;            // �ő�`���[�W����(Init�Ōv�Z���Ċi�[����)
    private float currentChargeStep;        // ���݂̃`���[�W�i�K��
    private bool isAdjust;                  // �g�p����HP�𒲐��������ǂ���
    private int nCurrentStep;               // ���݂̈З͒i�K��
    #endregion

    // Start is called before the first frame update
    #region init
    void Start()
    {
        g_state = STATE_BOW.BOW_NORMAL;
        fChargeTime = 0;
        // �J�[�\���̃T�C�h�̃I�u�W�F�N�g��S�Ď擾����
        objCursur = GameObject.FindGameObjectsWithTag("CursurSide");
        maxChargeTime = fValChargeTime * nMaxChargeStep;
        TellMaxChargeTime();        // �X���C�_�[�ɍő�`���[�W���Ԃ�`����

        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().SetChargeMaxTime(maxChargeTime);
        currentChargeStep = 0.0f;
        nCurrentStep = 0;       // �З͒i�K���̏�����

        isAdjust = false;       // �g�pHP��������Ԃɂ���
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
            scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MOVE);       // �X���C�_�[�𓮂���
            objPlayer.GetComponent<CSenaPlayer>().AddHp(-1 * nAtkDecHp);
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
                ChangeState(STATE_BOW.BOW_RESET);       // �`���[�W�����Z�b�g����
                ChangeState(STATE_BOW.BOW_NORMAL);      // �ʏ��ԂɕύX����
            }

            // �`���[�W���ɉE�N���b�N�������ꂽ�甭��
            if (Input.GetMouseButtonDown(1))
            {
                objPlayer.GetComponent<CSenaPlayer>().SetHp(-1 * nAtkDecHp);
                ChangeState(STATE_BOW.BOW_SHOT);      // ���ˏ�ԂɕύX����
                ChangeState(STATE_BOW.BOW_RESET);       // �`���[�W�����Z�b�g����
            }
        }
        #endregion
        //Debug.Log(g_state);
        //Debug.Log("Charge" + (int)fChargeTime);
        
        // �����HP�ʂ̒���
        #region adjust dec hp step
        if (Input.GetKeyDown(KeyCode.Q))
            AdjustUseHP(false);
        if (Input.GetKeyDown(KeyCode.E))
            AdjustUseHP(true);

        Debug.Log("Step:" + nCurrentStep);
        #endregion
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
                Destroy(objArrow);      // ������ł�����
                break;

            // �`���[�W���
            case STATE_BOW.BOW_CHARGE:
                g_state = STATE_BOW.BOW_CHARGE;
                CreateArrow();      // ��𐶐�����
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                objPlayer.GetComponent<CSenaPlayer>().SetHpBar();
                objArrow.GetComponent<CArrow>().Shot((int)fChargeTime);        // ��𔭎˂���
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                g_state = STATE_BOW.BOW_CHARGEMAX;
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.STOP);  // �J�[�\�����~�߂�
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MAXCHARGE);       // �X���C�_�[���~�߂�
                Debug.Log("ChargeMax");
                break;

            // �`���[�W���Z�b�g���
            case STATE_BOW.BOW_RESET:
                // ��𔭎˂��Ă��Ȃ����HP�o�[�����Z�b�g����
                objPlayer.GetComponent<CSenaPlayer>().ResetHPBar();
                ResetCharge();      // �`���[�W�����Z�b�g����
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
        switch (UpdateState)
        {
            // �ʏ���
            case STATE_BOW.BOW_NORMAL:
                break;

            // �`���[�W���
            case STATE_BOW.BOW_CHARGE:
                fChargeTime += Time.deltaTime;
                TellChargeTime();       // �`���[�W���Ԃ��X���C�_�[�ɓ`����
                if (fChargeTime > (currentChargeStep + 1) * fValChargeTime)
                    ++currentChargeStep;        // �`���[�W��1�i�K�グ��
                
                // �ő�`���[�W�i�K�ɂȂ�����
                if (currentChargeStep >= nMaxChargeStep)
                    ChangeState(STATE_BOW.BOW_CHARGEMAX);
                //Debug.Log("ChargeTime:" + fChargeTime.ToString("F1"));
                //Debug.Log("ChargeStep:" + currentChargeStep);
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                break;

            // �`���[�W���Z�b�g���
            case STATE_BOW.BOW_RESET:
                break;
        }
    }
    #endregion

    /*
    * @brief ��𐶐�����
    * @details ����|�̎q�I�u�W�F�N�g�Ƃ��Đ�������
    */
    #region create arrow
    private void CreateArrow()
    {
        // ��𕐊�̎q�I�u�W�F�N�g�Ƃ��ďo��
        objArrow = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.identity);
        objArrow.transform.parent = this.transform;
        objArrow.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        objArrow.transform.localScale = new Vector3(arrowSize, arrowSize, arrowSize);
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
        scChargeSlider.GetMaxChargeNum(maxChargeTime, nMaxChargeStep);
    }
    #endregion

    /*
    * @brief �`���[�W��Ԃ����Z�b�g����
    * @details �`���[�W��Ԃ������������Ƀ`���[�W���Ԃ�`���[�W�X�e�b�v������������
    */
    #region reset charge
    private void ResetCharge()
    {
        for (int i = 0; i < objCursur.Length; ++i)
            objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.RESET);  // �J�[�\�������ɖ߂�
        scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.RESET);       // �X���C�_�[�����ɖ߂�
        fChargeTime = 0.0f;     // �`���[�W��0�ɂ���
        currentChargeStep = 0;  // �`���[�W�i�K��0�ɖ߂�
    }
    #endregion

    /*
    * @brief �g�p����HP�𒲐�����
    * @param bool �����̒i�K���𑝂₷���ǂ���(true �� ���₷/ false ���@���炷)
    * @details�@�Ή��̃L�[�������ꂽ�ꍇ�A�g�p����HP�̒������s��
    */
    #region adjust use hp
    private void AdjustUseHP(bool add)
    {
        isAdjust = true;
        // �i�K���𑝂₷
        if (add)
        {
            ++nCurrentStep;
            if (nCurrentStep > maxDecStep)
                nCurrentStep = maxDecStep;
        }
        // �i�K�������炷
        else
        {
            --nCurrentStep;
            if (nCurrentStep < 0)
                nCurrentStep = 0;
        }
    }
    #endregion

    /*
    * @brief �З͒����i�K���̏���n��
    * @return int �З͒����i�K��
    */
    #region get step
    public int GetStep()
    {
        return nCurrentStep;
    }
    #endregion

    /*
    * @brief �ő�З͒����i�K���̏���n��
    * @return int �ő�З͒����i�K��
    */
    #region get max step
    public int GetMaxStep()
    {
        return maxDecStep;
    }
    #endregion
}
