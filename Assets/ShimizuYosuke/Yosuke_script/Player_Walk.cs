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
    [Header("�v���C���[�̃X�e�[�^�X")]
    [SerializeField] float fPlayerWalk = 2.0f;
    [SerializeField] float fJumpPower = 6.0f;
    [SerializeField] private float PlayerRot = 1.0f;


    //�v���C���[�̍��W��ݒ肷�邽�߂̕ϐ�
    private Rigidbody rb;

    //�W�����v��x�o���Ȃ��悤�ɂ���t���O
    //true�ŃW�����v�o���Ȃ��@false�ŃW�����v�ł�����
    private bool bJumpFlg;

    //��]���n�߂����̊p�x�̕ۑ�
    private Vector3 fPivot;
    //�ǂꂭ�炢��]���Ă��邩��ۑ�����
    private float leftRot;
    private float rightRot;
    private float backRot;

    // Animator �R���|�[�l���g
    private Animator animator;

    // �ݒ肵���t���O�̖��O
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";

    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private float applySpeed = 0.2f;       // ��]�̓K�p���x

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;       
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        bJumpFlg = false;
        // �����ɐݒ肳��Ă���Animator�R���|�[�l���g���K������
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        ///**WASD�Ńv���C���[�𓮂����܂�**/
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //�ړ�
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // Wait����Run�ɑJ�ڂ���
        //    this.animator.SetBool(key_isRun, true);

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //�J�����̐��ʕ����Ƃ̌v�Z������
        //    Vector3 camera = Camera.main.transform.TransformDirection(Vector3.forward);
        //    Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

        //    Debug.Log(camera);

        //    //����]������
        //    leftRot += 0.5f;

        //    Debug.Log(this.transform.forward);

        //    //�ړ�
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;
        //    // Wait����Run�ɑJ�ڂ���
        //    this.animator.SetBool(key_isRun, true);

        //    if (leftRot <= 1.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, -45, 0));
        //    }

        //}
        //else {
        //    leftRot = 0.0f;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    //��]�ϐ����v���X���Ă���
        //    backRot += 0.5f;

        //    //�ړ�
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // Wait����Run�ɑJ�ڂ���
        //    this.animator.SetBool(key_isRun, true);

        //    if (backRot <= 2.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, -45, 0));
        //    }
        //}
        //else {
        //    backRot = 0.0f;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    //��]�����Ă���
        //    rightRot += 0.5f;

        //    //�ړ�
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // Wait����Run�ɑJ�ڂ���
        //    this.animator.SetBool(key_isRun, true);

        //    if (rightRot <= 1.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, 45, 0));
        //    }
        //}
        //else {
        //    rightRot = 0.0f;
        //}


        //-------------WASD------------------�ňړ��Q
        // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z += 1;
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z -= 1;
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
            // Wait����Run�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, true);
        }

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �ړ������x�N�g��(velocity)�𑫂����݂܂�
            transform.position += velocity;
        }

        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̉�](transform.rotation)�̍X�V
            // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A�ړ��̔��Ε���(-velocity)�ɉ񂷉�]�Ƃ��܂�
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(velocity),
                                                  applySpeed);

            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �ړ������x�N�g��(velocity)�𑫂����݂܂�
            transform.position += velocity;
        }


        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //����]
        if (Input.GetKey(KeyCode.Q))
        {
            leftRot += 0.01f;

            if (leftRot < 1.0f)
            {
                this.transform.eulerAngles -= new Vector3(0, PlayerRot, 0);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.A))
            {
                leftRot = 0.0f;
            }
        }

        //�E��]
        if (Input.GetKey(KeyCode.E))
        {
            rightRot += 0.01f;

            if (rightRot < 1.0f)
            {
                this.transform.eulerAngles += new Vector3(0, PlayerRot, 0);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.D))
            {
                rightRot = 0.0f;
            }
        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        if (!Input.anyKey){
            // Run����Wait�ɑJ�ڂ���
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isJump,false);

            //��]�����X�V����
            fPivot = this.transform.forward;

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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                bJumpFlg = false;
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

}
