/**
 * @file        Player_Walk
 * @author      Shimizu_Yosuke
 * @date        2022/10/5
 * @brief       �v���C���[�̈ړ��𐧌䂷��
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{

    /** @brief �v���C���[�̈ړ����x*/
    [Header("�v���C���[�̈ړ��X�s�[�h")]
    [SerializeField] float fPlayerWalk = 2.0f;
    [Header("�v���C���[�̃W�����v�̑傫��")]
    [SerializeField] float fJumpPower = 6.0f;

    //�v���C���[�̍��W��ݒ肷�邽�߂̕ϐ�
    private Rigidbody rb;
    private CapsuleCollider cp;

    //�W�����v��x�o���Ȃ��悤�ɂ���t���O
    //true�ŃW�����v�o���Ȃ��@false�ŃW�����v�ł�����
    private bool bJumpFlg;
    //�n�ʂƐڐG���Ă��邩�̔���
    private bool bCollGround;

    // Animator �R���|�[�l���g
    private Animator animator;

    // �ݒ肵���t���O�̖��O
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";


    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;       
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cp = this.GetComponent<CapsuleCollider>();

        bCollGround = true;
        bJumpFlg = false;
        // �����ɐݒ肳��Ă���Animator�R���|�[�l���g���K������
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /**WASD�Ńv���C���[�𓮂����܂�**/
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0,0, fPlayerWalk);
            
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-fPlayerWalk, 0, 0);
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -fPlayerWalk);

            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(fPlayerWalk, 0, 0);

            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if(!Input.anyKey){
            // Run����Wait�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isJump,false);
        }


        // Wait or Run ����Jump�ɐ؂�ւ��鏈��
        // �X�y�[�X�L�[���������Ă���
        if (Input.GetKey(KeyCode.Space) && !bJumpFlg && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            // Wait or Run����Jump�ɑJ�ڂ���
            this.animator.SetBool(key_isJump, true);
            bJumpFlg = true;
            //�v���C���[�ɏ�x�N�g���̗͂�������
            rb.AddForce(0,fJumpPower,0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            Debug.Log("Rosalina");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                bJumpFlg = false;
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

}
