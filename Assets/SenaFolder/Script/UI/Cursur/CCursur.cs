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
    #endregion

    // �ϐ��錾
    #region valiable
    private Vector2 fDistance;
    private bool bMove;     // �ړ����K�v���ǂ���
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        bMove = false;      // �ړ����Ȃ��悤�ɂ���
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

    // Update is called once per frame
    void Update()
    {

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
    */
    #region notificate bow state
    public void setCursur(KIND_CURSURMOVE cursurMove)
    {
        switch(cursurMove)
        {
            case KIND_CURSURMOVE.MOVE:
                // �J�[�\���𓮂���
                MoveCursur();
                break;

            case KIND_CURSURMOVE.STOP:
                // �J�[�\�������̏�Ŏ~�߂�
                StopCursur();
                break;

            case KIND_CURSURMOVE.RESET:
                // �J�[�\����߂�
                ResetCursur();
                break;

            case KIND_CURSURMOVE.IDLE:
                // �������Ȃ�
                break;
        }
    }
    #endregion

    /*
    * @brief �J�[�\���𓮂���
    * @sa CCursur::notifBowState()
    * @detail 
    */
    #region move cursur
    private void MoveCursur()
    {
        Debug.Log("MoveCursur");
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
    #region move cursur
    private void ResetCursur()
    {
        Debug.Log("ResetCursur");
    }
    #endregion

}
