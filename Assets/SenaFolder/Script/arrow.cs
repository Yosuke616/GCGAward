using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private enum STATE_ARROW
    {
        ARROW_CHARGE = 0,       // �`���[�W���
        ARROW_NORMAL,           // �ʏ���
        ARROW_CHARGEMAX,        // �ő�`���[�W���
    }

    private STATE_ARROW g_state;
    [SerializeField] private GameObject PrefabArrow;       // ��̃I�u�W�F�N�g
    [SerializeField] private GameObject spawner;
    [SerializeField] private float maxChargeTime;
    private GameObject objArrow;
    private float fChargeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        g_state = STATE_ARROW.ARROW_NORMAL;
        fChargeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �X�V����
        UpdateState(g_state);
        // ���N���b�N�Ń`���[�W
        if (Input.GetMouseButtonDown(0))
            ChangeState(STATE_ARROW.ARROW_CHARGE);      // �`���[�W��ԂɕύX����

        // �`���[�W���ɍ��N���b�N�����ꂽ��`���[�W����
        if (g_state == STATE_ARROW.ARROW_CHARGE || g_state == STATE_ARROW.ARROW_CHARGEMAX)
            if(Input.GetMouseButtonUp(0))
            ChangeState(STATE_ARROW.ARROW_NORMAL);      // �ʏ��ԂɕύX����

        Debug.Log(g_state);
        Debug.Log("Charge" + (int)fChargeTime);
    }

    /*
    * @brief ��Ԃ�ύX�������Ɉ�x�����Ă΂��֐�
    * @param changeState �ύX�����̏��
    * @sa arrow::Update()
    * @details ��Ԃ�ύX�������Ƃ��ɐ��l�̏������Ȃǎn�߂Ɉ�x�������s���鏈��������
    */
    private void ChangeState(STATE_ARROW changeState)
    {
        switch(changeState)
        {
            // �ʏ���
            case STATE_ARROW.ARROW_NORMAL:
                g_state = STATE_ARROW.ARROW_NORMAL;
                fChargeTime = 0.0f;     // �`���[�W��0�ɂ���
                Destroy(objArrow);      // ������ł�����
                break;

            // �`���[�W���
            case STATE_ARROW.ARROW_CHARGE:
                g_state = STATE_ARROW.ARROW_CHARGE;
                // ��𕐊�̎q�I�u�W�F�N�g�Ƃ��ďo��
                objArrow = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
                objArrow.transform.parent = this.transform;
                break;

            // �ő�`���[�W���
            case STATE_ARROW.ARROW_CHARGEMAX:
                g_state = STATE_ARROW.ARROW_CHARGEMAX;
                break;
        }
    }
    
    /*
    * @brief ��Ԃ��Ƃ̍X�V����
    * @param UpdateState �X�V������
    * @sa arrow::Update()
    * @details ��̏�Ԃ��擾���Ė��t���[�����s���鏈��������
    */
    private void UpdateState(STATE_ARROW UpdateState)
    {
        switch(UpdateState)
        {
            // �ʏ���
            case STATE_ARROW.ARROW_NORMAL:
                break;

            // �`���[�W���
            case STATE_ARROW.ARROW_CHARGE:
                fChargeTime += Time.deltaTime;
                // maxChargeTime�ȏ�`���[�W����ƍő�`���[�W��Ԃɂ���
                if (fChargeTime > maxChargeTime)
                    ChangeState(STATE_ARROW.ARROW_CHARGEMAX);
                break;

            // �ő�`���[�W���
            case STATE_ARROW.ARROW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                break;
        }
    }
   

}
