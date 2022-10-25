using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Effekseer;

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
        BOW_COLLDOWN,         // �N�[���_�E�����
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
    [Header("�З͂̒i�K��1�グ�邲�Ƃɑ�����U����")]
    [SerializeField] private int nAddAtk;
    [Header("�U���͂̏����l")]
    [SerializeField] private int nDefAtk;
    [Header("�\���Ă�Ƃ��̃L���L����")]
    [SerializeField] private AudioClip seCharge;
    [Header("�i�K���オ�������ɖ炷��")]
    [SerializeField] private AudioClip[] seUpStep;
    [Header("���ˉ�")]
    [SerializeField] private AudioClip seShot;
    [Header("�N�[���_�E���^�C��")]
    [SerializeField] private float fDownTime;
    #endregion

    #region variable
    private STATE_BOW g_state;              // �|�̏��
    public const int nMaxArrow = 10;
    private GameObject[] objArrow = new GameObject[nMaxArrow];            // �|�I�u�W�F�N�g
    private float fChargeTime;              // �`���[�W�{�^���������Ă��鎞�� 
    private GameObject[] objCursur;         // �J�[�\��
    private float maxChargeTime;            // �ő�`���[�W����(Init�Ōv�Z���Ċi�[����)
    private int currentChargeStep;        // ���݂̃`���[�W�i�K��
    private int nOldStep = 0;                   // �ߋ��̃`���[�W�i�K��
    private bool isAdjust;                  // �g�p����HP�𒲐��������ǂ���
    private int nCurrentAtkStep;            // ���݂̈З͒i�K��
    private int nCurrentArrowSetNum = 0;    // ���ݍ\���Ă����̔ԍ�
    //private int nUseHP = 0;                 // ������̂Ɏg�p����HP
    private bool isShot = false;            // ������������ǂ���
    private AudioSource audioSource;
    private GameObject objString;
    private EffekseerEmitter effString;
    private float fTimer;
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
        currentChargeStep = 0;
        nCurrentAtkStep = 0;       // �З͒i�K���̏�����

        isAdjust = false;       // �g�pHP��������Ԃɂ���
        audioSource = GetComponent<AudioSource>();
        fTimer = 0.0f;
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
        if (g_state != STATE_BOW.BOW_COLLDOWN)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeState(STATE_BOW.BOW_CHARGE);    // �`���[�W��ԂɕύX����
            }
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
                //ChangeState(STATE_BOW.BOW_COLLDOWN);    // �N�[���_�E���^�C����ԂɑJ�ڂ���
                //ChangeState(STATE_BOW.BOW_NORMAL);      // �ʏ��ԂɕύX����
                Destroy(objArrow[nCurrentArrowSetNum].gameObject);
            }

            // �`���[�W���ɉE�N���b�N�������ꂽ�甭��
            if (Input.GetMouseButtonDown(1))
            {
                ChangeState(STATE_BOW.BOW_SHOT);      // ���ˏ�ԂɕύX����
                //ChangeState(STATE_BOW.BOW_COLLDOWN);    // �N�[���_�E���^�C����ԂɑJ�ڂ���
                //ChangeState(STATE_BOW.BOW_RESET);       // �`���[�W�����Z�b�g����
            }
        }
        #endregion
        Debug.Log(g_state);
        Debug.Log("fTimer:" + fTimer);
        //Debug.Log("Charge" + (int)fChargeTime);
        
        // �����HP�ʂ̒���
        #region adjust dec hp step
        if (Input.GetKeyDown(KeyCode.Q))
            AdjustUseHP(false);
        if (Input.GetKeyDown(KeyCode.E))
            AdjustUseHP(true);

        //Debug.Log("Step:" + nCurrentAtkStep);
        #endregion

        //Debug.Log("�З͒i�K��" + nCurrentAtkStep);
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
                break;

            // �`���[�W���
            case STATE_BOW.BOW_CHARGE:
                g_state = STATE_BOW.BOW_CHARGE;
                CreateArrow();      // ��𐶐�����

                // ���̌`��ύX����
                //ChangeStringShape();
                objString = transform.Find("eff_string").gameObject;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
                effString.enabled = true;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(0);
                effString.enabled = false;

                // �J�[�\���𓮂���
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.MOVE); 
                
                // �`���[�W�X���C�_�[�𓮂���
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MOVE);      

                // �З͕���������
                int nUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(-1 * nUseHP);

                nOldStep = nCurrentAtkStep;

                // ���ʉ��Đ�
                audioSource.PlayOneShot(seUpStep[currentChargeStep]);
                audioSource.PlayOneShot(seCharge);
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                g_state = STATE_BOW.BOW_SHOT;
                isShot = true;
                audioSource.PlayOneShot(seShot);        // ���ʉ��̍Đ�
                //objPlayer.GetComponent<CCharactorManager>().SetHpBarAnim();
                //objPlayer.GetComponent<CSenaPlayer>().DecBGHPBar(-1 * nShotUseHP);

                int nShotUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                // Player��HP�𔭎˂Ɏg��HP+�З͒����Ɏg��HP�����炷
                objPlayer.GetComponent<CCharactorManager>().ChangeHP(-1 * nShotUseHP);
                int nAtkValue = nDefAtk + nAddAtk * nCurrentAtkStep;                 // ��̍U����
                objArrow[nCurrentArrowSetNum].GetComponent<CArrow>().Shot((int)fChargeTime, nAtkValue); // ��𔭎˂���
                ChangeState(STATE_BOW.BOW_RESET);    // ���Z�b�g����
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                g_state = STATE_BOW.BOW_CHARGEMAX;
                for (int i = 0; i < objCursur.Length; ++i)
                    objCursur[i].GetComponent<CCursur>().setCursur(CCursur.KIND_CURSURMOVE.STOP);  // �J�[�\�����~�߂�
                scChargeSlider.setSlider(CChargeSlider.KIND_CHRGSLIDERMOVE.MAXCHARGE);       // �X���C�_�[���~�߂�

                //Debug.Log("ChargeMax");
                break;

            // �`���[�W���Z�b�g���
            case STATE_BOW.BOW_RESET:
                g_state = STATE_BOW.BOW_RESET;

                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(0);
                effString.enabled = true;
                effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
                effString.enabled = false;

                ChangeState(STATE_BOW.BOW_COLLDOWN);    // �N�[���_�E���^�C����ԂɑJ�ڂ���


                // ��𔭎˂��Ă��Ȃ����HP�o�[�����Z�b�g����
                if (!isShot)
                {
                    int nResetUseHP = nAtkDecHp + nAdjustHp * nCurrentAtkStep;
                    objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(nResetUseHP);
                }
                else
                    isShot = false;
                //objPlayer.GetComponent<CSenaPlayer>().ResetHPBar();
                ResetCharge();      // �`���[�W�����Z�b�g����
                nCurrentAtkStep = 0;            // �i�K�������Z�b�g����
                break;

            // �N�[���_�E�����
            case STATE_BOW.BOW_COLLDOWN:
                g_state = STATE_BOW.BOW_COLLDOWN;
                fTimer = 0.0f;              // �^�C�}�[�̏�����
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

                Debug.Log("���݂̃X�e�b�v:" + nCurrentAtkStep);
                // �i�K�����ς������HP�o�[���C������
                if (nOldStep != nCurrentAtkStep)
                {
                    Debug.Log("�O�X�e�b�v:" + nOldStep);
                    Debug.Log("StepChange");
                    int nChargeUseHP = nAdjustHp * (nCurrentAtkStep - nOldStep);
                    objPlayer.GetComponent<CCharactorManager>().ChangeHPFront(-1 * nChargeUseHP);
                }
                if (fChargeTime > (currentChargeStep + 1) * fValChargeTime)
                {
                    ++currentChargeStep;        // �`���[�W��1�i�K�グ��
                    audioSource.PlayOneShot(seUpStep[currentChargeStep]);
                }

                // �ő�`���[�W�i�K�ɂȂ�����
                if (currentChargeStep >= nMaxChargeStep)
                    ChangeState(STATE_BOW.BOW_CHARGEMAX);

                nOldStep = nCurrentAtkStep;
                //Debug.Log("ChargeTime:" + fChargeTime.ToString("F1"));
                //Debug.Log("ChargeStep:" + currentChargeStep);
                break;

            // ���ˏ��
            case STATE_BOW.BOW_SHOT:
                break;

            // �ő�`���[�W���
            case STATE_BOW.BOW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                // �i�K�����ς������HP�o�[���C������
                if (nOldStep != nCurrentAtkStep)
                {
                    int nChargeUseHP = nAdjustHp * (nCurrentAtkStep - nOldStep);
                    objPlayer.GetComponent<CSenaPlayer>().ChangeHPFront(-1 * nChargeUseHP);
                }
                nOldStep = nCurrentAtkStep;

                break;

            // �`���[�W���Z�b�g���
            case STATE_BOW.BOW_RESET:
                break;

            // �N�[���_�E�����
            case STATE_BOW.BOW_COLLDOWN:
                fTimer += Time.deltaTime;              // �^�C�}�[�X�V

                // �N�[���_�E���^�C�����I��������ʏ��Ԃɖ߂�
                if(fTimer > fDownTime)
                {
                    ChangeState(STATE_BOW.BOW_NORMAL);
                }
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
        for (int i = 0; i < nMaxArrow; ++i)
        {
            if (objArrow[i] == null)
            {
                // ��𕐊�̎q�I�u�W�F�N�g�Ƃ��ďo��
                objArrow[i] = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.identity);
                objArrow[i].transform.parent = this.transform;
                objArrow[i].transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                objArrow[i].transform.localScale = new Vector3(arrowSize, arrowSize, arrowSize);
                nCurrentArrowSetNum = i;
                i = nMaxArrow;
            }
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
        scChargeSlider.GetChargeTime(fChargeTime, currentChargeStep);
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
            ++nCurrentAtkStep;
            // ����l�̐ݒ�
            if (nCurrentAtkStep > maxDecStep)
                nCurrentAtkStep = maxDecStep;
            // ����HP�𑝂₷
            //else
                //objPlayer.GetComponent<CSenaPlayer>().AddHp(-1 * nAdjustHp);
        }
        // �i�K�������炷
        else
        {
            --nCurrentAtkStep;
            // �����l�̐ݒ�
            if (nCurrentAtkStep < 0)
                nCurrentAtkStep = 0;
            // ����HP�����炷
            //else
                //objPlayer.GetComponent<CSenaPlayer>().AddHp(nAdjustHp);
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
        return nCurrentAtkStep;
    }
    #endregion

    /*
     * @brief �`���[�W�i�K���̏���n��
     * @return int �`���[�W�i�K��
    */
    #region get charge step
    public int GetChargeStep()
    {
        return currentChargeStep;
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
    
    /*
    * @brief ���̌`��ύX����
    */
    //#region change string shape
    //private void ChangeStringShape(bool flg)
    //{
    //    GameObject objString = transform.Find("eff_string").gameObject;
    //    EffekseerEmitter effString;
    //    effString = objString.GetComponent<CEffectManager>().GetEmitterEff(1);
    //    effString.enabled = true;
    //    effString = objString.GetComponent<CEffectManager>().GetEmitterEff((0);
    //    effString.enabled = false;
    //}
    //#endregion
}
