/**
 * @file        Player_Walk
 * @author      Shimizu_Yosuke
 * @date        2022/10/5
 * @brief       �v���C���[�̈ړ��𐧌䂷��
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Walk : MonoBehaviour
{
    //�v���C���[�̃X�e�[�^�X
    public enum PLAYER_STATE {
        IDEL_STATE,
        WALK_STATE,
        RUN_STATE,
        JUMP_STATE,

        MAX_STATE
    }

    [Header("���̎��")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fall;

    private AudioSource AS;

    [Header("�s�b�`�̕ύX")]
    [SerializeField] private float pitchRange = 0.1f;

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

    //�v���C���[�̏��
    private PLAYER_STATE eState;
  
    //�ǂꂭ�炢��]���Ă��邩��ۑ�����
    private float leftRot;
    private float rightRot;

    // Animator �R���|�[�l���g
    private Animator animator;

    // �ݒ肵���t���O�̖��O
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    private const string key_isWalk = "isWalk";

    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x
    [SerializeField] private float applySpeed = 0.2f;       // ��]�̓K�p���x
    [SerializeField] private FollowCamera refCamera;        // �J�����̐�����]���Q�Ƃ���p
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

        AS = GetComponent<AudioSource>();

        eState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //���������Ă��Ȃ��Ƃ��͑ҋ@
        if (!Input.anyKey)
        {
            eState = 0;
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalk, false);
            this.animator.SetBool(key_isJump, false);
        }

        //�V�t�g���������Ă���Ƃ��̓A�j���[�V��������
        if (Input.GetKey(KeyCode.LeftShift)) {
            eState = 0;
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalk, false);
            this.animator.SetBool(key_isJump, false);
        }

        //-------------WASD------------------����
        // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z += 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z -= 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }

        if (eState == PLAYER_STATE.WALK_STATE)
        {
            moveSpeed = 5.0f;
            this.animator.SetBool(key_isWalk, true);
            this.animator.SetBool(key_isRun, false);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                eState = PLAYER_STATE.RUN_STATE;
                this.animator.SetBool(key_isRun, true);
            }
        }


        if (eState == PLAYER_STATE.RUN_STATE)
        {
            moveSpeed = 10.0f;

            if (Input.GetKey(KeyCode.W))
            {
                velocity.z += 0.1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity.x -= 0.1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity.z -= 0.1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity.x += 0.1f;
            }
        }

        //===============================================================================================================================
        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;
        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̉�](transform.rotation)�̍X�V
            // ����]��Ԃ̃v���C���[��Z+����(�㓪��)���A
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ��̔��Ε���(-velocity)�ɉ񂷉�]�ɒi�X�߂Â��܂�
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                  applySpeed);

            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �J�����̐�����](refCamera.hRotation)�ŉ񂵂��ړ�����(velocity)�𑫂����݂܂�
            transform.position += refCamera.hRotation * velocity;
        }
        //===============================================================================================================================

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

        // Wait or Run ����Jump�ɐ؂�ւ��鏈��
        // �X�y�[�X�L�[���������Ă���
        if (Input.GetKey(KeyCode.Space) && !bJumpFlg)
        {
            bJumpFlg = true;
            //�v���C���[�ɏ�x�N�g���̗͂�������
            rb.AddForce(0, fJumpPower, 0);
            eState = PLAYER_STATE.JUMP_STATE;
            this.animator.SetBool(key_isJump, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                if (bJumpFlg) {
                    bJumpFlg = false;
                    this.animator.SetBool(key_isRun, true);
                    this.animator.SetBool(key_isRun, false);
                    this.animator.SetBool(key_isWalk, false);
                }
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

    public void WalkSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange,pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(walk);
    }

    public void RunSound() {
        AS.pitch = 2.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(run);
    }

    public void JumpSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(jump);
    }

    public void FallSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(fall);
    }

}
