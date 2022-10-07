using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * @brief �|�̃J�[�\���\��
 * @details �|�̃`���[�W���Ԃɉ����Ē��S�ɂ���Ă������o
 */

public class CCursur : MonoBehaviour
{
    // �J�[�\���̓���
    public enum KIND_CURSURMOVE
    {
        IDLE = 0,   // �ҋ@���
        MOVE,       // �����Ă�����
        STOP,       // ��~���
        RESET,      // ���ɖ߂��Ă�����
    }
    #region serialize field
    [SerializeField] private GameObject objCenter;      // �J�[�\���̒��S�_
    [Header("�J�[�\���̃��Z�b�g�ɂ����鎞��")]
    [SerializeField] private float fResetTime;      // �J�[�\���̃��Z�b�g�ɂ����鎞��
    #endregion

    // �ϐ��錾
    #region valiable
    private Vector2 fDistance;
    private float fChargeCnt;
    private KIND_CURSURMOVE kCursurMove;
    private Vector3 defaultPos;     // �J�[�\�������ʒu�i�[�p
    private Vector3 resetStartPos;          // �J�[�\���������ʒu�Ƀ��Z�b�g����Ƃ��̃X�^�[�g���W
    private float fTime;
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        kCursurMove = KIND_CURSURMOVE.IDLE;     // �ҋ@��Ԃɂ���
        defaultPos = transform.position;        // �����ʒu���擾����
        fTime = 0.0f;
        // �J�[�\���̒��S�_�Ƃ̍��W���r���Ď��g�̈ʒu��c������
        calcPosition();
        #region debug log
        //if (fDistance.x < 0)
        //    Debug.Log("left");
        //else if (fDistance.x > 0)
        //    Debug.Log("right");
        //else if (fDistance.y > 0)
        //    Debug.Log("up");
        //else if (fDistance.y < 0)
        //    Debug.Log("bottom");
        #endregion

    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        switch(kCursurMove)
        {
            // �����Ă鎞
            case KIND_CURSURMOVE.MOVE:
                MoveCursur();
                break;
            // ��~���Ă��鎞
            case KIND_CURSURMOVE.STOP:
                break;
            // ���ɖ߂��Ă��鎞
            case KIND_CURSURMOVE.RESET:
                ResetCursur();      // �J�[�\����߂�
                break;
            // �ҋ@���Ă��鎞
            case KIND_CURSURMOVE.IDLE:
                break;
        }
    }

    /*
    * @brief �J�[�\���̈ʒu��c������
    * @sa CCursur::Start()
    * @detail ����̃J�[�\���͒��S�_��X���W��Y���W�̂ǂ��炩�݈̂قȂ��Ă���Ƃ������Ƃ𗘗p���Ď��g�̈ʒu��c������
    */
    #region calc position
    private void calcPosition()
    {
        if (transform.position.x != objCenter.transform.position.x)
            fDistance.x = transform.position.x - objCenter.transform.position.x;
        else if (transform.position.y != objCenter.transform.position.y)
            fDistance.y = transform.position.y - objCenter.transform.position.y;
        else
            Debug.Log("<color=red>calcDistanceError</color>");
    }
    #endregion

    /*
    * @brief �J�[�\�����ړ����邩�ǂ����w�����o��
    * @sa CBow::Update()
    * @detail �w�����o������x�����Ă΂��
    */
    #region set cursur
    public void setCursur(KIND_CURSURMOVE cursurMove)
    {
        switch(cursurMove)
        {
            case KIND_CURSURMOVE.MOVE:
                kCursurMove = KIND_CURSURMOVE.MOVE;     // �����Ă����Ԃɂ���
                break;

            case KIND_CURSURMOVE.STOP:
                kCursurMove = KIND_CURSURMOVE.STOP;     // ��~��Ԃɂ���
                StopCursur();       // �J�[�\�������̏�Ŏ~�߂�
                break;

            case KIND_CURSURMOVE.RESET:
                kCursurMove = KIND_CURSURMOVE.RESET;     // ���ɖ߂���Ԃɂ���
                resetStartPos = transform.position;
                break;

            case KIND_CURSURMOVE.IDLE:
                kCursurMove = KIND_CURSURMOVE.IDLE;      // �ҋ@��Ԃɂ���
                // �������Ȃ�
                break;
        }
    }
    #endregion

    /*
    * @brief �J�[�\���𓮂���
    * @sa CCursur::Update()
    * @detail 
    */
    #region move cursur
    private void MoveCursur()
    {
        //Debug.Log("MoveCursur");
        
        Vector3 pos = transform.position;   // ���W�擾

        // ���E�̃J�[�\����X���W�������ʒu�ɖ߂�
        if (fDistance.x != 0.0f)
            pos.x -= (fDistance.x / fChargeCnt) * Time.deltaTime;
        // �㉺�̃J�[�\����X���W�������ʒu�ɖ߂�
        else if (fDistance.y != 0.0f)
            pos.y -= (fDistance.y / fChargeCnt) * Time.deltaTime;
        
        transform.position = pos;       // ���W�i�[

    }
#endregion

/*
* @brief �J�[�\�����~�߂�
* @sa CCursur::notifBowState()
* @detail 
*/
#region stop cursur
private void StopCursur()
    {
        Debug.Log("StopCursur");
    }
    #endregion

    /*
    * @brief �J�[�\����߂�
    * @sa CCursur::notifBowState()
    * @detail 
    */
    #region reset cursur
    private void ResetCursur()
    {
        //Debug.Log("ResetCursur");
        fTime += Time.deltaTime;
        Vector3 pos = transform.position;   // ���W�擾

        // ���E�̃J�[�\����X���W�������ʒu�ɖ߂�
        if (fDistance.x != 0.0f)
            pos.x += ((defaultPos.x - resetStartPos.x) / fResetTime) * Time.deltaTime;
        else if (fDistance.y != 0.0f)
            pos.y += ((defaultPos.y - resetStartPos.y) / fResetTime) * Time.deltaTime;

        // �����ʒu�ɖ߂�����ҋ@��Ԃɖ߂�
        if (fTime > fResetTime)
        {
            pos = defaultPos;
            fTime = 0.0f;
            setCursur(KIND_CURSURMOVE.IDLE);
        }

        transform.position = pos;       // ���W�i�[
    }
    #endregion

    /*
   * @brief �ő�`���[�W���Ԃ̎擾
   * @sa CBow::Start();
   * @detail 
   */
    #region set charge max time
    public void SetChargeMaxTime(float time)
    {
        fChargeCnt = time;
    }
    #endregion

}
